import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewAccomComponent } from './new-accom.component';

describe('NewAccomComponent', () => {
  let component: NewAccomComponent;
  let fixture: ComponentFixture<NewAccomComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewAccomComponent]
    });
    fixture = TestBed.createComponent(NewAccomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
