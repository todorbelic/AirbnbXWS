import { Component } from '@angular/core';
import { UserProfileData } from 'src/app/model/userProfileData';

@Component({
  selector: 'app-host-profile',
  templateUrl: './host-profile.component.html',
  styleUrls: ['./host-profile.component.css']
})
export class HostProfileComponent {
  public constructor(){}
  userProfile : UserProfileData = new UserProfileData()
  ngOnInit(): void {
    this.loadUserInfo()
  }

  loadUserInfo(){

  }

  updateUser(){

  }
  deleteUser(){
    
  }
}
