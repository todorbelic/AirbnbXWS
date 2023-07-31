import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserProfileData } from 'src/app/dto/userProfileData';
import { AuthenticationService } from 'src/app/services/authentication-service';

@Component({
  selector: 'app-host-profile',
  templateUrl: './host-profile.component.html',
  styleUrls: ['./host-profile.component.css']
})
export class HostProfileComponent {
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
      this.toast.success('Profile edited successfully');
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
