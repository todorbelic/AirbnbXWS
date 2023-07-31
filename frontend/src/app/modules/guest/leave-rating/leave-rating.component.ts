import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReviewService } from 'src/app/services/review-service';

@Component({
  selector: 'app-leave-rating',
  templateUrl: './leave-rating.component.html',
  styleUrls: ['./leave-rating.component.css']
})
export class LeaveRatingComponent implements OnInit {

  stars: number[] = [1, 2, 3, 4, 5];
  selectedValue: number = 0;
  isMouseover = true;
  value: number = 0;

  countStar(star: number) {
    this.isMouseover = false;
    this.selectedValue = star;
    this.value = star;

  }

  addClass(star: number) {
    if (this.isMouseover) {
      this.selectedValue = star;
    }
  }

  removeClass() {
    if (this.isMouseover) {
      this.selectedValue = 0;
    }
  }

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialogRef: MatDialogRef<LeaveRatingComponent>, private ratingService: ReviewService) { }

  ngOnInit(): void {
  }

  sendReview() {
    this.ratingService.rateAccommodation(this.data.guestId, this.data.accommodationId, this.value).subscribe({
        next: response => {
          alert("Feedback sent!");
        },
        error: message => {
          alert("You can't rate this!")
        }
      }
    )
  }

  cancelReview() {
    this.dialogRef.close();
  }
}
