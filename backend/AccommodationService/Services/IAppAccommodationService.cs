using AccommodationService.Model;

namespace AccommodationService.Services
{
    public interface IAppAccommodationService
    {
        public List<Accommodation> Get();

        public Task <AppAccommodation> GetById(string id);

        Task AddAccommodation(AccommodationRequest dto);

        Task UpdateAccommodation(Accommodation dto);
    }
}
