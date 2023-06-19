namespace ReviewService.Service
{
    public interface IReviewService
    {
        Task RateAccomodation(RateAccommodationRequest request);
        Task RateHost(RateHostRequest request);
        Task<GetRatingsForAccommodationResponse> GetRatingsForAccommodation(GetRatingsForAccommodationRequest request);
        Task<GetRatingsForHostResponse> GetRatingsForHost(GetRatingsForHostRequest request);
        public Task DeleteAccommodationRating(DeleteAccommodationRatingRequest request);
        public Task DeleteHostRating(DeleteHostRatingRequest request);
    }
}
