import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CreateAccommodationRequest } from 'src/app/model/create-accommodation-request';
import { AccomService } from 'src/app/services/accom-service';
import { AuthenticationService } from 'src/app/services/authentication-service';

@Component({
  selector: 'app-new-accom',
  templateUrl: './new-accom.component.html',
  styleUrls: ['./new-accom.component.css'],
})
export class NewAccomComponent {
  createAccommodationRequest: CreateAccommodationRequest = new CreateAccommodationRequest
  base64Images: string[] = []
  benefits: string=''
  paymentOption: string =''

  constructor(private toast: ToastrService, private accommodationService: AccomService, private router: Router){}

  onUploadChange(event: any){
    const file = event.target.files[0];

    if(file){
      const reader = new FileReader();

      reader.onload = this.handleReaderLoaded.bind(this)
      reader.readAsBinaryString(file)
    }
  }

  handleReaderLoaded(e: any){
    this.base64Images.push('data:image/png;base64,' + btoa(e.target.result));
    console.log(this.base64Images)
  }

  validateNumberOfGuests(){
    if(this.createAccommodationRequest.request.minGuests > this.createAccommodationRequest.request.maxGuests){
      this.toast.error('Minimum must be lower than maximum!')
      return false;
    }

    return true;
  }

  addAccommodation(){
    if(this.validateNumberOfGuests()){
      //console.log(this.createAccommodationRequest)
      this.createAccommodationRequest.request.pictures = this.base64Images
      let userId = localStorage.getItem('userId')
      if(userId === null){
       this.toast.error("You're not logged in as a host!")
      } else {
        this.createAccommodationRequest.request.hostId = userId
        var parts = this.benefits.split(',')

        for(var part of parts){
          this.createAccommodationRequest.request.benefits.push(part)
        }

        this.createAccommodationRequest.request.paymentOption = parseInt(this.paymentOption)

        this.accommodationService.createNewAccommodation(this.createAccommodationRequest).subscribe( res =>{
          this.toast.success(res.response)
          this.router.navigate(['/host-home'])
        }, 
        (err: HttpErrorResponse) => {
          this.toast.error(err.error)
        }
        )
      }
    }
  }

}


