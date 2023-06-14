import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user';
import { LogInRequestData } from '../dto/logInRequestData'
import jwt_decode from 'jwt-decode';
import { Reservation } from '../model/reservation';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
//proveriti port
  apiHost: string = 'http://localhost:44329/'
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' })

  constructor(private http: HttpClient) { }


  getActiveForHost(id:any): Observable<any> {
    return this.http.get(this.apiHost + 'api/reservation/host' + id, { headers: this.headers })
  }

  getActiveForGuest(id:any): Observable<any> {
    return this.http.get(this.apiHost + 'api/reservation/guest' + id, { headers: this.headers })
  }

  getFutureForAccommodation(): Observable<any> {
    return this.http.get(this.apiHost + 'api/reservation/accommodation', { headers: this.headers })
  }

  manualReservation(reservation:Reservation): Observable<any> {
    return this.http.post(this.apiHost + 'api/reservation/manual',reservation, { headers: this.headers })
  }

  automaticReservation(reservation:Reservation): Observable<any> {
    return this.http.post(this.apiHost + 'api/reservation/automatic',reservation, { headers: this.headers })
  }

  approveReservation(reservation:Reservation): Observable<any> {
    return this.http.post(this.apiHost + 'api/reservation/approve',reservation, { headers: this.headers })
  }
  
  cancelReservation(reservation:Reservation): Observable<any> {
    return this.http.post(this.apiHost + 'api/reservation/cancel',reservation, { headers: this.headers })
  }
  

  
}