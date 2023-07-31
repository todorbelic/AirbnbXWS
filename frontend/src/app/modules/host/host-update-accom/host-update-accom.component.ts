import { Component } from '@angular/core';
import { Accommodation } from 'src/app/model/accommodation';

@Component({
  selector: 'app-host-update-accom',
  templateUrl: './host-update-accom.component.html',
  styleUrls: ['./host-update-accom.component.css']
})
export class HostUpdateAccomComponent {
  public card:Accommodation=new Accommodation;

  constructor(){

  }

  ngOnInit(){

  }
}
