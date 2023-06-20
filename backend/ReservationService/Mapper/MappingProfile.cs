using AutoMapper;
using ReservationService.Model;

namespace ReservationService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SendReservationRequestRequest, Reservation>()
                .ForMember(
                dest => dest.HostId, opt => opt.MapFrom(src => src.Request.HostId))
                .ForMember(
                 dest => dest.AccommodationId, opt => opt.MapFrom(src => src.Request.AccommodationId))
                .ForMember(
                 dest => dest.GuestId, opt => opt.MapFrom(src => src.Request.GuestId))
                .ForMember(
                 dest => dest.StartDate, opt => opt.MapFrom(src => src.Request.StartDate))
                .ForMember(
                 dest => dest.EndDate, opt => opt.MapFrom(src => src.Request.EndDate))
                .ForMember(
                 dest => dest.GuestCount, opt => opt.MapFrom(src => src.Request.GuestCount));
            CreateMap<Reservation, ReservationView>();
            
        }
    }
}
