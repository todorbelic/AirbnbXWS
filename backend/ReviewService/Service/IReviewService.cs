namespace ReviewService.Service
{
    public interface IReviewService
    {
        Task RateAccomodation(RateAccommodationRequest request);
        Task RateHost(RateHostRequest request);
        Task<GetRatingsForAccommodationResponse> GetRatingsForAccommodation(GetRatingsForAccommodationRequest request);
        Task<GetRatingsForHostResponse> GetRatingsForHost(GetRatingsForHostRequest request);
    }
}
