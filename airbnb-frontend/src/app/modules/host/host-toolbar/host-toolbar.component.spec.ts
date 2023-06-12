import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HostToolbarComponent } from './host-toolbar.component';

describe('HostToolbarComponent', () => {
  let component: HostToolbarComponent;
  let fixture: ComponentFixture<HostToolbarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HostToolbarComponent]
    });
    fixture = TestBed.createComponent(HostToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
