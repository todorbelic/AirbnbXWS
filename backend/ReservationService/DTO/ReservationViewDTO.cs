using System;

namespace ReservationService.DTO
{
    public class ReservationViewDTO
    {
        public string Id { get; set; }
        public string HostName { get; set; }
        public string GuestName { get; set; }
        public string AccommodationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestCount { get; set; }

        public ReservationViewDTO(string id, string hostName, string guestName, string accommodationName, DateTime startDate, DateTime endDate, int guestCount)
        {
            Id = id;
            HostName = hostName;
            GuestName = guestName;
            AccommodationName = accommodationName;
            StartDate = startDate;
            EndDate = endDate;
            GuestCount = guestCount;
        }
    }
}
