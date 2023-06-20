import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Routes, RouterModule } from "@angular/router";
import { AppRoutingModule } from "src/app/app-routing.module";
import { MaterialModule } from "src/app/material/material.module";
import { HostEditProfileComponent } from "./host-edit-profile/host-edit-profile.component";
import { HostHomeComponent } from "./host-home/host-home.component";
import { HostProfileComponent } from "./host-profile/host-profile.component";
import { HostToolbarComponent } from "./host-toolbar/host-toolbar.component";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { HostUpdateAccomComponent } from './host-update-accom/host-update-accom.component';
import {MatListModule} from '@angular/material/list';
import { NewAccommComponent } from './new-accom/new-accomm.component';
import { NgSelectModule } from "@ng-select/ng-select";
import { PendingRequestsComponent } from './pending-requests/pending-requests.component';
import { HostReservationDialogComponent } from './host-reservation-dialog/host-reservation-dialog.component';


const routes: Routes = [
    { path: 'host-profile', component: HostProfileComponent },
    { path: 'host-edit', component: HostEditProfileComponent},
    { path:'host-home', component: HostHomeComponent},
    { path: 'host-add-accommodation', component: NewAccommComponent},
    { path:'host-update-accom', component: HostUpdateAccomComponent},
    {path: 'host-pending-res',component:PendingRequestsComponent}


  ];
  

  @NgModule({
    declarations: [
        HostEditProfileComponent,
        HostProfileComponent,
        HostHomeComponent,
        HostToolbarComponent,
        HostUpdateAccomComponent,
        NewAccommComponent,
        PendingRequestsComponent,
        HostReservationDialogComponent,
  ],
    imports: [
      CommonModule,
      AppRoutingModule,
      MaterialModule,
      FormsModule,
      MatInputModule,
      MatFormFieldModule,
      MatListModule,
      NgSelectModule,
      RouterModule.forChild(routes)
    ]
  })
  export class HostModule { }
  