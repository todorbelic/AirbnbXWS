using MongoDB.Bson;

namespace NotificationService.Model
{
    public class Document : IDocument
    {
        public ObjectId Id { get; set ; }
    }
}
