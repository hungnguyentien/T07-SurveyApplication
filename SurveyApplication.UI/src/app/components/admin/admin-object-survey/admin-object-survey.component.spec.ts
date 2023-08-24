import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminObjectSurveyComponent } from './admin-object-survey.component';

describe('AdminObjectSurveyComponent', () => {
  let component: AdminObjectSurveyComponent;
  let fixture: ComponentFixture<AdminObjectSurveyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminObjectSurveyComponent]
    });
    fixture = TestBed.createComponent(AdminObjectSurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
