import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from 'src/app/model/reservation';
import { AuthenticationService } from 'src/app/services/authentication-service';
import { ReservationService } from 'src/app/services/reservation-service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog } from '@angular/material/dialog';
import { AcceptReservationRequest } from 'src/app/dto/accept-reservation-request';
import { DenyReservationRequest } from 'src/app/dto/deny-reservation-request';
import { ReservationView } from 'src/app/dto/reservation-view';
import { GuestReservationDialogComponent } from '../guest-reservation-dialog/guest-reservation-dialog.component';

@Component({
  selector: 'app-all-reservations',
  templateUrl: './all-reservations.component.html',
  styleUrls: ['./all-reservations.component.css']
})
export class AllReservationsComponent {
 
  constructor(private reservationService:ReservationService,private authService:AuthenticationService,private toast: ToastrService, private router: Router, private route: ActivatedRoute,public dialog: MatDialog){

  }


  ngOnInit():void{
    this.reservationService.getAllForGuest(this.authService.getId()).subscribe(res=>{
      this.reservations=res.reservations;
      
      this.dataSource.data=this.reservations;

    });
  }

  public dataSource = new MatTableDataSource<ReservationView>();
  public displayedColumns = ['AccommodationName','GuestName','GuestCount','startDate','endDate','status','click'];
  public reservations:ReservationView[]=[];

  public isAccepted:boolean=false;
  public isResolved:boolean=false;


  
  resClick(res: ReservationView): void{
    const dialogRef = this.dialog.open(GuestReservationDialogComponent, {
      data:{ reservation: res, isAccepted : this.isAccepted, isResolved: this.isResolved},
    })
    dialogRef.afterClosed().subscribe(result => {
      console.log(result)
        if(result.isResolved){
          if(result.isAccepted){
            this.acceptReservation(res);
          } else {
              this.denyReservation(res)
          }
        }
      this.isResolved = false
      }
    )
  }

  acceptReservation(res:ReservationView){
    var acceptedRes=new AcceptReservationRequest()
    acceptedRes.id=res.ReservationId;
    this.reservationService.acceptReservation(acceptedRes).subscribe(res=>{
      this.toast.success("Reservation accepted!")
    })
  }

  denyReservation(res:ReservationView){
      var deniedRes=new DenyReservationRequest()
      deniedRes.requestId=res.ReservationId;
      this.reservationService.denyReservation(deniedRes).subscribe(res=>{
        this.toast.success("Reservation denied!")
      })
    
  }
}
