using Grpc.Net.Client;
using ReviewService.Model;
using ReviewService.Repository;
using Host = ReviewService.Model.Host;

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
            await _guestRepository.Relate<Host, Rating>($"{{guestId :'{request.GuestId}'}}", $"{{hostId :'{request.HostId}'}}", rating, DateTime.Now, rating.Value);

            using var channel = GrpcChannel.ForAddress("http://localhost:8083");
            var client = new ReviewNotification.ReviewNotificationClient(channel);
            var reply = await client.RateHostAsync(
                            new RateHostNotificationRequest { HostId = request.HostId, Rating=request.Rating });

            //_logger.Log(LogLevel.Information, "Finished accepting reservation");
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

        public async Task DeleteAccommodationRating(DeleteAccommodationRatingRequest request)
        {
            await _guestRepository.DeleteRelationship<Accommodation, Rating>($"{{guestId :'{request.GuestId}'}}", $"{{accommodationId :'{request.AccommodationId}'}}", new Rating());
        }

        public async Task DeleteHostRating(DeleteHostRatingRequest request)
        {
            await _guestRepository.DeleteRelationship<Accommodation, Rating>($"{{guestId :'{request.GuestId}'}}", $"{{hostId :'{request.HostId}'}}", new Rating());
        }
    }
}
