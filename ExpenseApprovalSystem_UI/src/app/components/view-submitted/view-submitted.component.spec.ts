import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewSubmittedComponent } from './view-submitted.component';

describe('ViewSubmittedComponent', () => {
  let component: ViewSubmittedComponent;
  let fixture: ComponentFixture<ViewSubmittedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewSubmittedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewSubmittedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
