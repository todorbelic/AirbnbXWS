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
            if (!CreateCanGuestRateAccommodationRequest(request.AccommodationId, request.GuestId)) throw new Exception("guest cannot rate this accommodation!");
            Rating rating = new Rating() { Value = (int)request.Rating };
            await _guestRepository.Relate<Accommodation, Rating>($"{{guestId :'{request.GuestId}'}}", $"{{accommodationId :'{request.AccommodationId}'}}", rating, DateTime.Now, rating.Value);
        }

        
        private bool CreateCanGuestRateAccommodationRequest(string accommodationId, string guestId)
        {
            CanGuestRateAccommodationRequest request = new CanGuestRateAccommodationRequest() { GuestId = guestId, AccommodationId = accommodationId };
            var channel = GrpcChannel.ForAddress("http://reservation_service:8080");
            var client = new InternalReservationServiceRPC.InternalReservationServiceRPCClient(channel);
            return client.CanGuestRateAccommodation(request).Response;
        }

        private bool CreateCanGuestRateHostRequest(string hostId, string guestId)
        {
            CanGuestRateHostRequest request = new CanGuestRateHostRequest() { HostId = hostId, GuestId = guestId };
            var channel = GrpcChannel.ForAddress("http://reservation_service:8080");
            var client = new InternalReservationServiceRPC.InternalReservationServiceRPCClient(channel);
            return client.CanGuestRateHost(request).Response;
        }
        
        public async Task RateHost(RateHostRequest request)
        {
            if (!CreateCanGuestRateHostRequest(request.HostId, request.GuestId)) throw new Exception("guest cannot rate this host!");
            Rating rating = new Rating() { Value = (int)request.Rating };
            await _guestRepository.Relate<Host, Rating>($"{{guestId :'{request.GuestId}'}}", $"{{hostId :'{request.HostId}'}}", rating, DateTime.Now, rating.Value);

            using var channel = GrpcChannel.ForAddress("http://localhost:8083");
            var client = new ReviewNotification.ReviewNotificationClient(channel);
            var reply = await client.RateHostAsync(
                            new RateHostNotificationRequest { HostId = request.HostId, Rating=request.Rating });
        }

        public async Task<GetRatingsForAccommodationResponse> GetRatingsForAccommodation(GetRatingsForAccommodationRequest request) 
        {
            string nodeQuery = "n: Accommodation";
            string whereQuery = $"WHERE n.accommodationId = '{request.AccommodationId}'";
            GetRatingsForAccommodationResponse response = await _guestRepository.GetAccommodationRatings(nodeQuery, whereQuery, "accommodationId");
            foreach (AccommodationRating rating in response.Ratings)
            {
                using var channel = GrpcChannel.ForAddress("http://user_service:8080");
                var client = new UserRatingRPC.UserRatingRPCClient(channel);
                var reply = await client.GetUserAsync(
                                  new GetUserRequest { Id = rating.UserId });
                if(reply.User != null)
                {
                    rating.UserId = reply.User.FirstName + ' ' + reply.User.LastName;
                }
            }
            return response;
        }

        public async Task<GetRatingsForHostResponse> GetRatingsForHost(GetRatingsForHostRequest request)
        {
            string nodeQuery = "n: Host";
            string whereQuery = $"WHERE n.hostId = '{request.HostId}'";
            GetRatingsForHostResponse response = await _guestRepository.GetHostRatings(nodeQuery, whereQuery, "accommodationId");
            foreach(HostRating rating in response.Ratings)
            {
                using var channel = GrpcChannel.ForAddress("http://user_service:8080");
                var client = new UserRatingRPC.UserRatingRPCClient(channel);
                var reply = await client.GetUserAsync(
                                  new GetUserRequest { Id = rating.UserId });
                if (reply.User != null)
                {
                    rating.UserId = reply.User.FirstName + ' ' + reply.User.LastName;
                }
            }
            return response;
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
