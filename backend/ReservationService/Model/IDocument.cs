using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReservationService.Model
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
