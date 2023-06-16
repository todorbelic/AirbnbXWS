using AutoMapper;
using ReservationService.DTO;
using ReservationService.Model;

namespace ReservationService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReservationRequestDTO, Reservation>();
            CreateMap<Reservation, ReservationViewDTO>();
        }
    }
}
