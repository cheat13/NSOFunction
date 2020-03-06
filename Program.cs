using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using MongoDB.Driver;
using Newtonsoft.Json;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    class Program
    {
        private static string _path = @"E:\ContranZip\DataBlob";
        private static IMongoDatabase database { get; set; }
        private static IMongoCollection<SurveyData> surveyData { get; set; }
        private static IMongoCollection<ReportEaInfo> reportEAInfo { get; set; }
        private static IMongoCollection<RecordProcessed> recordProcessed { get; set; }
        private static IMongoCollection<EaApproved> eaApproved { get; set; }
        private static IMongoCollection<ContainerBlobData> containerBlobData { get; set; }
        private static ProcessFunction processFunction { get; set; }
        public static int Task = 0;
        public static int CurrentTask = 0;
        public static int SubTask = 0;
        public static int CurrentSubTask = 0;
        private const string animation = @"|/-\";
        private static int animationIndex = 0;

        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://firstclass:Th35F1rstCla55@mongodbnewzfdggw5bmqhbq-vm0.southeastasia.cloudapp.azure.com/water");
            database = client.GetDatabase("water");
            eaApproved = database.GetCollection<EaApproved>("EaApproved");
            recordProcessed = database.GetCollection<RecordProcessed>("recordprocessed");
            surveyData = database.GetCollection<SurveyData>("SurveyCleaning");

            processFunction = new ProcessFunction(database);

            var eaLst = eaApproved.AsQueryable().Select(it => it.EA).ToList();
            Processing(eaLst);

            Console.WriteLine("Done!");
        }

        private static void Processing(IEnumerable<string> eaLst)
        {
            var eaProcessed = recordProcessed.Find(it => eaLst.Contains(it._id) && it.Status == true)
                .Project(it => it._id)
                .ToListAsync()
                .GetAwaiter()
                .GetResult();

            var qryEA = eaLst.Except(eaProcessed);

            Task = qryEA.Count();

            foreach (var ea in qryEA)
            {
                ProcessEA(ea);
                CurrentTask++;
            }

            CurrentSubTask = SubTask;
            ProgressBar();
            SubTask = 0;
        }

        private static void ProcessEA(string ea)
        {
            try
            {
                ProgressBar();

                var dataLst = new List<DataProcessed>();
                var commuLst = new List<CommunitySample>();

                var surveys = surveyData.Aggregate()
                    .Match(it => it.EA == ea && it.Enlisted == true && it.DeletionDateTime == null)
                    .Project(it =>
                        new
                        {
                            SampleType = it.SampleType,
                            BuildingId = it.BuildingId,
                            ContainerName = it.ContainerName,
                            BlobName = it.BlobName
                        })
                    .ToListAsync()
                    .GetAwaiter()
                    .GetResult();

                SubTask = surveys.Count();

                var comLst = surveys.Where(it => it.SampleType == "c").ToList();

                foreach (var com in comLst)
                {
                    var existsComFile = File.Exists($@"{_path}\{com.ContainerName}\{com.BlobName}");
                    if (existsComFile)
                    {
                        var commu = ReadModelFrom<CommunitySample>(com.ContainerName, com.BlobName);
                        commuLst.Add(commu);
                    }
                }

                var qryCommu = commuLst
                    .GroupBy(it => string.Format("{0} {1}", it.Management?.Vil, it.Management?.Vil_name))
                    .Select(it => it.OrderByDescending(i => i._id).First())
                    .ToList();

                foreach (var commu in qryCommu)
                {
                    var dataProcessed = processFunction.CommunityProcessing(ea, commu);
                    dataLst.Add(dataProcessed);

                    CurrentSubTask++;
                    ProgressBar();
                }

                var sampleGroup = surveys.Except(comLst).GroupBy(it => it.BuildingId);

                foreach (var grp in sampleGroup)
                {
                    var bld = grp.FirstOrDefault(it => it.SampleType == "b");
                    if (bld == null) continue;

                    var existsBldFile = File.Exists($@"{_path}\{bld.ContainerName}\{bld.BlobName}");
                    if (existsBldFile)
                    {
                        var building = ReadModelFrom<BuildingSample>(bld.ContainerName, bld.BlobName);

                        var isType4or5 = building.BuildingType == BuildingType.Apartment || building.BuildingType == BuildingType.Office;
                        var isNotAllowGiveInfo = building.UnitAccess == UnitAccess.NotAllowGiveInfo;

                        if (isType4or5 && isNotAllowGiveInfo)
                        {
                            var dataProcessed = processFunction.BuildingProcessing(ea, building);
                            dataLst.Add(dataProcessed);

                            CurrentSubTask++;
                            ProgressBar();
                        }
                        else
                        {
                            var untLst = grp.Where(it => it.SampleType == "u").ToList();
                            foreach (var unt in untLst)
                            {
                                try
                                {
                                    var existsUntFile = File.Exists($@"{_path}\{unt.ContainerName}\{unt.BlobName}");
                                    if (existsUntFile)
                                    {
                                        var unit = ReadModelFrom<HouseHoldSample>(unt.ContainerName, unt.BlobName);
                                        var dataProcessedLst = processFunction.UnitProcessing(ea, unit, building, qryCommu);
                                        dataLst.AddRange(dataProcessedLst);
                                    }

                                    CurrentSubTask++;
                                    ProgressBar();
                                }
                                catch (System.Exception)
                                {
                                    throw;
                                }
                            }
                        }
                    }
                }

                if (dataLst.Any())
                {
                    WriteFile(ea, dataLst);
                    UpSertRecord(ea, true, string.Empty);
                }
                else
                {
                    UpSertRecord(ea, false, "Survey data in HDD not found.");
                }
            }
            catch (System.Exception e)
            {
                UpSertRecord(ea, false, e.Message);
            }

            CurrentSubTask = SubTask;
            ProgressBar();
            CurrentSubTask = 0;
        }

        private static T ReadModelFrom<T>(string containerName, string blobName)
        {
            using (var reader = new StreamReader($@"{_path}\{containerName}\{blobName}"))
            {
                var jsonString = reader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<T>(jsonString);

                return model;
            }
        }

        private static void ProgressBar()
        {
            var progressTask = (Task > 0) ? (double)CurrentTask / Task : 0;
            var progressSubTask = (SubTask > 0) ? (double)CurrentSubTask / SubTask : 0;

            if (Task > 0) progressSubTask /= Task;

            var currentProgress = progressTask + progressSubTask;
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
