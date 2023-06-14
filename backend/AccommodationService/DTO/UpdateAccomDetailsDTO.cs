namespace AccommodationService.DTO
{
    public class UpdateAccomDetailsDTO
    {
        public string AccomId;
        public List<List<int>> Occasions { get; set; }
        public int NewPrice { get; set; }
    }
}
