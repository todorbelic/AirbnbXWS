using AccommodationService.Model;

namespace AccommodationService.Services
{
    public interface IAppAccommodationService
    {
        public List<Accommodation> Get();

        public Task <AppAccommodation> GetById(string id);

        Task AddAccommodation(AccommodationRequest dto);

        Task UpdateAccommodation(Accommodation dto);
        List<AccommodationSearch> SearchAccommodations(SearchAccommodationsRequest request);

        AccommodationForReservationView GetAccommodationForReservation(string accommodationId);
        IEnumerable<AccommodationForReservationView> getAccommodationsForReservations(IEnumerable<string> accommodationIds);
        
        Task<string> GetTypeOfResConfirmationForAccommodation(string accommodationId);
    }
}
