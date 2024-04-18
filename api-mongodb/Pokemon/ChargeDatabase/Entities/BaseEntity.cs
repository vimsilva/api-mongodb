﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_mongodb.ChargeDatabase.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
