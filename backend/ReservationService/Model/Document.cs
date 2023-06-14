using MongoDB.Bson;

namespace ReservationService.Model
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}
