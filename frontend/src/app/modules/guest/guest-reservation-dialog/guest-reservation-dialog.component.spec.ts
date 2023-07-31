import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuestReservationDialogComponent } from './guest-reservation-dialog.component';

describe('GuestReservationDialogComponent', () => {
  let component: GuestReservationDialogComponent;
  let fixture: ComponentFixture<GuestReservationDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GuestReservationDialogComponent]
    });
    fixture = TestBed.createComponent(GuestReservationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
