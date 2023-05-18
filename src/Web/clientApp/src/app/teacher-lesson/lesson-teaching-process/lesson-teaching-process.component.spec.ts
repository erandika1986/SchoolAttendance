import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonTeachingProcessComponent } from './lesson-teaching-process.component';

describe('LessonTeachingProcessComponent', () => {
  let component: LessonTeachingProcessComponent;
  let fixture: ComponentFixture<LessonTeachingProcessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonTeachingProcessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonTeachingProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
