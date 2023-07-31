using AccommodationService.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace AccommodationService.Handlers
{
    public class AccommodationHandler : AccommodationServiceRPC.AccommodationServiceRPCBase
    {
        private readonly ILogger<AccommodationHandler> _logger;
        private readonly IAppAccommodationService _accommodationService;
        
        public AccommodationHandler(ILogger<AccommodationHandler> logger, IAppAccommodationService accommodationService)
        {
            _logger = logger;
            _accommodationService = accommodationService;
        }
        [Authorize(Roles = "HOST")]
        public override async Task<CreateAccommodationResponse> CreateAccommodation(CreateAccommodationRequest request, ServerCallContext context)
        {
            await _accommodationService.AddAccommodation(request.Request);
            return new CreateAccommodationResponse()
            {
                Response = "uspesno kreiranje"
            };
        }

        public override Task<GetAllResponse> GetAllAccommodations(GetAllRequest request, ServerCallContext context)
        {
            List<Accommodation> accommodations = _accommodationService.Get();

            return Task.FromResult(new GetAllResponse()
            {
                Accommodations = {accommodations}
            });
        }

        public override async Task<GetAccommodationResponse> GetAccommodation(GetAccommodationRequest request, ServerCallContext context)
        {
            return new GetAccommodationResponse() { Accommodation = await _accommodationService.GetAccommodation(request.Id) };
        }

        public override Task<SearchAccommodationsResponse> SearchAccommodations(SearchAccommodationsRequest request, ServerCallContext context)
        {
            List<AccommodationSearch> accommodationSearched = _accommodationService.SearchAccommodations(request);
            return Task.FromResult(new SearchAccommodationsResponse()
            {
                Accommodations = { accommodationSearched}
            });
        }
    }
}
