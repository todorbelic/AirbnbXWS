import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Accommodation } from 'src/app/model/accommodation';
import { AccomService } from 'src/app/services/accom-service';
import { AuthenticationService } from 'src/app/services/authentication-service';
import { ReviewService } from 'src/app/services/review-service';

@Component({
  selector: 'app-accommodation-details',
  templateUrl: './accommodation-details.component.html',
  styleUrls: ['./accommodation-details.component.css']
})
export class AccommodationDetailsComponent implements OnInit{
  
  private id: any
  public card: Accommodation = new Accommodation();
  public accRatings!: any[]
  public hostRatings!: any[]
  public averageAccRating: number = 0;
  public averageHostRating: number = 0;

  constructor(private route: ActivatedRoute, private router: Router, private dialog: MatDialog,
    private jwtHelper : AuthenticationService, private accommodationService : AccomService, private reviewService: ReviewService) { }

  
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.accommodationService.getById(this.id).subscribe(res=> {
      this.card = res.accommodation;
    })
    this.reviewService.getAccommodationRatings(this.id).subscribe(res=> {
      this.accRatings = res.ratings
      this.averageAccRating = res.averageRating
    })
    this.reviewService.getHostRatings(this.card.hostId).subscribe(res=> {
      this.hostRatings = res.ratings
      this.averageHostRating = res.averageRating
    })
  }

  getUserName(id: any) {
    // this.jwtHelper.getUser(id).subscribe(res=> {
    //   //console.log(res)
    //   //return res.user.firstName + ' ' + res.user.lastName
    // })
  }
}
