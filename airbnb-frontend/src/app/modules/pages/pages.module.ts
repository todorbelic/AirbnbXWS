import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Routes, RouterModule } from "@angular/router";
import { AppRoutingModule } from "src/app/app-routing.module";
import { MaterialModule } from "src/app/material/material.module";
import { LandingPageComponent } from './landing-page/landing-page.component';
import { LoginPageComponent } from "./login-page/login-page.component";
import { RegistrationPageComponent } from "./registration-page/registration-page.component";
import { MainToolbarComponent } from './main-toolbar/main-toolbar.component';
import { NewAccomComponent } from './new-accom/new-accom.component';
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbCarouselModule } from "@ng-bootstrap/ng-bootstrap";
import { MatDatepicker } from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import { NgSelectModule } from "@ng-select/ng-select";

const routes: Routes = [
    { path: '', component: LandingPageComponent },
    { path: 'login', component: LoginPageComponent},
    { path:'register', component: RegistrationPageComponent},

  ];
  
  
  
  @NgModule({
    declarations: [
      LoginPageComponent,
      RegistrationPageComponent,
      LandingPageComponent,
      MainToolbarComponent,
      NewAccomComponent
  ],
    imports: [
      CommonModule,
      AppRoutingModule,
      MaterialModule,
      FormsModule,
      MatInputModule,
      MatFormFieldModule,
      NgbModule,
      NgbCarouselModule,
      MatNativeDateModule,
      NgSelectModule,
      RouterModule.forChild(routes)
    ]
  })
  export class PagesModule { }
  