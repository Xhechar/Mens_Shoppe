import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminVisualsComponent } from './admin-visuals.component';

describe('AdminVisualsComponent', () => {
  let component: AdminVisualsComponent;
  let fixture: ComponentFixture<AdminVisualsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminVisualsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminVisualsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
