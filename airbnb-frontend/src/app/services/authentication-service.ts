import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user';
import { LogInRequestData } from '../dto/logInRequestData'
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
//proveriti port
  apiHost: string = 'http://localhost:8000/'
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' })

  constructor(private http: HttpClient) { }

  registerUser(user: User): Observable<any> {
    return this.http.post(this.apiHost + 'api/user/register', {user: user}, { headers: this.headers})
  }

  logInUser(credentials:LogInRequestData): Observable<any> {
    return this.http.post(this.apiHost + 'api/user/login', credentials, { headers: this.headers })
  }

  public setSession(token:any) {
     const decoded: any = jwt_decode(token);
     //ubaciti info u local storage
     localStorage.setItem('currentUser', token);
   }
 
   logout() {
    localStorage.clear();
   }
 
}