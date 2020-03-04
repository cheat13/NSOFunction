using MongoDB.Bson.Serialization.Attributes;

namespace NSOFunction.Models
{
    public class EaApproved
    {
        [BsonId]
        public string _id { get; set; }
        public string EA { get; set; }
    }
}