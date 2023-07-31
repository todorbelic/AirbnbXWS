using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using ReviewService.Service;
using System.Data;

namespace ReviewService.Handlers
{
    public class ReviewHandler : RatingServiceRPC.RatingServiceRPCBase
    {
        private readonly IReviewService _reviewService;

        public ReviewHandler(IReviewService reviewService)
        {
            this._reviewService = reviewService;
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<RateAccommodationResponse> RateAccommodation(RateAccommodationRequest request, ServerCallContext context)
        {
            await _reviewService.RateAccomodation(request);
            return new RateAccommodationResponse();
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<RateHostResponse> RateHost(RateHostRequest request, ServerCallContext context)
        {
            await _reviewService.RateHost(request);
            return new RateHostResponse();
        }

        public override async Task<GetRatingsForAccommodationResponse> GetRatingsForAccommodation(GetRatingsForAccommodationRequest request, ServerCallContext context)
        {
            return await _reviewService.GetRatingsForAccommodation(request);
        }

        public override async Task<GetRatingsForHostResponse> GetRatingsForHost(GetRatingsForHostRequest request, ServerCallContext context)
        {
            return await _reviewService.GetRatingsForHost(request);
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<DeleteAccommodationRatingResponse> DeleteAccommodationRating(DeleteAccommodationRatingRequest request, ServerCallContext context)
        {
            await _reviewService.DeleteAccommodationRating(request);
            return new DeleteAccommodationRatingResponse();
        }

        [Authorize(Roles = "GUEST")]
        public override async Task<DeleteHostRatingResponse> DeleteHostRating(DeleteHostRatingRequest request, ServerCallContext context)
        {
            await _reviewService.DeleteHostRating(request);
            return new DeleteHostRatingResponse();
        }
    }
}
