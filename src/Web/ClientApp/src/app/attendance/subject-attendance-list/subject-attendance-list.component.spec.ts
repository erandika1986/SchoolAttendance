import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectAttendanceListComponent } from './subject-attendance-list.component';

describe('SubjectAttendanceListComponent', () => {
  let component: SubjectAttendanceListComponent;
  let fixture: ComponentFixture<SubjectAttendanceListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubjectAttendanceListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubjectAttendanceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
