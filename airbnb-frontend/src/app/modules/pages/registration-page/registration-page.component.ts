import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Address } from 'src/app/model/address';
import { User } from 'src/app/model/user';
import { AuthenticationService } from 'src/app/services/authentication-service';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css']
})

export class RegistrationPageComponent {
  registrationUser : User = new User()
  registrationAddress : Address = new Address()
  testPassword : string = ''
  
  constructor(private toast : ToastrService, private authService : AuthenticationService) { }

  signUp() {
    if(this.validityChecked()) {
      this.registrationUser.address = this.registrationAddress
      this.authService.registerUser(this.registrationUser).subscribe(res => {
        console.log(res)
        this.toast.success('Registration successful!')
      })
    }
  }

  validityChecked(){
    if (this.registrationUser.password !== this.testPassword) {
      this.toast.error('Password fields need to be matching!')
      return false;
    }
    return true
  }

/*
fieldsAreEmpty(object: Object) { 
  return Object.values(object).some(
      value => {
      if (value === null || value === '')  return true
      return false
    })
}
*/

}
