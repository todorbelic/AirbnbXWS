namespace ReviewService.Model
{
    public class Rating : Neo4jRelationship
    {
        public int Value { get; set; }
        public DateTime Date { get; set; }

        public DateTimeOffset Created { get; set; }

        public Rating()
        {
            Name = "RATE";
            Created = DateTimeOffset.UtcNow;
        }


    }
}
