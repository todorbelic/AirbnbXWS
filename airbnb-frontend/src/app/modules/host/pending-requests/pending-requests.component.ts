import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from 'src/app/model/reservation';
import { ReservationService } from 'src/app/services/reservation-service';
import { AuthenticationService } from 'src/app/services/authentication-service';
import { ReservationView } from 'src/app/dto/reservation-view';

@Component({
  selector: 'app-pending-requests',
  templateUrl: './pending-requests.component.html',
  styleUrls: ['./pending-requests.component.css']
})
export class PendingRequestsComponent {
  public dataSource = new MatTableDataSource<ReservationView>();
  public displayedColumns = ['AccommodationName','startDate','endDate','GuestCount','HostName','status','click',];
  public reservations:ReservationView[]=[];

  constructor(private reservationService:ReservationService,private authService:AuthenticationService){

  }

  ngOnInit():void{
    this.reservationService.getAllForHost(this.authService.getId()).subscribe(res=>{
      this.reservations=res.reservations;
      this.dataSource.data=this.reservations;

      console.log(this.reservations[0])
    });
  }

  resClick(request: ReservationView): void{
    // const dialogRef = this.dialog.open(ControlUserDialogComponent, {
    //   data:{ employee: request, isBlocked : this.isBlocked, isResolved: this.isResolved},
    // })
    // dialogRef.afterClosed().subscribe(result => {
    //   console.log(result)
    //     if(result.isResolved){
    //       if(result.isBlocked){
    //         this.blockUser(request)
           
    //       } else  this.blockRefreshToken(request)
    //     }
    //   this.isResolved = false
    //   }
    // )
  }

}
