import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user';
import { LogInRequestData } from '../dto/logInRequestData'
import jwt_decode from 'jwt-decode';
import { CreateAccommodationRequest } from '../model/create-accommodation-request';

@Injectable({
  providedIn: 'root'
})
export class AccomService {
//proveriti port
  apiHost: string = 'http://localhost:8000/'
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' })

  constructor(private http: HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get(this.apiHost + 'api/accommodation',{ headers: this.headers})
  }

  getForHost(id:any): Observable<any> {
    return this.http.get(this.apiHost + 'api/accommodation/host' + id, { headers: this.headers })
  }

  createNewAccommodation(request: CreateAccommodationRequest): Observable<any>{
    return this.http.post(this.apiHost + 'accommodation/new', request, {headers: this.headers} )
  }
}