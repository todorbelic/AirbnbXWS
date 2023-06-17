using Amazon.Runtime.Internal;
using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Hosting;
using ReservationService.DTO;
using ReservationService.Enums;
using ReservationService.Model;
using ReservationService.Repository;
using System.Globalization;

namespace ReservationService.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        public ReservationService(IMapper mapper, IRepository<Reservation> repository) {
            _repository= repository;
            _mapper= mapper;
        }

        public async Task<bool> AcceptReservation(string reservationId)
        {
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            if (reservation == null) return false;
            reservation.Status = Enums.ReservationStatus.ACTIVE;
            await DenyAllReservationsThatOverlap(reservation);
            await _repository.ReplaceOneAsync(reservation);


            using var channel = GrpcChannel.ForAddress("http://localhost:8083");
            var client = new ReservationNotification.ReservationNotificationClient(channel);
            var reply = await client.ReservationAcceptedAsync(
                            new ReservationAcceptedRequest { AccomId=reservation.AccommodationId, GuestId=reservation.GuestId, HostId=reservation.HostId});
            
            return true;
        }

        public async Task<ReservationViewDTO> GetById(string reservationId)
        {
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            //ovde moram ili promeniti da se ne vidi adresa al moram request get accommodation details i host name
            return _mapper.Map<ReservationViewDTO>(reservation);
        }

        public async Task<bool> SendReservationRequest(SendReservationRequestRequest dto)
        {
            IEnumerable<Reservation> activeInAccommodation = _repository.FilterBy(r => r.AccommodationId == dto.Request.AccommodationId
            && r.Status == Enums.ReservationStatus.ACTIVE);
            Reservation dtoReservation = _mapper.Map<Reservation>(dto);
            foreach(Reservation reservation in activeInAccommodation)
            {
                if (Overlaps(dtoReservation.StartDate, dtoReservation.EndDate, reservation.StartDate, reservation.EndDate)) return false;
            }
            //ovde provera za smestaj ako automatski prihvata rezervacije odmah status = active, ako ne, status = pending
            if (true) dtoReservation.Status = ReservationStatus.ACTIVE;
            else dtoReservation.Status = ReservationStatus.PENDING;
            await _repository.InsertOneAsync(dtoReservation);
            return true;
        }

        private async Task DenyAllReservationsThatOverlap(Reservation reservation)
        {
            IEnumerable<Reservation> pendingInAccommodation = _repository.FilterBy(r => r.AccommodationId == reservation.AccommodationId
            && r.Status == Enums.ReservationStatus.PENDING);
            foreach(Reservation res in pendingInAccommodation)
            {
                if(Overlaps(reservation.StartDate, reservation.EndDate, res.StartDate, res.EndDate))
                {
                    Reservation newReservation = res;
                    newReservation.Status = ReservationStatus.DENIED;
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
            && r.Status == ReservationStatus.CANCELLED).Count();
        }

        public bool CanGuestRateHost(string guestId, string hostId)
        {
            //ovo mozda promeniti jer pise da moze ako je imao bar jednu rezervaciju koju nije otkazao, znaci sve osim cancelled?
            IEnumerable<Reservation> guestHistoryWithHost = _repository.FilterBy(r => r.HostId == hostId && r.GuestId == guestId
            && r.Status == ReservationStatus.FINISHED);
            if(guestHistoryWithHost.Any()) return true;
            return false;
        }

        public bool CanGuestRateAccommodation(string guestId, string accommodationId)
        {
            IEnumerable<Reservation> guestHistoryWithAccommodation = _repository.FilterBy(r => r.AccommodationId == accommodationId 
            && r.GuestId == guestId
            && r.Status == ReservationStatus.FINISHED);
            if(guestHistoryWithAccommodation.Any()) return true;
            return false;
        }

        public bool IsAccommodationAvailableForDateRange(IsAccommodationAvailableForDateRangeRequest request)
        {
            DateTime start = DateTime.Parse(request.TimeFrame.StartDate);
            DateTime end = DateTime.Parse(request.TimeFrame.EndDate);
            IEnumerable<Reservation> reservationsForAccommodation = _repository.FilterBy(r => r.AccommodationId == request.AccommodationId 
            && r.Status == ReservationStatus.ACTIVE && DateTime.Compare(r.StartDate, DateTime.Now)>=0);
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
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.HostId == hostId && r.Status == ReservationStatus.ACTIVE);
            return _mapper.Map<IEnumerable<ReservationViewDTO>>(reservations);
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationViewDTO> GetActiveForGuest(string guestId)
        {
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.GuestId == guestId && r.Status == ReservationStatus.ACTIVE);
            return _mapper.Map<IEnumerable<ReservationViewDTO>>(reservations);
        }

        public async Task<bool> DeleteReservationRequest(string requestId)
        {
            Reservation reservation = await _repository.FindByIdAsync(requestId);
            if (reservation == null) return false;
            if (reservation.Status != ReservationStatus.PENDING) return false;
            reservation.Status = ReservationStatus.DELETED;
            await _repository.ReplaceOneAsync(reservation);
            return true;
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationViewDTO> GetReservationRequestsForGuest(string guestId)
        {
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.GuestId == guestId && r.Status == ReservationStatus.PENDING);
            return _mapper.Map<IEnumerable<ReservationViewDTO>>(reservations);
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationViewDTO> GetReservationRequestsForHost(string hostId)
        {
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.HostId == hostId && r.Status == ReservationStatus.PENDING);
            return _mapper.Map<IEnumerable<ReservationViewDTO>>(reservations);
        }

        public async Task<bool> DenyReservationRequest(string requestId)
        {
            Reservation reservation = await _repository.FindByIdAsync(requestId);
            if (reservation == null) return false;
            reservation.Status = Enums.ReservationStatus.DENIED;
            await _repository.ReplaceOneAsync(reservation);
            return true;
        }

        //moze da otkaze samo ako je bar dan pre pocetka rezervacije
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

        public bool IsHostNoteworthyReservationWise(string hostId)
        {
            int totalReservationCount = GetTotalCountForHost(hostId);
            ///za istaknutog hosta, vraca procenat - ako 56% vrati 56
            int cancellationRate = GetCancellationCountForHost(hostId)/totalReservationCount;
            int totalDays = GetTotalReservedDaysForHost(hostId);
            return totalReservationCount>5 && cancellationRate > 5 && totalDays > 05;
        }

       
       private int GetTotalReservedDaysForHost(string hostId)
        {
            //da li da ubrojim i aktivne?
            int days = 0;
            IEnumerable<Reservation> allFinished = _repository.FilterBy(r => r.HostId == hostId && r.Status == ReservationStatus.FINISHED );
            foreach(Reservation reservation in allFinished)
            {
                days = days + reservation.EndDate.Day - reservation.StartDate.Day; 
            }
            return days;
        }

        private int GetCancellationCountForHost(string hostId)
        {
            return _repository.FilterBy(r => r.HostId == hostId && r.Status == ReservationStatus.CANCELLED).Count();
        }
        private int GetTotalCountForHost(string hostId)
        {
            return _repository.FilterBy(r => r.HostId == hostId && r.Status != ReservationStatus.DENIED && r.Status != ReservationStatus.DELETED).Count();
        }

    }
}
