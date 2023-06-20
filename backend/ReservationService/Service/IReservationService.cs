using ReservationService.DTO;
using ReservationService.Model;

namespace ReservationService.Service
{
    public interface IReservationService
    {
        bool CanGuestRateHost(string guestId, string hostId);
        bool CanGuestRateAccommodation(string guestId, string accommodationId);
        bool IsHostNoteworthyReservationWise(string hostId);
        bool IsAccommodationAvailableForDateRange(IsAccommodationAvailableForDateRangeRequest dto);
        IEnumerable<ReservationView> GetActiveForHost(string hostId);
        IEnumerable<ReservationView> GetAllForHost(string hostId);
        IEnumerable<ReservationView> GetActiveForGuest(string guestId);
        IEnumerable<ReservationView> GetAllForGuest(string guestId);
        Task<bool> AcceptReservation(string reservationId);
        Task<bool> SendReservationRequest(SendReservationRequestRequest dto);
        Task<bool> CancelReservation(string reservationId);
        Task<bool> DeleteReservationRequest(string requestId);
        int GetCancellationNumberForGuest(string guestId);
        IEnumerable<ReservationView> GetReservationRequestsForGuest(string guestId);
        IEnumerable<ReservationView> GetReservationRequestsForHost(string hostId);
        Task<bool> DenyReservationRequest(string requestId);
        Task<ReservationView> GetById(string reservationId);
    }
}
