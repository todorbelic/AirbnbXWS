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
      RouterModule.forChild(routes)
    ]
  })
  export class PagesModule { }
  