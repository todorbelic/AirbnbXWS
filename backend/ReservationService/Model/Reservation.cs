using ReservationService.Attributes;
using ReservationService.Enums;

namespace ReservationService.Model
{
    [BsonCollection("reservations")]
    public class Reservation: Document
    {
        public string AccommodationId { get; set; }
        public string HostId { get; set; }
        public string GuestId { get; set; }
        public int GuestCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ReservationStatus Status { get; set; }

        public Reservation(string accommodationId, string hostId, string guestId, int guestCount, DateTime startDate, DateTime endDate, ReservationStatus status)
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
