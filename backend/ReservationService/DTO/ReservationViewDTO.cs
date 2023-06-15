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
    }
}
