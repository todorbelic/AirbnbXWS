import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserProfileData } from '../dto/userProfileData';

@Injectable({
  providedIn: 'root'
})
export class UserService {
//proveriti port
  apiHost: string = 'http://localhost:44329/'
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' })

  constructor(private http: HttpClient) { }

  getUser(): Observable<any> {
    return this.http.get(this.apiHost + 'api/user',{ headers: this.headers})
  }

  editProfile(user: UserProfileData): Observable<any> {
    return this.http.get(this.apiHost + 'api/user', { headers: this.headers })
  }
}