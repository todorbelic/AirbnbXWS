using Amazon.Runtime.Internal;
using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Hosting;
﻿using AutoMapper;
using Grpc.Net.Client;
using ReservationService.DTO;
using ReservationService.Model;
using ReservationService.Repository;
using Grpc.Net.Client;
using System.Data;

namespace ReservationService.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<IReservationService> _logger;
        private readonly ReservationAccommodationRPC.ReservationAccommodationRPCClient _client;
        public ReservationService(IMapper mapper, IRepository<Reservation> repository, ILogger<IReservationService> logger) {
            _repository= repository;
            _mapper= mapper;
            _logger= logger;
             var channel = GrpcChannel.ForAddress("http://localhost:8080");
             _client = new ReservationAccommodationRPC.ReservationAccommodationRPCClient(channel);
        }

        public async Task<bool> AcceptReservation(string reservationId)
        {
            EnteredMethodLog("AcceptReservation");
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            if (reservation == null) return false;
            reservation.Status = "ACTIVE";
            await DenyAllReservationsThatOverlap(reservation);
            await _repository.ReplaceOneAsync(reservation);


            using var channel = GrpcChannel.ForAddress("http://localhost:8083");
            var client = new ReservationNotification.ReservationNotificationClient(channel);
            var reply = await client.ReservationAcceptedAsync(
                            new ReservationAcceptedRequest { AccomId=reservation.AccommodationId, GuestId=reservation.GuestId, HostId=reservation.HostId});
            
            _logger.Log(LogLevel.Information, "Finished accepting reservation");
            return true;
        }

        public async Task<ReservationView> GetById(string reservationId)
        {
            EnteredMethodLog("GetById");
            Reservation reservation = await _repository.FindByIdAsync(reservationId);
            ReservationView view = _mapper.Map<ReservationView>(reservation);
            GetAccommodationViewForReservationResponse response = createGetAccommodationForReservationRequest(reservation.AccommodationId);
            view.AccommodationName = response.Accommodation.Name;
            view.Address = response.Accommodation.Address;
            view.GuestName = "";
            view.HostName = "";
            return view;
        }

        private GetAccommodationViewForReservationResponse createGetAccommodationForReservationRequest(string accommodationId)
        {
            GetAccommodationViewForReservationRequest request = new GetAccommodationViewForReservationRequest() { Id = accommodationId };
            return _client.GetAccommodationViewForReservation(request);
        }

        private GetAccommodationViewForMultipleReservationsResponse createGetAccommodationsForReservationsRequest(List<string> accommodationIds)
        {
            GetAccommodationViewForMultipleReservationsRequest request = new GetAccommodationViewForMultipleReservationsRequest();
            request.Ids.AddRange(accommodationIds);
            _logger.Log(LogLevel.Information, request.Ids.ToArray().ToString());
            return _client.GetAccommodationViewForMultipleReservations(request);
        }

        public async Task<bool> SendReservationRequest(SendReservationRequestRequest dto)
        {
            EnteredMethodLog("SendReservationRequest");
            IEnumerable<Reservation> activeInAccommodation = _repository
                .FilterBy(r => r.AccommodationId.Equals(dto.Request.AccommodationId)
                && r.Status.Equals("ACTIVE"));
            Reservation dtoReservation = _mapper.Map<Reservation>(dto);
            foreach (Reservation reservation in activeInAccommodation)
            {
                if (Overlaps(dtoReservation.StartDate, dtoReservation.EndDate, reservation.StartDate, reservation.EndDate)) return false;
            }
            //ovde provera za smestaj ako automatski prihvata rezervacije odmah status = active, ako ne, status = pending
            //if (true) dtoReservation.Status = "ACTIVE";
            dtoReservation.Status = "PENDING";
            await _repository.InsertOneAsync(dtoReservation);
            _logger.Log(LogLevel.Information, "Finished sending reservation request");
            return true;
        }

        private async Task DenyAllReservationsThatOverlap(Reservation reservation)
        {
            EnteredMethodLog("DenyAllReservationsThatOverlap");
            IEnumerable<Reservation> pendingInAccommodation = _repository
                .FilterBy(r => r.AccommodationId.Equals(reservation.AccommodationId)
                && r.Status.Equals("PENDING") 
                && !r.Id.Equals(reservation.Id));
            foreach(Reservation res in pendingInAccommodation)
            {
                if(Overlaps(reservation.StartDate, reservation.EndDate, res.StartDate, res.EndDate))
                {
                    Reservation newReservation = res;
                    newReservation.Status = "DENIED";
                    await _repository.ReplaceOneAsync(newReservation);
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

        //ovo ne radi, guestId dodje prazan????/
        public int GetCancellationNumberForGuest(string guestId)
        {
            _logger.Log(LogLevel.Information, guestId);
            EnteredMethodLog("GetCancellationNumberForGuest");
            var res = _repository.FilterBy(r => r.GuestId.Equals(guestId)
            && r.Status.Equals("CANCELLED")).Count();
            _logger.LogInformation(res.ToString());
            return res;
        }

        public bool CanGuestRateHost(string guestId, string hostId)
        {
            EnteredMethodLog("CanGuestRateHost");
            //ovo mozda promeniti jer pise da moze ako je imao bar jednu rezervaciju koju nije otkazao, znaci sve osim cancelled?
            IEnumerable<Reservation> guestHistoryWithHost = _repository.FilterBy(r => r.HostId.Equals(hostId) && r.GuestId.Equals(guestId)
            && r.Status.Equals("FINISHED"));
            if(guestHistoryWithHost.Any()) return true;
            return false;
        }

        public bool CanGuestRateAccommodation(string guestId, string accommodationId)
        {
            EnteredMethodLog("CanGuestRateAccommodation");
            IEnumerable<Reservation> guestHistoryWithAccommodation = _repository.FilterBy(r => r.AccommodationId.Equals(accommodationId) 
            && r.GuestId.Equals(guestId)
            && r.Status.Equals("FINISHED"));
            if(guestHistoryWithAccommodation.Any()) return true;
            return false;
        }


        public bool IsAccommodationAvailableForDateRange(IsAccommodationAvailableForDateRangeRequest request)
        {
            EnteredMethodLog("IsAccommodationAvailableForDateRange");
            DateTime start = DateTime.Parse(request.TimeFrame.StartDate);
            DateTime end = DateTime.Parse(request.TimeFrame.EndDate);
            IEnumerable<Reservation> reservationsForAccommodation = _repository.FilterBy(r => r.AccommodationId.Equals(request.AccommodationId) 
            && r.Status.Equals("ACTIVE") && DateTime.Compare(r.StartDate, DateTime.Now)>=0);
            if (!reservationsForAccommodation.Any()) return true;
            foreach(Reservation res in reservationsForAccommodation)
            {
                if (Overlaps(res.StartDate, res.EndDate, start, end)) return false;
            }
            return true;
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationView> GetActiveForHost(string hostId)
        {
            EnteredMethodLog("GetActiveForHost");
            _logger.Log(LogLevel.Information, hostId);
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.HostId.Equals(hostId) && r.Status.Equals("ACTIVE"));
            if(reservations == null) return new List<ReservationView>();
            IEnumerable<ReservationView> views = _mapper.Map<IEnumerable<ReservationView>>(reservations);
            _logger.Log(LogLevel.Information, views.First().GuestCount.ToString() + " " + views.First().GuestName);
            GetAccommodationViewForMultipleReservationsResponse response = createGetAccommodationsForReservationsRequest(views.Select(c => c.ReservationId).ToList());
            _logger.Log(LogLevel.Information, response.Accommodations.Count().ToString());
            _logger.Log(LogLevel.Information, response.Accommodations.First().Name);
            views = _mapper.Map<IEnumerable<ReservationView>>(response);
            _logger.Log(LogLevel.Information, views.First().AccommodationName + " " + views.First().Address + " " + views.First().GuestCount);
            return views;
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        public IEnumerable<ReservationView> GetActiveForGuest(string guestId)
        {
            EnteredMethodLog("GetActiveForGuest");
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.GuestId.Equals(guestId) && r.Status.Equals("ACTIVE"));
            if (reservations == null) return new List<ReservationView>();
            return _mapper.Map<IEnumerable<ReservationView>>(reservations);
        }

        public async Task<bool> DeleteReservationRequest(string requestId)
        {
            EnteredMethodLog("DeleteReservationRequest");
            Reservation reservation = await _repository.FindByIdAsync(requestId);
            if (reservation == null) return false;
            if (!reservation.Status.Equals("PENDING")) return false;
            reservation.Status = "DELETED";
            await _repository.ReplaceOneAsync(reservation);
            return true;
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        //i proveriti sta sse desi ako nijedan ne zadovoljava uslov sta vrati
        public IEnumerable<ReservationView> GetReservationRequestsForGuest(string guestId)
        {
            EnteredMethodLog("GetReservationRequestsForGuest");
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.GuestId.Equals(guestId) && r.Status.Equals("PENDING"));
            return _mapper.Map<IEnumerable<ReservationView>>(reservations);
        }

        //ovde ce mi isto trebati get accommodation by id ili tako nesto
        //isto proveri za uslov kao i za get active oba
        public IEnumerable<ReservationView> GetReservationRequestsForHost(string hostId)
        {
            EnteredMethodLog("GetReservationRequestsForHost");
            IEnumerable<Reservation> reservations = _repository.FilterBy(r => r.HostId.Equals(hostId) && r.Status.Equals("PENDING"));
            return _mapper.Map<IEnumerable<ReservationView>>(reservations);
        }

        public async Task<bool> DenyReservationRequest(string requestId)
        {
            EnteredMethodLog("DenyReservationRequest");
            Reservation reservation = await _repository.FindByIdAsync(requestId);
            if (reservation == null) return false;
            if (!reservation.Status.Equals("PENDING")) return false;
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
            if (!reservation.Status.Equals("ACTIVE") ||
                DateTime.Compare(reservation.StartDate, DateTime.Now.AddDays(1)) < 0) return false;
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
            IEnumerable<Reservation> allFinished = _repository.FilterBy(r => r.HostId.Equals(hostId) && r.Status.Equals("FINISHED"));
            foreach(Reservation reservation in allFinished)
            {
                days = days + reservation.EndDate.Day - reservation.StartDate.Day; 
            }
            return days;
        }

        private int GetCancellationCountForHost(string hostId)
        {
            EnteredMethodLog("GetCancellationCountForHost");
            return _repository.FilterBy(r => r.HostId.Equals(hostId) && r.Status.Equals("CANCELLED")).Count();
        }
        private int GetTotalCountForHost(string hostId)
        {
            EnteredMethodLog("GetTotalCountForHost");
            return _repository.FilterBy(r => r.HostId.Equals(hostId) && (r.Status.Equals("PENDING") || r.Status.Equals("ACTIVE") || r.Status.Equals("FINISHED"))).Count();
        }

        private void EnteredMethodLog(string method)
        {
            _logger.Log(LogLevel.Information, "Entered the " + method + " method in Reservation service.");
        }

    }
}
