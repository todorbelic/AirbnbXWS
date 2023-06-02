using MongoDB.Bson;

namespace UserService.Model
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}
