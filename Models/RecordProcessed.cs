using System;
using MongoDB.Bson.Serialization.Attributes;

namespace NSOFunction.Models
{
    public class RecordProcessed
    {
        [BsonId]
        public string _id { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string errorMessage { get; set; }
    }
}
