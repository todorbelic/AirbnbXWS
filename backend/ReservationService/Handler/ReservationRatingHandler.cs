using Grpc.Core;
using ReservationService.Service;

namespace ReservationService.Handler
{
    public class ReservationRatingHandler : ReservationRatingServiceRPC.ReservationRatingServiceRPCBase
    {

        private readonly IReservationService _reservationService;

        public ReservationRatingHandler(IReservationService reservationService)
        {
            this._reservationService = reservationService;
        }

        public override Task<CanGuestRateHostResponse> CanGuestRateHost(CanGuestRateHostRequest request, ServerCallContext context)
        {
            bool response = _reservationService.CanGuestRateHost(request.GuestId, request.HostId);
            return Task.FromResult(new CanGuestRateHostResponse() { Response = response });
        }

        public override Task<CanGuestRateAccommodationResponse> CanGuestRateAccommodation(CanGuestRateAccommodationRequest request, ServerCallContext context)
        {
            bool response = _reservationService.CanGuestRateAccommodation(request.GuestId, request.AccommodationId);
            return Task.FromResult(new CanGuestRateAccommodationResponse() { Response = response });
        }
    }
}
