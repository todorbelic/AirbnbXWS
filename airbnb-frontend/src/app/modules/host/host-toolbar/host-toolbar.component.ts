import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-host-toolbar',
  templateUrl: './host-toolbar.component.html',
  styleUrls: ['./host-toolbar.component.css']
})
export class HostToolbarComponent {

  public constructor(private router: Router) {  }

  HomeClick(){
    this.router.navigate(['/host-home']);
  }

  ProfileClick(){
    this.router.navigate(['/host-profile']);
  }

  LogoutClick(){
    //ovo popraviti
    this.router.navigate(['']);

  }

  ReservationsClick(){
    this.router.navigate(['/host-reservations']);

  }

  NewAccommodationClick(){
    this.router.navigate(['/host-add-accommodation']);
  }
}
