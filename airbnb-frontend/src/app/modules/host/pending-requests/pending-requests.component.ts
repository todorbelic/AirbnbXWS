import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from 'src/app/model/reservation';
import { ReservationService } from 'src/app/services/reservation-service';
import { AuthenticationService } from 'src/app/services/authentication-service';

@Component({
  selector: 'app-pending-requests',
  templateUrl: './pending-requests.component.html',
  styleUrls: ['./pending-requests.component.css']
})
export class PendingRequestsComponent {
  public dataSource = new MatTableDataSource<Reservation>();
  public displayedColumns = ['accommodationId','startDate','endDate','guestCount','hostId','reservationStatus'];
  public reservations:Reservation[]=[];

  constructor(private reservationService:ReservationService,private authService:AuthenticationService){

  }

  ngOnInit():void{
    this.reservationService.getActiveForHost(this.authService.getId()).subscribe(res=>{
      this.reservations=res.reservations;
      this.dataSource.data=this.reservations;

      console.log(this.reservations[0])
    });
  }

}
