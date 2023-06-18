namespace ReviewService.Model
{
    public class Guest : Neo4jEntity
    {
        public string GuestId { get; set; }
        public DateTimeOffset Created { get; set; }

        public Guest()
        {
            Label = "Guest";
            Created = DateTimeOffset.UtcNow;
        }
    }
}
