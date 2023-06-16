namespace ReviewService.Model
{
    public class Rating : Neo4jRelationship
    {
        public double Value { get; set; }
        public DateOnly Date { get; set; }

        public DateTimeOffset Created { get; set; }

        public Rating()
        {
            Name = "RATE";
            Created = DateTimeOffset.UtcNow;
        }


    }
}
