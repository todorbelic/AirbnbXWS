import { Component, OnInit } from '@angular/core';
import { RegistrationRequestData } from 'src/app/dto/registrationRequestData';
import { UserProfileData } from 'src/app/dto/userProfileData';

@Component({
  selector: 'app-guest-profile',
  templateUrl: './guest-profile.component.html',
  styleUrls: ['./guest-profile.component.css']
})
export class GuestProfileComponent implements OnInit {

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
