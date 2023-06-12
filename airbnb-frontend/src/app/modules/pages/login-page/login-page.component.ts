import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LogInRequestData } from 'src/app/model/logInRequestData';
import { AuthenticationService } from 'src/app/services/authentication-service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  credentials : LogInRequestData = new LogInRequestData()
  constructor(private authService : AuthenticationService,  private toast : ToastrService, private router: Router) { }

  logInUser(){
    if(this.validityChecked()) {
      this.authService.logInUser(this.credentials).subscribe(res => {
        this.authService.setSession(res);
        //ovo ce biti naknadno implementirano
        //let role=this.authService.getRole();
        let role = 'HOST'
        if(role==='HOST') this.router.navigate(['/host-home']);
        else if (role==='GUEST') this.router.navigate(['/guest-home']);
        else console.log('ERROR: no such user type');
        })
    }
  }

  validityChecked(){
    if(this.credentials.username !=='' && this.credentials.password !== '') return true
    this.toast.error('you have to fill up all fields!')
    return false
  }
  }
