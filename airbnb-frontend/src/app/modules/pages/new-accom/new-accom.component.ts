import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CreateAccommodationRequest } from 'src/app/model/create-accommodation-request';
import { AccomService } from 'src/app/services/accom-service';

@Component({
  selector: 'app-new-accom',
  templateUrl: './new-accom.component.html',
  styleUrls: ['./new-accom.component.css'],
})
export class NewAccomComponent {
  createAccommodationRequest: CreateAccommodationRequest = new CreateAccommodationRequest
  base64Images: string[] = []

  constructor(private toast: ToastrService, private accommodationService: AccomService){}

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
      console.log(this.createAccommodationRequest)
      this.createAccommodationRequest.request.pictures = this.base64Images
      let  currentUser = localStorage.getItem('currentUser')
      console.log(currentUser)
    }
  }

}


