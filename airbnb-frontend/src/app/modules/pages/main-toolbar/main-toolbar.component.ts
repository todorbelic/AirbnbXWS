import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/model/user';
import { AuthenticationService } from 'src/app/services/authentication-service';


@Component({
  selector: 'app-main-toolbar',
  templateUrl: './main-toolbar.component.html',
  styleUrls: ['./main-toolbar.component.css']
})
export class MainToolbarComponent {

  public user:User=new User();

  constructor(private router: Router, private authService:AuthenticationService) {  }

  ngOnInit():void {
   // this.user=this.authService.
   this.user.role='HOST'
  }


  HomeClick(){
    this.router.navigate(['/']);
  }

  RegisterClick(){
    this.router.navigate(['/register']);
  }

  LoginClick(){
    this.router.navigate(['/login']);

  }

  AccountClick(){
    this.router.navigate(['/account']);

  }
  ReservationsClick(){
    this.router.navigate(['/reservations']);

  }


}
