using System;
using MongoDB.Bson.Serialization.Attributes;

namespace NSOFunction.Models
{
    public class ContainerBlobData
    {
        [BsonId]
        public string _id { get; set; }
        public string ContainerName { get; set; }
        public string BlobName { get; set; }
    }
}
