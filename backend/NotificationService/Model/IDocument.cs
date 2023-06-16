using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;



namespace NotificationService.Model
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
