import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUnitTypeComponent } from './admin-unit-type.component';

describe('AdminUnitTypeComponent', () => {
  let component: AdminUnitTypeComponent;
  let fixture: ComponentFixture<AdminUnitTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminUnitTypeComponent]
    });
    fixture = TestBed.createComponent(AdminUnitTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
