namespace ReviewService.Model
{
    public class Host : Neo4jEntity
    {
        public string HostId { get; set; }
        public DateTimeOffset Created { get; set; }

        public Host()
        {
            Label = "Host";
            Created = DateTimeOffset.UtcNow;
        }
    }
}
