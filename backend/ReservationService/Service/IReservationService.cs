using ReservationService.DTO;

namespace ReservationService.Service
{
    public interface IReservationService
    {
        Task<bool> CancelReservation(string reservationId);
        Task<bool> SendReservationRequest(ReservationRequestDTO dto);
        Task<ReservationViewDTO> GetById(string reservationId);
        Task<ReservationViewDTO> GetByAccommodationId(string accommodationId);
    }
}
