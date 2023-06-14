import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RegistrationRequestData } from 'src/app/model/registrationRequestData';
import { UserProfileData } from 'src/app/model/userProfileData';
import { AuthenticationService } from 'src/app/services/authentication-service';

@Component({
  selector: 'app-guest-profile',
  templateUrl: './guest-profile.component.html',
  styleUrls: ['./guest-profile.component.css']
})
export class GuestProfileComponent implements OnInit {

  public constructor(private authService: AuthenticationService, private toast : ToastrService){}
  
  userProfile : UserProfileData = new UserProfileData()
  ngOnInit(): void {
    this.loadUserInfo()
  }

  loadUserInfo(){
    this.authService.getCurrentUser().subscribe(res=>{
      this.userProfile = res.user;
    })
  }

  updateUser(){
    this.authService.updateUser(this.userProfile).subscribe(res=> {
      this.toast.error('Profile edited successfully');
    }, error=> {
      if(error.status == 409) {
        this.toast.error(error.error.message)
      }else{
        this.toast.error('Something went wrong!')
      }
    });
  }
  deleteUser(){
    
  }
}
