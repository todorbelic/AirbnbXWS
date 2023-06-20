using AccommodationService.Services;
using Grpc.Core;

namespace AccommodationService.Handlers
{
    public class AccommodationReservationHandler : ReservationAccommodationRPC.ReservationAccommodationRPCBase
    {
        private readonly IAppAccommodationService _accommodationService;
        public AccommodationReservationHandler(IAppAccommodationService accommodationService) { 
        
            _accommodationService = accommodationService;
        }

        public override async Task<GetAccommodationViewForMultipleReservationsResponse> GetAccommodationViewForMultipleReservations(GetAccommodationViewForMultipleReservationsRequest request, ServerCallContext context)
        {
            IEnumerable<AccommodationForReservationView> dtos = _accommodationService.getAccommodationsForReservations(request.Ids);
            GetAccommodationViewForMultipleReservationsResponse response = new GetAccommodationViewForMultipleReservationsResponse();
            response.Accommodations.AddRange(dtos);
            return response;
        }


        public override async Task<GetAccommodationViewForReservationResponse> GetAccommodationViewForReservation(GetAccommodationViewForReservationRequest request, ServerCallContext context)
        {
            return new GetAccommodationViewForReservationResponse() { Accommodation = _accommodationService.GetAccommodationForReservation(request.Id) };
        }

        public override async Task<GetTypeOfReservationConfirmationResponse> GetTypeOfReservationConfirmation(GetTypeOfReservationConfirmationRequest request, ServerCallContext context)
        {
            string type = await  _accommodationService.GetTypeOfResConfirmationForAccommodation(request.AccommodationId);
            return new GetTypeOfReservationConfirmationResponse() { TypeOfConfirmation= type };
        }
    }
}
