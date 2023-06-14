import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HostUpdateAccomComponent } from './host-update-accom.component';

describe('HostUpdateAccomComponent', () => {
  let component: HostUpdateAccomComponent;
  let fixture: ComponentFixture<HostUpdateAccomComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HostUpdateAccomComponent]
    });
    fixture = TestBed.createComponent(HostUpdateAccomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
