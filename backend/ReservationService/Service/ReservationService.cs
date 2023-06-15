using AutoMapper;
using ReservationService.DTO;
using ReservationService.Model;
using ReservationService.Repository;

namespace ReservationService.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        public ReservationService(IRepository<Reservation> repository, IMapper mapper) {
            _repository= repository;
            _mapper= mapper;
        }

        public async Task<bool> CancelReservation(string reservationId)
        {
           Reservation reservation = await _repository.FindByIdAsync(reservationId);
            if (reservation == null) return false;
            if (reservation.Status != Enums.ReservationStatus.ACTIVE ||
                DateTime.Compare(reservation.StartDate, DateTime.Now.AddDays(1)) > 0) return false;
            reservation.Status = Enums.ReservationStatus.CANCELLED;
            await _repository.ReplaceOneAsync(reservation);
            return true;
        }

        public async Task<ReservationViewDTO> GetById(string reservationId)
        {
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            return _mapper.Map<ReservationViewDTO>(reservation);
        }

        public async Task<bool> SendReservationRequest(ReservationRequestDTO dto)
        {
            IEnumerable<Reservation> activeInAccommodation = _repository.FilterBy(r => r.AccommodationId == dto.AccommodationId
            && r.Status == Enums.ReservationStatus.ACTIVE);
            Reservation dtoReservation = _mapper.Map<Reservation>(dto);
            foreach(Reservation reservation in activeInAccommodation)
            {
                if (Overlaps(dtoReservation.StartDate, dtoReservation.EndDate, reservation.StartDate, reservation.EndDate)) return false;
            }
            //ovde provera za smestaj ako automatski prihvata rezervacije odmah status = active, ako ne, status = pending
            await _repository.InsertOneAsync(dtoReservation);
            return true;
        }

        public async Task ConfirmReservation(string reservationId)
        {
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            reservation.Status = Enums.ReservationStatus.ACTIVE;
            await _repository.ReplaceOneAsync(reservation);
        }


        private bool Overlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (DateTime.Compare(start1, start2) < 0)
            {
                return DateTime.Compare(end1, start2) > 0;
            }
            else if (DateTime.Compare(start2, start1) < 0)
            {
                return DateTime.Compare(end2, start1) > 0;
            }
            return true;

        }
    }
}
