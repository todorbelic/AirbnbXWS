import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
;


@Injectable({
  providedIn: 'root'
})
export class ReviewService {
//proveriti port
  apiHost: string = 'http://localhost:8000/'
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' })

  constructor(private http: HttpClient) { }

  rateAccommodation(guestId: any, accommodationId:any, rating:number): Observable<any> {
    return this.http.post(this.apiHost + 'review/accommodation', {guestId: guestId, accommodationId: accommodationId, rating: rating},{ headers: this.headers})
  }

  rateHost(guestId: any, hostId:any, rating:number): Observable<any> {
    return this.http.post(this.apiHost + 'review/host', {guestId: guestId, hostId: hostId, rating: rating}, { headers: this.headers })
  }

  getAccommodationRatings(id: any) : Observable<any> {
    return this.http.get(this.apiHost + 'review/accommodation/' +id, { headers: this.headers })
  }

  getHostRatings(id: any) : Observable<any> {
    return this.http.get(this.apiHost + 'review/host/' +id, { headers: this.headers })
  }

  deleteAccommodationRating(userId: any, accId: any) {
    return this.http.post(this.apiHost + 'review/accommodation/delete' ,{guestId:userId, accommodationId: accId}, { headers: this.headers })
  }

  deleteHostRating(userId: any, accId: any) {
    return this.http.post(this.apiHost + 'review/host/delete' ,{guestId:userId, accommodationId: accId}, { headers: this.headers })
  }
}