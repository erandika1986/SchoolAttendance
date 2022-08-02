import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonPrerequisitesComponent } from './lesson-prerequisites.component';

describe('LessonPrerequisitesComponent', () => {
  let component: LessonPrerequisitesComponent;
  let fixture: ComponentFixture<LessonPrerequisitesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonPrerequisitesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonPrerequisitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
