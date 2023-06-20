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
  public displayedColumns = ['accom','startDate','endDate','guestCount','host','status'];
  public reservations:Reservation[]=[];

  constructor(private reservationService:ReservationService,private authService:AuthenticationService){

  }

  ngOnInit():void{
    this.reservationService.getActiveForGuest(this.authService.getId()).subscribe(res=>{
      this.reservations=res.reservations;
      
      this.dataSource.data=this.reservations;

      console.log('res='+res)
      console.log('res.reservations='+this.reservations)
    });
  }
}
