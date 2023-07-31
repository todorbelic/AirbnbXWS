import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewAccommComponent } from './new-accomm.component';

describe('NewAccommComponent', () => {
  let component: NewAccommComponent;
  let fixture: ComponentFixture<NewAccommComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewAccommComponent]
    });
    fixture = TestBed.createComponent(NewAccommComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
