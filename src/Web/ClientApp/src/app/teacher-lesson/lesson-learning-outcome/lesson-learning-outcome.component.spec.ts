import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonLearningOutcomeComponent } from './lesson-learning-outcome.component';

describe('LessonLearningOutcomeComponent', () => {
  let component: LessonLearningOutcomeComponent;
  let fixture: ComponentFixture<LessonLearningOutcomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonLearningOutcomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonLearningOutcomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
