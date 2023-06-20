import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from 'src/app/model/reservation';
import { AuthenticationService } from 'src/app/services/authentication-service';
import { ReservationService } from 'src/app/services/reservation-service';

@Component({
  selector: 'app-all-reservations',
  templateUrl: './all-reservations.component.html',
  styleUrls: ['./all-reservations.component.css']
})
export class AllReservationsComponent {
  public dataSource = new MatTableDataSource<Reservation>();
  public displayedColumns = ['host','accom','startDate','endDate','guestCount','status'];

  constructor(private reservationService:ReservationService,private authService:AuthenticationService){

  }

  ngOnInit(){
    this.reservationService.getActiveForGuest(this.authService.getId()).subscribe(res=>{
      this.dataSource=res;
    });
  }

  revokeCertificate(certificate:any){
    
  }
}
