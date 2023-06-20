import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user';
import { LogInRequestData } from '../dto/logInRequestData'
import jwt_decode from 'jwt-decode';
import { Reservation } from '../model/reservation';
import { AcceptReservationRequest } from '../dto/accept-reservation-request';
import { ReservationRequest } from '../dto/reservation-request';
import { SendReservationRequest } from '../dto/send-reservation-request';
import { DeleteReservationRequestData } from '../dto/delete-reservation-request';
import { CancelReservationRequest } from '../dto/cancel-reservation-request';
import { DenyReservationRequest } from '../dto/deny-reservation-request';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
//proveriti port
  apiHost: string = 'http://localhost:8000/reservation'
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' })

  constructor(private http: HttpClient) { }


  getRequestsForHost(id:any): Observable<any> {
    return this.http.get(this.apiHost + '/host/requests?hostId=' + id, { headers: this.headers })
  }

  getAllForGuest(id:any): Observable<any> {
    return this.http.get(this.apiHost + '/guest?guestId=' + id, { headers: this.headers })
  }

  getCancellationNumberForGuest(id : any) : Observable<any>{
    return this.http.get(this.apiHost + '/guest/cancellation-number/' + id, {headers : this.headers})
  }

  acceptReservation(reservation: AcceptReservationRequest): Observable<any> {
    return this.http.post(this.apiHost + '/accept-request',reservation, { headers: this.headers })
  }
  
  sendReservationRequest(reservationRequest : SendReservationRequest) : Observable<any>{
    return this.http.post(this.apiHost + '/send-request', reservationRequest, {headers : this.headers})
  }

  deleteReservationRequest(request : DeleteReservationRequestData) : Observable<any>{
    return this.http.post(this.apiHost + '/delete-request' , request, { headers : this.headers})
  }

  cancelReservation(reservationId : CancelReservationRequest): Observable<any> {
    return this.http.post(this.apiHost + '/cancel?reservationId='+reservationId, { headers: this.headers })
  }

  denyReservation(reservationId : DenyReservationRequest): Observable<any> {
    return this.http.post(this.apiHost + '/deny',reservationId, { headers: this.headers })
  }





  
}