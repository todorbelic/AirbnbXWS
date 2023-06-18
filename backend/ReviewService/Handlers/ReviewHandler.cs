using Grpc.Core;

namespace ReviewService.Handlers
{
    public class ReviewHandler : RatingService.RatingServiceBase
    {
        public override Task<RateAccommodationResponse> RateAccommodation(RateAccommodationRequest request, ServerCallContext context)
        {
            return base.RateAccommodation(request, context);
        }

        public override Task<RateHostResponse> RateHost(RateHostRequest request, ServerCallContext context)
        {
            return base.RateHost(request, context);
        }

        public override Task<GetRatingsForAccommodationResponse> GetRatingsForAccommodation(GetRatingsForAccommodationRequest request, ServerCallContext context)
        {
            return base.GetRatingsForAccommodation(request, context);
        }

        public override Task<GetRatingsForHostResponse> GetRatingsForHost(GetRatingsForHostRequest request, ServerCallContext context)
        {
            return base.GetRatingsForHost(request, context);
        }
    }
}
