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
        IEnumerable<ReservationViewDTO> GetActiveForHost(string hostId);
        IEnumerable<ReservationViewDTO> GetActiveForGuest(string guestId);
        Task<bool> AcceptReservation(string reservationId);
        Task<bool> SendReservationRequest(SendReservationRequestRequest dto);
        Task<bool> CancelReservation(string reservationId);
        Task<bool> DeleteReservationRequest(string requestId);
        IEnumerable<ReservationViewDTO> GetReservationRequestsForGuest(string guestId);
        IEnumerable<ReservationViewDTO> GetReservationRequestsForHost(string hostId);
        Task<bool> DenyReservationRequest(string requestId);
        Task<ReservationViewDTO> GetById(string reservationId);
    }
}
