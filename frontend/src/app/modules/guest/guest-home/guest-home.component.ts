import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AccommodationSearchRequest } from 'src/app/dto/accommodation-search-request';
import { AccomService } from 'src/app/services/accom-service';
import { AuthenticationService } from 'src/app/services/authentication-service';
import { LeaveRatingComponent } from '../leave-rating/leave-rating.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-guest-home',
  templateUrl: './guest-home.component.html',
  styleUrls: ['./guest-home.component.css']
})
export class GuestHomeComponent {

  public searchText: any= "";
  // cards = [
  //   {
  //     title: 'Card Title 1',
  //     description: 'Some quick example text to build on the card title and make up the bulk of the card content',
  //     buttonText: 'Button',
  //     img: 'https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20(34).jpg'
  //   }

  // ];

  public cards:any[]=[]
  public showSearched = false;
  public accSearch: AccommodationSearchRequest = new AccommodationSearchRequest();
  constructor(private accomService:AccomService, public dialog: MatDialog, private authService: AuthenticationService, private router: Router) { 
  }

  ngOnInit(): void {
    this.loadAccom();
  }

  resetSearch() {
    this.accSearch = new AccommodationSearchRequest();
    this.showSearched = false;
    this.loadAccom();
  }

  applySearch() {
    this.showSearched = true;
    this.accomService.getSearched(this.accSearch).subscribe(res=> {
      this.cards = res.accommodations;
    })
  }

  loadAccom(){
    this.accomService.getAll().subscribe(res=>{
      this.cards=res.accommodations
    })

  }
  rateAccommodation(id: any) {
    const dialogRef = this.dialog.open(LeaveRatingComponent, {
      width: '350px',
      data: {
        accommodationId: id,
        guestId: this.authService.getId()
      }
    })
  }

  accommodationDetails(id: any) {
    this.router.navigate(['guest-accommodation-details/' + id]);
  }

  loadPictures(){

    // this.cards.forEach(accom => {
    //   var accomm=accom as Accommodation
    //   const reader = new FileReader();
    //   reader.onload = (e) => accomm.pictures = e.target.result;
    //   reader.readAsDataURL(new Blob([data])); 
    // });
    
  }
}

