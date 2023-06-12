import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-guest-toolbar',
  templateUrl: './guest-toolbar.component.html',
  styleUrls: ['./guest-toolbar.component.css']
})
export class GuestToolbarComponent{
 
 
  constructor(private router: Router) {  }

  HomeClick(){
    this.router.navigate(['/guest-home']);
  }

  ProfileClick(){
    this.router.navigate(['/guest-profile']);
  }

  LogoutClick(){
    //ovo popraviti
    this.router.navigate(['']);

  }

  ReservationsClick(){
    this.router.navigate(['/guest-reservations']);

  }
}
