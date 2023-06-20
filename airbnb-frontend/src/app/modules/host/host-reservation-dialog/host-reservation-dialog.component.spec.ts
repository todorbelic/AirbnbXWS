import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HostReservationDialogComponent } from './host-reservation-dialog.component';

describe('HostReservationDialogComponent', () => {
  let component: HostReservationDialogComponent;
  let fixture: ComponentFixture<HostReservationDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HostReservationDialogComponent]
    });
    fixture = TestBed.createComponent(HostReservationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
