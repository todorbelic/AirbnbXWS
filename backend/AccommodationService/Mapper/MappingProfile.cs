
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
            CreateMap<Accommodation, AppAccommodation>();
           // CreateMap<AppAccommodation, Accommodation>();
        }
    }
}
