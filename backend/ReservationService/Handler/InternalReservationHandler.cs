using ReservationService.DTO;
using ReservationService.Model;
using ReservationService.Service;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

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

        public override Task<CanGuestRateAccommodationResponse> CanGuestRateAccommodation(CanGuestRateAccommodationRequest request, ServerCallContext context)
        {
            //return _reservationService.CanGuestRateAccommodation(request.GuestId, request.AccommodationId);
            return base.CanGuestRateAccommodation(request, context);
        }

        public override Task<IsAccommodationAvailableForDateRangeResponse> IsAccommodationAvailableForDateRange(IsAccommodationAvailableForDateRangeRequest request, ServerCallContext context)
        {
            //return _reservationService.IsAccommodationAvailableForDateRange(dto);
            return base.IsAccommodationAvailableForDateRange(request, context);
        }

        public override Task<DoesGuestHaveActiveReservationsResponse> DoesGuestHaveActiveReservations(DoesGuestHaveActiveReservationsRequest request, ServerCallContext context)
        {
            return base.DoesGuestHaveActiveReservations(request, context);
        }

        public override Task<DoesHostHaveActiveReservationsResponse> DoesHostHaveActiveReservations(DoesHostHaveActiveReservationsRequest request, ServerCallContext context)
        {
            return base.DoesHostHaveActiveReservations(request, context);
        }
    }
}
