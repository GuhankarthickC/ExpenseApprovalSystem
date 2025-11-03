import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPendingComponent } from './view-pending.component';

describe('ViewPendingComponent', () => {
  let component: ViewPendingComponent;
  let fixture: ComponentFixture<ViewPendingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewPendingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewPendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
