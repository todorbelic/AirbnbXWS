using AccommodationService.Attributes;

namespace AccommodationService.Model
{
    [BsonCollection("accommodations")]
    public class AppAccommodation:Document
    {
        public AppAccommodation() { }

        public string HostId { get; set; }
        public string Name { get; set; }

        public AppAddress Address { get; set; }

        public List<string> Benefits { get; set; }

        public List<string> Pictures { get; set; }

        public int MinGuests { get; set; }

        public int MaxGuests { get; set; }

        public PaymentOption PaymentOption { get; set; }

        public int Price { get; set; }
    }
}
