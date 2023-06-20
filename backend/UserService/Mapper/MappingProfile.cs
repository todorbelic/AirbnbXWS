using AutoMapper;
using UserService.Model;

namespace UserService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationUser, AppUser>().ReverseMap();
            CreateMap<Address, UserAddress>().ReverseMap();
            CreateMap<User, AppUser>().ReverseMap();
            CreateMap<User, UserRating>().ReverseMap();
            CreateMap<UserAddress, UserRatingAddress>();
        }
    }
}
