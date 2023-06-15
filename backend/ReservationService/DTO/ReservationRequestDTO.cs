using ReservationService.Enums;

namespace ReservationService.DTO
{
    public class ReservationRequestDTO
    {
        public string AccommodationId { get; set; }
        public string HostId { get; set; }
        public string GuestId { get; set; }
        public int GuestCount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string Status { get; set; }
    }
}
