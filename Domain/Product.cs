﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Task1_Marketplace.Domain
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
        public UserInfo AddedBy { get; set; }
    }

    public class UserInfo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
