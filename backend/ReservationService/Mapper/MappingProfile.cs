using AutoMapper;
using ReservationService.DTO;
using ReservationService.Model;

namespace ReservationService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SendReservationRequestRequest, Reservation>();
            CreateMap<Reservation, ReservationViewDTO>();
        }
    }
}
