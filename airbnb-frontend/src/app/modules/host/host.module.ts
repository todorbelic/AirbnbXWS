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
import { NewAccomComponent } from "../pages/new-accom/new-accom.component";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { HostUpdateAccomComponent } from './host-update-accom/host-update-accom.component';
import {MatListModule} from '@angular/material/list';


const routes: Routes = [
    { path: 'host-profile', component: HostProfileComponent },
    { path: 'host-edit', component: HostEditProfileComponent},
    { path:'host-home', component: HostHomeComponent},
    { path: 'host-add-accommodation', component: NewAccomComponent},
    { path:'host-update-accom', component: HostUpdateAccomComponent},


  ];
  

  @NgModule({
    declarations: [
        HostEditProfileComponent,
        HostProfileComponent,
        HostHomeComponent,
        HostToolbarComponent,
        HostUpdateAccomComponent,
  ],
    imports: [
      CommonModule,
      AppRoutingModule,
      MaterialModule,
      FormsModule,
      MatInputModule,
      MatFormFieldModule,
      MatListModule,
      RouterModule.forChild(routes)
    ]
  })
  export class HostModule { }
  