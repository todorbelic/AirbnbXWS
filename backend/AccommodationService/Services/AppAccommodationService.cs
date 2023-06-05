using AccommodationService.Exceptions;
using AccommodationService.Model;
using AccommodationService.Repository;
using AutoMapper;

namespace AccommodationService.Services
{
    public class AppAccommodationService : IAccommodationService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<AppAccommodation> _repository;
        private readonly IMapper _mapper;

        public AppAccommodationService(IConfiguration configuration, IRepository<AppAccommodation> repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAccommodation(AccommodationRequest dto)
        {
           AppAccommodation accommodation = _mapper.Map<AppAccommodation>(dto);
           await _repository.InsertOneAsync(accommodation);
        }

        public List<Accommodation> Get()
        {
            List<AppAccommodation> accommodations = _repository.AsQueryable().ToList();
            List<Accommodation> accommodationsGRPC = _mapper.Map<List<Accommodation>>(accommodations);
            return accommodationsGRPC;
        }

        public async Task<AppAccommodation> GetById(string id) => await _repository.FindByIdAsync(id);

        public async Task UpdateAccommodation(Accommodation dto)
        {
            AppAccommodation accommodation = await GetById(dto.Id);
            if(accommodation == null)
            {
                throw new AccommodationNotFoundException();
            }

            AppAccommodation accommodationToUpdate = _mapper.Map<AppAccommodation>(dto);
            await _repository.ReplaceOneAsync(accommodationToUpdate);
        }
    }
}
