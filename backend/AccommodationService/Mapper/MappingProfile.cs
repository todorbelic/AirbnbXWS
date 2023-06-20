using AccommodationService.Model;
using AutoMapper;

namespace AccommodationService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccommodationRequest, AppAccommodation>().ReverseMap();
            CreateMap<AppAddress, Address>().ReverseMap();
            CreateMap<Accommodation, AppAccommodation>().ReverseMap();
            CreateMap<AppAccommodation, AccommodationSearch>().ReverseMap();
            CreateMap<AppAccommodation, AccommodationForReservationView>()
                .ForMember(dest => dest.Address, opt => opt
                .MapFrom(src => src.Address.StreetAddress + ", " + src.Address.City + ", " + src.Address.Country));
        }
    }
}
