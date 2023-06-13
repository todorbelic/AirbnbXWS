using MongoDB.Bson;

namespace AccommodationService.Model
{
    public class Document : IDocument
    {
        public ObjectId Id { get; set ; }
    }
}
