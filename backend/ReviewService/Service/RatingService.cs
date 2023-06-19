using ReviewService.Model;
using ReviewService.Repository;

namespace ReviewService.Service
{
    public class RatingService : IReviewService
    {
        private readonly IRepository<Guest> _guestRepository;

        public RatingService(IRepository<Guest> guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task RateAccomodation(RateAccommodationRequest request)
        {
            Rating rating = new Rating() { Value = (int)request.Rating };
            await _guestRepository.Relate<Accommodation, Rating>($"{{guestId :'{request.GuestId}'}}", $"{{accommodationId :'{request.AccommodationId}'}}", rating, DateTime.Now, rating.Value);
        }

        public async Task RateHost(RateHostRequest request)
        {
            Rating rating = new Rating() { Value = (int)request.Rating };
            await _guestRepository.Relate<Accommodation, Rating>($"{{guestId :'{request.GuestId}'}}", $"{{hostId :'{request.HostId}'}}", rating, DateTime.Now, rating.Value);
        }

        public async Task<GetRatingsForAccommodationResponse> GetRatingsForAccommodation(GetRatingsForAccommodationRequest request) 
        {
            string nodeQuery = "n: Accommodation";
            string whereQuery = $"WHERE n.accommodationId = '{request.AccommodationId}'";
            return await _guestRepository.GetAccommodationRatings(nodeQuery, whereQuery, "accommodationId");
        }

        public async Task<GetRatingsForHostResponse> GetRatingsForHost(GetRatingsForHostRequest request)
        {
            string nodeQuery = "n: Host";
            string whereQuery = $"WHERE n.hostId = '{request.HostId}'";
            return await _guestRepository.GetHostRatings(nodeQuery, whereQuery, "hostId");
        }
    }
}
