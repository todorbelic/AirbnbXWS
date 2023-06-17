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

        public override async Task<GetActiveForHostResponse> GetActiveForHost(GetActiveForHostRequest request, ServerCallContext context)
        {
            
            var res = new GetActiveForHostResponse();
            var reservationViews = _reservationService.GetActiveForHost(request.HostId);
            res.Reservations.AddRange(reservationViews);
            return res;
        }

        public override async Task<GetActiveForGuestResponse> GetActiveForGuest(GetActiveForGuestRequest request, ServerCallContext context)
        {
            var res = new GetActiveForGuestResponse();
            var reservationViews = _reservationService.GetActiveForGuest(request.GuestId);
            res.Reservations.AddRange(reservationViews);
            return res;
        }

        public override async Task<AcceptReservationResponse> AcceptReservation(AcceptReservationRequest request, ServerCallContext context)
        {
            bool response = await _reservationService.AcceptReservation(request.Id);
            return new AcceptReservationResponse()
            {
                Response = response
            };
            
        }

        //skontati sto ovde vraca prazan string
        public override async Task<SendReservationRequestResponse> SendReservationRequest(SendReservationRequestRequest request, ServerCallContext context)
        {
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

        public override async Task<GetReservationRequestsForGuestResponse> GetReservationRequestsForGuest(GetReservationRequestsForGuestRequest request, ServerCallContext context)
        {
            var res = new GetReservationRequestsForGuestResponse();
            var reservationViews = _reservationService.GetReservationRequestsForGuest(request.GuestId);
            res.Requests.AddRange(reservationViews);
            return res;
        }

        public override async Task<GetReservationRequestsForHostResponse> GetReservationRequestsForHost(GetReservationRequestsForHostRequest request, ServerCallContext context)
        {
            var res = new GetReservationRequestsForHostResponse();
            var reservationViews = _reservationService.GetReservationRequestsForHost(request.HostId);
            res.Requests.AddRange(reservationViews);
            return res;
        }

        public override async Task<DenyReservationRequestResponse> DenyReservationRequest(DenyReservationRequestRequest request, ServerCallContext context)
        {
            bool response = await _reservationService.DenyReservationRequest(request.RequestId);
            return new DenyReservationRequestResponse() { Response = response };
        }

        public override async Task<GetCancellationNumberForGuestResponse> GetCancellationNumberForGuest(GetCancellationNumberForGuestRequest request, ServerCallContext context)
        {
            int response =  _reservationService.GetCancellationNumberForGuest(request.GuestId);
            return new GetCancellationNumberForGuestResponse() { CancellationNumber = response };
        }
    }
}
