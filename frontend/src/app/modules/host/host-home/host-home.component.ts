import { Component } from '@angular/core';
import { AccommodationSearchRequest } from 'src/app/dto/accommodation-search-request';
import { AccomService } from 'src/app/services/accom-service';
import { AuthenticationService } from 'src/app/services/authentication-service';

@Component({
  selector: 'app-host-home',
  templateUrl: './host-home.component.html',
  styleUrls: ['./host-home.component.css']
})
export class HostHomeComponent {
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
  constructor(private accomService:AccomService,private authService:AuthenticationService) { 
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
  searchHostAccom(){
    // this.accomService.getForHost(this.authService.getId()).subscribe(res=>{
    //   this.cards=res.accommodations
    // })
    this.cards.filter(a=>a.hostId===this.authService.getId())
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

