using ReservationService.DTO;
using ReservationService.Model;
using ReservationService.Service;
using Grpc.Core;

namespace ReservationService.Handler
{
    public class InternalReservationHandler : InternalReservationServiceRPC.InternalReservationServiceRPCBase
    {
        private readonly IReservationService _reservationService;
        public InternalReservationHandler(IReservationService reservationService) {
            _reservationService = reservationService;
        }
        
        public override async Task<CanGuestRateHostResponse> CanGuestRateHost(CanGuestRateHostRequest request, ServerCallContext context)
        {
            bool response = _reservationService.CanGuestRateHost(request.GuestId, request.HostId);
            return new CanGuestRateHostResponse() { Response = response };
        }

        public override async Task<CanGuestRateAccommodationResponse> CanGuestRateAccommodation(CanGuestRateAccommodationRequest request, ServerCallContext context)
        {
             bool response = _reservationService.CanGuestRateAccommodation(request.GuestId, request.AccommodationId);
           return new CanGuestRateAccommodationResponse() { Response = response };
        }

        public override async Task<IsAccommodationAvailableForDateRangeResponse> IsAccommodationAvailableForDateRange(IsAccommodationAvailableForDateRangeRequest request, ServerCallContext context)
        {
            bool response = _reservationService.IsAccommodationAvailableForDateRange(request);
            return new IsAccommodationAvailableForDateRangeResponse () { IsAvailable = response };
        }

        public override async Task<DoesGuestHaveActiveReservationsResponse> DoesGuestHaveActiveReservations(DoesGuestHaveActiveReservationsRequest request, ServerCallContext context)
        {
            bool response = _reservationService.GetActiveForGuest(request.GuestId).Count() > 0;
            return new DoesGuestHaveActiveReservationsResponse() { Response = response };
        }

        public override async Task<DoesHostHaveActiveReservationsResponse> DoesHostHaveActiveReservations(DoesHostHaveActiveReservationsRequest request, ServerCallContext context)
        {
            bool response = _reservationService.GetActiveForHost(request.HostId).Count() > 0;
            return new DoesHostHaveActiveReservationsResponse() { Response = response };
        }

        public override async Task<IsHostFeaturedResponse> IsHostFeatured(IsHostFeaturedRequest request, ServerCallContext context)
        {
            bool response = _reservationService.IsHostNoteworthyReservationWise(request.HostId);
            return new IsHostFeaturedResponse() { IsFeatured = response };
        }
    }
}
