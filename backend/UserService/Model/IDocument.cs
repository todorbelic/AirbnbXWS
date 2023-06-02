using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Model
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
