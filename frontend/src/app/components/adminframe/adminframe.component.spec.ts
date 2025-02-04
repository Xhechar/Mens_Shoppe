import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminframeComponent } from './adminframe.component';

describe('AdminframeComponent', () => {
  let component: AdminframeComponent;
  let fixture: ComponentFixture<AdminframeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminframeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminframeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
