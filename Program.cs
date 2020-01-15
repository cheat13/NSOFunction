using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using CsvHelper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MongoDB.Driver;
using Newtonsoft.Json;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    class Program
    {
        private static IMongoCollection<SurveyData> surveyData { get; set; }
        private static IMongoCollection<ReportEaInfo> reportEAInfo { get; set; }
        private static IMongoCollection<RecordProcessed> recordProcessed { get; set; }
        private static CloudBlobClient blobClient { get; set; }
        private static ProcessFunction processFunction { get; set; }
        public static int Task = 0;
        public static int CurrentTask = 0;
        public static int CWTTask = 0;
        public static int CurrentCWTTask = 0;
        public static int EATask = 0;
        public static int CurrentEATask = 0;
        private const string animation = @"|/-\";
        private static int animationIndex = 0;

        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://dbagent:Nso4Passw0rd5@mongodbproykgte5e7lvm7y-vm0.southeastasia.cloudapp.azure.com/nso");
            var database = client.GetDatabase("nso");
            reportEAInfo = database.GetCollection<ReportEaInfo>("reporteainfo");
            surveyData = database.GetCollection<SurveyData>("survey");
            recordProcessed = database.GetCollection<RecordProcessed>("recordprocessed");

            var storage = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=nsostorage;AccountKey=Lzw/JGZTvtHoRxo8GKjWQy5rm3vprPahD4YsoRXhi7Ai+Gyg34tq+Y+HrEuNf5SOAib1jNNavkFVghaKk/488w==;EndpointSuffix=core.windows.net");
            blobClient = storage.CreateCloudBlobClient();

            processFunction = new ProcessFunction();

            var CWTLst = reportEAInfo.AsQueryable().Select(it => it.CWT).Distinct().OrderBy(it => it).Skip(64).Take(13).ToList();
            ProcessCWTLst(CWTLst);

            // Console.Write("CWT: ");
            // var cwt = Console.ReadLine();
            // ProcessCWT(cwt);

            // Console.Write("Area_Code: ");
            // var area_Code = Console.ReadLine();
            // ProcessTAM(area_Code);

            // Console.Write("EA: ");
            // var ea = Console.ReadLine();
            // var hasProcessed = recordProcessed.Find(it => it._id == ea && it.Status == true).AnyAsync().GetAwaiter().GetResult();
            // if (!hasProcessed)
            // {
            //     ProcessEA(ea);
            // }

            Console.WriteLine("Done!");
        }

        private static void ProcessCWTLst(IEnumerable<string> cwtLst)
        {
            Task = cwtLst.Count();
            Console.WriteLine($"CWT count: {cwtLst.Count()}");

            foreach (var cwt in cwtLst)
            {
                ProcessCWT(cwt);
                CurrentTask++;
            }

            CurrentTask = Task;
            ProgressBar();
        }

        private static void ProcessCWT(string cwt)
        {
            Processing(it => it.CWT == cwt && (it.DateApproveFs.HasValue || it.DateApprovePs.HasValue));
        }

        private static void ProcessTAM(string area_Code)
        {
            Processing(it => it.Area_Code == area_Code && (it.DateApproveFs.HasValue || it.DateApprovePs.HasValue));
        }

        private static void Processing(Expression<Func<ReportEaInfo, bool>> expression)
        {
            var eaLst = reportEAInfo.Find(expression)
                .Project(it => it._id)
                .ToListAsync()
                .GetAwaiter()
                .GetResult();

            var eaProcessed = recordProcessed.Find(it => eaLst.Contains(it._id) && it.Status == true)
                .Project(it => it._id)
                .ToListAsync()
                .GetAwaiter()
                .GetResult();

            var qryEA = eaLst.Except(eaProcessed);

            CWTTask = qryEA.Count();

            foreach (var ea in qryEA)
            {
                ProcessEA(ea);
                CurrentCWTTask++;
            }

            CurrentCWTTask = CWTTask;
            ProgressBar();
            CWTTask = 0;
        }

        private static void ProcessEA(string ea)
        {
            try
            {
                ProgressBar();

                var dataLst = new List<DataProcessed>();
                var commuLst = new List<CommunitySample>();

                var surveys = surveyData.Find(it => it.EA == ea && it.Enlisted == true && it.DeletionDateTime == null)
                    .ToListAsync()
                    .GetAwaiter()
                    .GetResult();

                EATask = surveys.Count();

                var comLst = surveys.Where(it => it.SampleType == "c").ToList();

                foreach (var com in comLst)
                {
                    var container = blobClient.GetContainerReference(com.ContainerName);
                    var commu = ReadModelFrom<CommunitySample>(container, com.BlobName);
                    commuLst.Add(commu);
                }

                var qryCommu = commuLst
                    .GroupBy(it => string.Format("{0} {1}", it.Management?.Vil, it.Management?.Vil_name))
                    .Select(it => it.OrderByDescending(i => i._id).First())
                    .ToList();

                foreach (var commu in qryCommu)
                {
                    var dataProcessed = processFunction.CommunityProcessing(ea, commu);
                    dataLst.Add(dataProcessed);

                    CurrentEATask++;
                    ProgressBar();
                }

                var sampleGroup = surveys.Except(comLst).GroupBy(it => it.BuildingId);

                foreach (var grp in sampleGroup)
                {
                    var bld = grp.FirstOrDefault(it => it.SampleType == "b");
                    if (bld == null)
                    {
                        continue;
                    }
                    var container = blobClient.GetContainerReference(bld.ContainerName);
                    var building = ReadModelFrom<BuildingSample>(container, bld.BlobName);

                    var isType4or5 = building.BuildingType == BuildingType.Apartment || building.BuildingType == BuildingType.Office;
                    var isNotAllowGiveInfo = building.UnitAccess == UnitAccess.NotAllowGiveInfo;

                    if (isType4or5 && isNotAllowGiveInfo)
                    {
                        var dataProcessed = processFunction.BuildingProcessing(ea, building);
                        dataLst.Add(dataProcessed);

                        CurrentEATask++;
                        ProgressBar();
                    }
                    else
                    {
                        var untLst = grp.Where(it => it.SampleType == "u").ToList();
                        foreach (var unt in untLst)
                        {
                            try
                            {
                                var containerUnt = blobClient.GetContainerReference(unt.ContainerName);
                                var unit = ReadModelFrom<HouseHoldSample>(containerUnt, unt.BlobName);
                                var dataProcessedLst = processFunction.UnitProcessing(ea, unit, building, qryCommu);
                                dataLst.AddRange(dataProcessedLst);

                                CurrentEATask++;
                                ProgressBar();
                            }
                            catch (System.Exception)
                            {
                                throw;
                            }
                        }
                    }
                }

                WriteFile(ea, dataLst);
                UpSertRecord(ea, true, string.Empty);
            }
            catch (System.Exception e)
            {
                UpSertRecord(ea, false, e.Message);
            }

            CurrentEATask = EATask;
            ProgressBar();
            CurrentEATask = 0;
        }

        private static T ReadModelFrom<T>(CloudBlobContainer container, string fileName)
        {
            var blob = container.GetBlockBlobReference(fileName);
            var blobContent = blob.DownloadTextAsync().GetAwaiter().GetResult();
            var model = JsonConvert.DeserializeObject<T>(blobContent);

            return model;
        }

        private static void ProgressBar()
        {
            var progressTask = (Task > 0) ? (double)CurrentTask / Task : 0;
            var progressCWTTask = (CWTTask > 0) ? (double)CurrentCWTTask / CWTTask : 0;
            var progressEATask = (EATask > 0) ? (double)CurrentEATask / EATask : 0;

            if (Task > 0) progressCWTTask /= Task;
            if (CWTTask > 0) progressEATask /= Task * CWTTask;

            var currentProgress = progressTask + progressCWTTask + progressEATask;
            var progressBlockCount = (int)(currentProgress * 50);
            var percent = currentProgress * 100;
            var progress = string.Format("\r[{0}{1}] {2,6:N2}%  {3}",
                new string('=', progressBlockCount),
                new string(' ', 50 - progressBlockCount),
                percent,
                animation[animationIndex++ % animation.Length]);

            Console.Write(progress);
        }

        private static void WriteFile(string ea, List<DataProcessed> dataLst)
        {
            var reg = ea.Substring(0, 1);
            var cwt = ea.Substring(1, 2);
            var amp = ea.Substring(1, 4);
            var tam = ea.Substring(1, 6);
            var path = $@"DataProcesses\{reg}\{cwt}\{amp}\{tam}";
            var filePath = $@"DataProcesses\{reg}\{cwt}\{amp}\{tam}\{ea}.csv";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(dataLst);
            }
        }

        private static void UpSertRecord(string ea, bool status, string message)
        {
            var def = Builders<RecordProcessed>.Update
                .Set(it => it.Status, status)
                .Set(it => it.CreatedDateTime, DateTime.Now)
                .Set(it => it.errorMessage, message);

            recordProcessed.UpdateOne(it => it._id == ea, def, new UpdateOptions { IsUpsert = true });
        }
    }
}
