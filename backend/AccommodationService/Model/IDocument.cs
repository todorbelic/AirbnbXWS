using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;



namespace AccommodationService.Model
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
