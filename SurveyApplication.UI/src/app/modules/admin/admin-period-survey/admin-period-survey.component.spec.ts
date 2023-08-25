import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPeriodSurveyComponent } from './admin-period-survey.component';

describe('AdminPeriodSurveyComponent', () => {
  let component: AdminPeriodSurveyComponent;
  let fixture: ComponentFixture<AdminPeriodSurveyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminPeriodSurveyComponent]
    });
    fixture = TestBed.createComponent(AdminPeriodSurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
