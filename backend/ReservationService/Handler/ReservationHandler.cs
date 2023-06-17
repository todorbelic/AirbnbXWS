using Grpc.Core;
using ReservationService.Service;

namespace ReservationService.Handler
{
    public class ReservationHandler : ReservationServiceRPC.ReservationServiceRPCBase
    {
        private readonly IReservationService _reservationService;

        public ReservationHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public override Task<GetActiveForHostResponse> GetActiveForHost(GetActiveForHostRequest request, ServerCallContext context)
        {
            /*
            var reservationViews = _reservationService.GetActiveForHost(request.HostId);
            if (reservationViews == null) var res = new GetActiveForHostResponse() { Reservations = new IEnumerable<ReservationViewDTO>() };
            var res = new GetActiveForHostResponse() {
            */
            return base.GetActiveForHost(request, context);
        }

        public override Task<GetActiveForGuestResponse> GetActiveForGuest(GetActiveForGuestRequest request, ServerCallContext context)
        {
            /*
            var reservationViews = _reservationService.GetActiveForGuest(guestId);
            if (reservationViews == null) return Enumerable.Empty<ReservationViewDTO>();
            return reservationViews;
            */
            return base.GetActiveForGuest(request, context);
        }

        public override async Task<AcceptReservationResponse> AcceptReservation(AcceptReservationRequest request, ServerCallContext context)
        {
            bool response = await _reservationService.AcceptReservation(request.Id);
            return new AcceptReservationResponse()
            {
                Response = response
            };
            
        }

        public override async Task<SendReservationRequestResponse> SendReservationRequest(SendReservationRequestRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "Entered send reservation request with host id " + request.Request.HostId);
            bool response =  await _reservationService.SendReservationRequest(request);
            return new SendReservationRequestResponse()
            {
                Response = response
            };
        }

        public override async Task<DeleteReservationRequestResponse> DeleteReservationRequest(DeleteReservationRequestRequest request, ServerCallContext context)
        {
           bool response = await _reservationService.DeleteReservationRequest(request.RequestId);
            return new DeleteReservationRequestResponse()
            {
                Response = response
            };
        }

        public override async Task<CancelReservationResponse> CancelReservation(CancelReservationRequest request, ServerCallContext context)
        {
            bool response = await _reservationService.CancelReservation(request.ReservationId);
            return new CancelReservationResponse()
            {
                Response = response
            };
        }

        public override Task<GetReservationRequestsForGuestResponse> GetReservationRequestsForGuest(GetReservationRequestsForGuestRequest request, ServerCallContext context)
        {
            /*
            var reservationViews = _reservationService.GetReservationRequestsForGuest(request.GuestId);
            if (reservationViews == null) return new GetReservationRequestsForGuestResponse() { Requests= new List<ReservationView>() };
           
            return reservationViews;
            */
            return base.GetReservationRequestsForGuest(request, context);
        }

        public override Task<GetReservationRequestsForHostResponse> GetReservationRequestsForHost(GetReservationRequestsForHostRequest request, ServerCallContext context)
        {
            /*
            var reservationViews = _reservationService.GetReservationRequestsForHost(hostId);
            if (reservationViews == null) return Enumerable.Empty<ReservationViewDTO>();
            return reservationViews;
            */
            return base.GetReservationRequestsForHost(request, context);
        }

        public override async Task<DenyReservationRequestResponse> DenyReservationRequest(DenyReservationRequestRequest request, ServerCallContext context)
        {
            bool response = await _reservationService.DenyReservationRequest(request.RequestId);
            return new DenyReservationRequestResponse() { Response = response };
        }

        public override Task<GetCancellationNumberForGuestResponse> GetCancellationNumberForGuest(GetCancellationNumberForGuestRequest request, ServerCallContext context)
        {
            return base.GetCancellationNumberForGuest(request, context);
        }
    }
}
