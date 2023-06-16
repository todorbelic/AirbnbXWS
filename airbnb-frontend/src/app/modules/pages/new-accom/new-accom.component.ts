import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CreateAccommodationRequest } from 'src/app/model/create-accommodation-request';
import { AccomService } from 'src/app/services/accom-service';
import { AuthenticationService } from 'src/app/services/authentication-service';
import { NgSelectComponent } from '@ng-select/ng-select';

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

  selectedBenefit: number = 0;

  benefitList: string[] = []

  benefitsDropDown = [
    {id: 1, name: 'wifi'},
    {id: 2, name: 'tv'},
    {id: 3, name: 'balcony'},
    {id: 4, name: 'laundry machine'},
    {id: 5, name: 'stove'},
    {id: 6, name: 'air conditioning'},
    {id: 7, name: 'mountain view'},
    {id: 8, name: 'pet friendly'},
    {id: 9, name: 'free parking'},
  ]

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

  onRemove(event: any){
    console.log(this.selectedBenefit)
    
  }

  onAdd(event: any){
    console.log(this.selectedBenefit)
  }

  onChange(event: any){  
    this.benefitList = []

    if(event !== null){
      for(var e of event){       
        var name = e.name as string
        this.benefitList.push(name)
       }
    } 
    console.log(this.benefitList)
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

        if(this.benefitList.length === 0){
          this.toast.error("Please, select at least one benefit!")
        } else{
          this.createAccommodationRequest.request.benefits = this.benefitList
          this.createAccommodationRequest.request.paymentOption = parseInt(this.paymentOption)

          console.log(this.createAccommodationRequest)

          this.accommodationService.createNewAccommodation(this.createAccommodationRequest).subscribe( res =>{
            this.toast.success(res.response)
            this.router.navigate(['/host-home'])
          }, (err: HttpErrorResponse) => {
            this.toast.error(err.error)
          }
        )
        } 
      }
    }
  }

}


