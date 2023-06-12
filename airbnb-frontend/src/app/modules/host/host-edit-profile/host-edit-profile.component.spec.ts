import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HostEditProfileComponent } from './host-edit-profile.component';

describe('HostEditProfileComponent', () => {
  let component: HostEditProfileComponent;
  let fixture: ComponentFixture<HostEditProfileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HostEditProfileComponent]
    });
    fixture = TestBed.createComponent(HostEditProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
