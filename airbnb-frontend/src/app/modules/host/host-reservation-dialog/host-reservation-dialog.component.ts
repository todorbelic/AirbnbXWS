import { Component , Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReservationView } from 'src/app/dto/reservation-view';

@Component({
  selector: 'app-host-reservation-dialog',
  templateUrl: './host-reservation-dialog.component.html',
  styleUrls: ['./host-reservation-dialog.component.css']
})
export class HostReservationDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<HostReservationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {reservation: ReservationView, isAccepted: boolean, isResolved: boolean, reasoning : string}
  ) {}
  cancel(){
    this.data.isResolved = false
    this.dialogRef.close(this.data)
  }

  blockUser(){
    this.data.isResolved = true
    //this.data.isBlocked = true
    this.dialogRef.close(this.data)
  }
  blockRefreshTokens(){
    this.data.isResolved = true
    //this.data.isBlocked = false
    this.dialogRef.close(this.data)
  }
  
}
