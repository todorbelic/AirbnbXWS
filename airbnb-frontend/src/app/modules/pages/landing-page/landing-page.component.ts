import { Component } from '@angular/core';
import { Accommodation } from 'src/app/model/accommodation';
import { AccomService } from 'src/app/services/accom-service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {

  public searchText: any= "";
  // cards = [
  //   {
  //     title: 'Card Title 1',
  //     description: 'Some quick example text to build on the card title and make up the bulk of the card content',
  //     buttonText: 'Button',
  //     img: 'https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20(34).jpg'
  //   }

  // ];

  public cards:Accommodation[]=[]

  
  constructor(private accomService:AccomService) { }

  ngOnInit(): void {
    this.loadAccom();
  }

  
  applySearch() {
  }

  loadAccom(){
    this.accomService.getAll().subscribe(res=>{
      this.cards=res
    })

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
