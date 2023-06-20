import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Reservation } from 'src/app/model/reservation';
import { ReservationService } from 'src/app/services/reservation-service';
import { AuthenticationService } from 'src/app/services/authentication-service';
import { ReservationView } from 'src/app/dto/reservation-view';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog } from '@angular/material/dialog';
import { HostReservationDialogComponent } from '../host-reservation-dialog/host-reservation-dialog.component';
import { AcceptReservationRequest } from 'src/app/dto/accept-reservation-request';
import { DenyReservationRequest } from 'src/app/dto/deny-reservation-request';

@Component({
  selector: 'app-pending-requests',
  templateUrl: './pending-requests.component.html',
  styleUrls: ['./pending-requests.component.css']
})
export class PendingRequestsComponent {
  public dataSource = new MatTableDataSource<ReservationView>();
  public displayedColumns = ['AccommodationName','GuestName','GuestCount','startDate','endDate','status','click'];
  public reservations:ReservationView[]=[];

  public isAccepted:boolean=false;
  public isResolved:boolean=false;


  constructor(private reservationService:ReservationService,private authService:AuthenticationService,private toast: ToastrService, private router: Router, private route: ActivatedRoute,public dialog: MatDialog){

  }

  ngOnInit():void{
    console.log(this.authService.getId())
    this.reservationService.getRequestsForHost(this.authService.getId()).subscribe(res=>{
      this.reservations=res.requests;
      this.dataSource.data=this.reservations;

      
    });
  }

  resClick(res: ReservationView): void{
    const dialogRef = this.dialog.open(HostReservationDialogComponent, {
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
