﻿using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize(Roles = "HOST")]
        public override async Task<GetActiveForHostResponse> GetActiveForHost(GetActiveForHostRequest request, ServerCallContext context)
        {
            
            var res = new GetActiveForHostResponse();
            var reservationViews = _reservationService.GetActiveForHost(request.HostId);
            res.Reservations.AddRange(reservationViews);
            return res;
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<GetActiveForGuestResponse> GetActiveForGuest(GetActiveForGuestRequest request, ServerCallContext context)
        {
            var res = new GetActiveForGuestResponse();
            var reservationViews = _reservationService.GetActiveForGuest(request.GuestId);
            res.Reservations.AddRange(reservationViews);
            return res;
        }

        [Authorize(Roles = "HOST")]
        public override async Task<AcceptReservationResponse> AcceptReservation(AcceptReservationRequest request, ServerCallContext context)
        {
            bool response = await _reservationService.AcceptReservation(request.ReservationId);

            return new AcceptReservationResponse()
            {
                Response = response
            };
            
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<SendReservationRequestResponse> SendReservationRequest(SendReservationRequestRequest request, ServerCallContext context)
        {
            bool response =  await _reservationService.SendReservationRequest(request);
            return new SendReservationRequestResponse()
            {
                Response = response
            };
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<DeleteReservationRequestResponse> DeleteReservationRequest(DeleteReservationRequestRequest request, ServerCallContext context)
        {
           bool response = await _reservationService.DeleteReservationRequest(request.ReservationId);
            return new DeleteReservationRequestResponse()
            {
                Response = response
            };
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<CancelReservationResponse> CancelReservation(CancelReservationRequest request, ServerCallContext context)
        {
            bool response = await _reservationService.CancelReservation(request.ReservationId);
            return new CancelReservationResponse()
            {
                Response = response
            };
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<GetReservationRequestsForGuestResponse> GetReservationRequestsForGuest(GetReservationRequestsForGuestRequest request, ServerCallContext context)
        {
            var res = new GetReservationRequestsForGuestResponse();
            var reservationViews = _reservationService.GetReservationRequestsForGuest(request.GuestId);
            res.Requests.AddRange(reservationViews);
            return res;
        }

        [Authorize(Roles = "HOST")]
        public override async Task<GetReservationRequestsForHostResponse> GetReservationRequestsForHost(GetReservationRequestsForHostRequest request, ServerCallContext context)
        {
            var res = new GetReservationRequestsForHostResponse();
            var reservationViews = _reservationService.GetReservationRequestsForHost(request.HostId);
            res.Requests.AddRange(reservationViews);
            return res;
        }

        [Authorize(Roles = "HOST")]
        public override async Task<DenyReservationRequestResponse> DenyReservationRequest(DenyReservationRequestRequest request, ServerCallContext context)
        {
            bool response = await _reservationService.DenyReservationRequest(request.ReservationId);
            return new DenyReservationRequestResponse() { Response = response };
        }

        [Authorize(Roles = "HOST")]
        public override async Task<GetCancellationNumberForGuestResponse> GetCancellationNumberForGuest(GetCancellationNumberForGuestRequest request, ServerCallContext context)
        {
            int response =  _reservationService.GetCancellationNumberForGuest(request.GuestId);
            return new GetCancellationNumberForGuestResponse() { CancellationNumber = response };
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<GetAllForGuestResponse> GetAllForGuest(GetAllForGuestRequest request, ServerCallContext context)
        {
            var res = new GetAllForGuestResponse();
            var reservationViews = _reservationService.GetAllForGuest(request.GuestId);
            res.Reservations.AddRange(reservationViews);
            return res;
        }

        [Authorize(Roles = "HOST")]
        public override async Task<GetAllForHostResponse> GetAllForHost(GetAllForHostRequest request, ServerCallContext context)
        {
            var res = new GetAllForHostResponse();
            var reservationViews = _reservationService.GetAllForHost(request.HostId);
            res.Reservations.AddRange(reservationViews);
            return res;
        }
    }
}
