namespace ReviewService.Model
{
    public class Accommodation : Neo4jEntity
    {
        public string AccommodationId { get; set; }
        public DateTimeOffset Created { get; set; }

        public Accommodation()
        {
            Label = "Accommodation";
            Created = DateTimeOffset.UtcNow;
        }
    }
}
