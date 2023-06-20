using AccommodationService.DTO;
using AccommodationService.Exceptions;
using AccommodationService.Model;
using AccommodationService.Repository;
using AutoMapper;
using Grpc.Net.Client;
using System.Linq;
using System.Linq.Expressions;

namespace AccommodationService.Services
{
    public class AppAccommodationService : IAppAccommodationService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<AppAccommodation> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<IAppAccommodationService> _logger;

        public AppAccommodationService(IConfiguration configuration, IRepository<AppAccommodation> repository, IMapper mapper, ILogger<IAppAccommodationService> logger)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
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

        public async Task UpdateAccomDetails(UpdateAccomDetailsDTO dto)
        {
            if (!CreateIsAccommodationAvailableForDateRangeRequest(dto.AccomId)) throw new AccommodationNotAvailableException();
            AppAccommodation accommodation = await GetById(dto.AccomId);
            if (accommodation == null)
            {
                throw new AccommodationNotFoundException();
            }
            accommodation.SpecialPrice = dto.NewPrice;
            accommodation.Occasions = dto.Occasions;
            await _repository.ReplaceOneAsync(accommodation);
        }

        private bool CreateIsAccommodationAvailableForDateRangeRequest(string accommodationId)
        {
            DateTime current = DateTime.Now;
            ReservationTimeFrameProto timeframe = new ReservationTimeFrameProto() { StartDate= current.ToString(), EndDate = current.AddYears(10).ToString()};
            IsAccommodationAvailableForDateRangeRequest request = new IsAccommodationAvailableForDateRangeRequest() { AccommodationId = accommodationId, TimeFrame = timeframe };
            var channel = GrpcChannel.ForAddress("http://reservation_service:8080");
            var client = new InternalReservationServiceRPC.InternalReservationServiceRPCClient(channel);
            return client.IsAccommodationAvailableForDateRange(request).IsAvailable;
        }

        public List<AccommodationSearch> SearchAccommodations(SearchAccommodationsRequest request)
        {
            Expression<Func<AppAccommodation, bool>> filterExpression = a => a.MinGuests <= request.NumberOfGuests
                                                                       && a.MaxGuests >= request.NumberOfGuests
                                                                       && a.Address.Country.ToLower().Contains(request.Country.ToLower())
                                                                       && a.Address.City.ToLower().Contains(request.City.ToLower())
                                                                       && a.Address.StreetAddress.ToLower().Contains(request.StreetAddress.ToLower());
            
            List<AppAccommodation> accommodations = _repository.FilterBy(filterExpression).ToList();
            List<AccommodationSearch> accommodationsSearched = _mapper.Map<List<AccommodationSearch>>(accommodations);
            accommodationsSearched.ForEach(accommodations => accommodations.PriceTotal = accommodations.BasePrice * request.NumberOfGuests);
            return accommodationsSearched;
        }

        public AccommodationForReservationView GetAccommodationForReservation(string accommodationId)
        {
            
            AppAccommodation accommodation = _repository.FindById(accommodationId);
            if (accommodation == null) throw new AccommodationNotFoundException();
            return _mapper.Map<AccommodationForReservationView>(accommodation);
        }

        public IEnumerable<AccommodationForReservationView> getAccommodationsForReservations(IEnumerable<string> accommodationIds)
        {
            List<AppAccommodation> accommodations = _repository.AsQueryable().ToList();
            if (accommodations.Count == 0) return new List<AccommodationForReservationView>();
            IEnumerable<AppAccommodation> accommodationViewsToReturn = accommodations.Where(accommodation => accommodationIds.Any(id => id.Equals(accommodation.Id.ToString())));
            return _mapper.Map<IEnumerable<AccommodationForReservationView>>(accommodationViewsToReturn);
        }

        public async Task<string> GetTypeOfResConfirmationForAccommodation(string accommodationId)
        {
            AppAccommodation accommodation = await _repository.FindByIdAsync(accommodationId);
            if (accommodation == null) throw new AccommodationNotFoundException();
            return accommodation.TypeOfReservationConfirmation;
        }
    }
}
