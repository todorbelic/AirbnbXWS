using ReservationService.Enums;

namespace ReservationService.Model
{
    public class ReservationModel: Document
    {
        public string AccommodationId { get; set; }
        public string HostId { get; set; }
        public string GuestId { get; set; }
        public int GuestCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public AppReservationStatus Status { get; set; }

        public ReservationModel(string accommodationId, string hostId, string guestId, int guestCount, DateTime startDate, DateTime endDate, AppReservationStatus status)
        {
            AccommodationId = accommodationId;
            HostId = hostId;
            GuestId = guestId;
            GuestCount = guestCount;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }
}
