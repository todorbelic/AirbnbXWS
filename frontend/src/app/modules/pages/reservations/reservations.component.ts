import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from 'src/app/model/reservation';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent {

  public dataSource = new MatTableDataSource<Reservation>();
  public displayedColumns = ['takeOffCity','landingCity','takeOffDate','landingDate','ticketPricePerPassenger','ticketNumber'];

}
