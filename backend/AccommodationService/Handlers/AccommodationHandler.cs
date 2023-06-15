using AccommodationService.Model;
using AccommodationService.Services;
using Grpc.Core;

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
                Accommodations= {accommodations}
            });
        }

        public override Task<GetAccommodationResponse> GetAccommodation(GetAccommodationRequest request, ServerCallContext context)
        {
            return base.GetAccommodation(request, context);
        }
    }
}
