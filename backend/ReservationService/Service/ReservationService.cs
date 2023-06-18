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
        private readonly ILogger<IReservationService> _logger;
        public ReservationService(IMapper mapper, IRepository<Reservation> repository, ILogger<IReservationService> logger) {
            _repository= repository;
            _mapper= mapper;
            _logger= logger;
        }

        public async Task<bool> AcceptReservation(string reservationId)
        {
            EnteredMethodLog("AcceptReservation");
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            if (reservation == null) return false;
            reservation.Status = "ACTIVE";
            await DenyAllReservationsThatOverlap(reservation);
            await _repository.ReplaceOneAsync(reservation);
            _logger.Log(LogLevel.Information, "Finished accepting reservation");
            return true;
        }

        public async Task<ReservationViewDTO> GetById(string reservationId)
        {
            EnteredMethodLog("GetById");
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            //ovde moram ili promeniti da se ne vidi adresa al moram request get accommodation details i host name
            return _mapper.Map<ReservationViewDTO>(reservation);
        }

        public async Task<bool> SendReservationRequest(SendReservationRequestRequest dto)
        {
            EnteredMethodLog("SendReservationRequest");
            IEnumerable<Reservation> activeInAccommodation = _repository
                .FilterBy(r => r.AccommodationId.Equals(dto.Request.AccommodationId)
                && r.Status == "ACTIVE");
            Reservation dtoReservation = _mapper.Map<Reservation>(dto);
            foreach (Reservation reservation in activeInAccommodation)
            {
                if (Overlaps(dtoReservation.StartDate, dtoReservation.EndDate, reservation.StartDate, reservation.EndDate)) return false;
            }
            //ovde provera za smestaj ako automatski prihvata rezervacije odmah status = active, ako ne, status = pending
            if (true) dtoReservation.Status = "ACTIVE";
            //else dtoReservation.Status = "PENDING";
            await _repository.InsertOneAsync(dtoReservation);
            return true;
        }

        private async Task DenyAllReservationsThatOverlap(Reservation reservation)
        {
            EnteredMethodLog("DenyAllReservationsThatOverlap");
            IEnumerable<Reservation> pendingInAccommodation = _repository.FilterBy(r => r.AccommodationId.Equals(reservation.AccommodationId)
            && r.Status == "PENDING");
            foreach(Reservation res in pendingInAccommodation)
            {
                if(Overlaps(reservation.StartDate, reservation.EndDate, res.StartDate, res.EndDate))
                {
                    Reservation newReservation = res;
                    newReservation.Status = "DENIED";
                    await _repository.ReplaceOneAsync(reservation);
                }
            }
            
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

        public int GetCancellationNumberForGuest(string guestId)
        {
            return _repository.FilterBy(r => r.GuestId == guestId
            && r.Status == "CANCELLED").Count();
        }

        public bool CanGuestRateHost(string guestId, string hostId)
        {
            EnteredMethodLog("CanGuestRateHost");
            //ovo mozda promeniti jer pise da moze ako je imao bar jednu rezervaciju koju nije otkazao, znaci sve osim cancelled?
            IEnumerable<Reservation> guestHistoryWithHost = _repository.FilterBy(r => r.HostId == hostId && r.GuestId == guestId
            && r.Status == "FINISHED");
            if(guestHistoryWithHost.Any()) return true;
            return false;
        }

        public bool CanGuestRateAccommodation(string guestId, string accommodationId)
        {
            EnteredMethodLog("CanGuestRateAccommodation");
            IEnumerable<Reservation> guestHistoryWithAccommodation = _repository.FilterBy(r => r.AccommodationId == accommodationId 
            && r.GuestId == guestId
            && r.Status == "FINISHED");
            if(guestHistoryWithAccommodation.Any()) return true;
            return false;
        }

        public bool IsAccommodationAvailableForDateRange(IsAccommodationAvailableForDateRangeRequest request)
        {
            EnteredMethodLog("IsAccommodationAvailableForDateRange");
            DateTime start = DateTime.Parse(request.TimeFrame.StartDate);
            DateTime end = DateTime.Parse(request.TimeFrame.EndDate);
            IEnumerable<Reservation> reservationsForAccommodation = _repository.FilterBy(r => r.AccommodationId == request.AccommodationId 
            && r.Status == "ACTIVE" && DateTime.Compare(r.StartDate, DateTime.Now)>=0);
            if (!reservationsForAccommodation.Any()) return true;
            foreach(Reservation res in reservationsForAccommodation)
            {
                if (Overlaps(res.StartDate, res.EndDate, start, end)) return false;
            }
            return true;
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationViewDTO> GetActiveForHost(string hostId)
        {
            EnteredMethodLog("GetActiveForHost");
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.HostId == hostId && r.Status == "ACTIVE");
            return _mapper.Map<IEnumerable<ReservationViewDTO>>(reservations);
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationViewDTO> GetActiveForGuest(string guestId)
        {
            EnteredMethodLog("GetActiveForGuest");
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.GuestId == guestId && r.Status == "ACTIVE");
            return _mapper.Map<IEnumerable<ReservationViewDTO>>(reservations);
        }

        public async Task<bool> DeleteReservationRequest(string requestId)
        {
            EnteredMethodLog("DeleteReservationRequest");
            Reservation reservation = await _repository.FindByIdAsync(requestId);
            if (reservation == null) return false;
            if (reservation.Status != "PENDING") return false;
            reservation.Status = "DELETED";
            await _repository.ReplaceOneAsync(reservation);
            return true;
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationViewDTO> GetReservationRequestsForGuest(string guestId)
        {
            EnteredMethodLog("GetReservationRequestsForGuest");
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.GuestId == guestId && r.Status == "PENDING");
            return _mapper.Map<IEnumerable<ReservationViewDTO>>(reservations);
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationViewDTO> GetReservationRequestsForHost(string hostId)
        {
            EnteredMethodLog("GetReservationRequestsForHost");
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.HostId == hostId && r.Status == "PENDING");
            return _mapper.Map<IEnumerable<ReservationViewDTO>>(reservations);
        }

        public async Task<bool> DenyReservationRequest(string requestId)
        {
            EnteredMethodLog("DenyReservationRequest");
            Reservation reservation = await _repository.FindByIdAsync(requestId);
            if (reservation == null) return false;
            reservation.Status = "DENIED";
            await _repository.ReplaceOneAsync(reservation);
            return true;
        }

        //moze da otkaze samo ako je bar dan pre pocetka rezervacije
        public async Task<bool> CancelReservation(string reservationId)
        {
            EnteredMethodLog("CancelReservation");
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            if (reservation == null) return false;
            if (reservation.Status != "ACTIVE" ||
                DateTime.Compare(reservation.StartDate, DateTime.Now.AddDays(1)) > 0) return false;
            reservation.Status = "CANCELLED";
            await _repository.ReplaceOneAsync(reservation);
            return true;
        }

        public bool IsHostNoteworthyReservationWise(string hostId)
        {
            EnteredMethodLog("IsHostNoteworthyReservationWise");
            int totalReservationCount = GetTotalCountForHost(hostId);
            ///za istaknutog hosta, vraca procenat - ako 56% vrati 56
            int cancellationRate = GetCancellationCountForHost(hostId)/totalReservationCount;
            int totalDays = GetTotalReservedDaysForHost(hostId);
            return totalReservationCount>5 && cancellationRate > 5 && totalDays > 05;
        }

       
       private int GetTotalReservedDaysForHost(string hostId)
        {
            EnteredMethodLog("GetTotalReservedDaysForHost");
            //da li da ubrojim i aktivne?
            int days = 0;
            IEnumerable<Reservation> allFinished = _repository.FilterBy(r => r.HostId == hostId && r.Status == "FINISHED");
            foreach(Reservation reservation in allFinished)
            {
                days = days + reservation.EndDate.Day - reservation.StartDate.Day; 
            }
            return days;
        }

        private int GetCancellationCountForHost(string hostId)
        {
            EnteredMethodLog("GetCancellationCountForHost");
            return _repository.FilterBy(r => r.HostId == hostId && r.Status == "CANCELLED").Count();
        }
        private int GetTotalCountForHost(string hostId)
        {
            EnteredMethodLog("GetTotalCountForHost");
            return _repository.FilterBy(r => r.HostId == hostId && r.Status != "DENIED" && r.Status != "DELETED").Count();
        }

        private void EnteredMethodLog(string method)
        {
            _logger.Log(LogLevel.Information, "Entered the " + method + " method in Reservation service.");
        }

    }
}
