import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user';
import { LogInRequestData } from '../dto/logInRequestData'
import jwt_decode from 'jwt-decode';
import { UserProfileData } from '../model/userProfileData';

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
    return this.http.post(this.apiHost + 'user/login', {credentials: credentials}, { headers: this.headers })
  }

  getCurrentUser() : Observable<any> {
    return this.http.get(this.apiHost + 'user/' + localStorage.getItem('userId'), {headers: this.headers});
  }

  updateUser(user: UserProfileData) : Observable<any> {
    return this.http.post(this.apiHost + 'user', {user: user}, {headers: this.headers});
  }

  public setSession(token:any) {
     const decoded: any = jwt_decode(token);
     //ubaciti info u local storage
     localStorage.setItem('currentUser', token);
     localStorage.setItem('role', decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
     localStorage.setItem('userId', decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid']);
     localStorage.setItem("expires_at", decoded['exp']);
   }
 
   logout() {
    localStorage.clear();
   }

   getRole() : string | null {
    return localStorage.getItem('role');
   }
 
}