import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseAnAnswerComponent } from './choose-an-answer.component';

describe('ChooseAnAnswerComponent', () => {
  let component: ChooseAnAnswerComponent;
  let fixture: ComponentFixture<ChooseAnAnswerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChooseAnAnswerComponent]
    });
    fixture = TestBed.createComponent(ChooseAnAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
