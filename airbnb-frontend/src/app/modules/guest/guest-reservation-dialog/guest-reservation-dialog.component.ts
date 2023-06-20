import { Component , Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReservationView } from 'src/app/dto/reservation-view';


@Component({
  selector: 'app-guest-reservation-dialog',
  templateUrl: './guest-reservation-dialog.component.html',
  styleUrls: ['./guest-reservation-dialog.component.css']
})
export class GuestReservationDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<GuestReservationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {reservation: ReservationView, isCancelled: boolean, isResolved: boolean, reasoning : string}
  ) {}
  cancel(){
    this.data.isResolved = false
    console.log(this.data.reservation)
    this.dialogRef.close(this.data)
  }

  cancelClick(){
    this.data.isResolved = true
    this.data.isCancelled = true
    this.dialogRef.close(this.data)
  }
}
