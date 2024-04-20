using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace api_mongodb.Core
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
