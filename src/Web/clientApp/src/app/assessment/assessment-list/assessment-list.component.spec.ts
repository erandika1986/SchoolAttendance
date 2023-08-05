import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssessmentListComponent } from './assessment-list.component';

describe('AssessmentListComponent', () => {
  let component: AssessmentListComponent;
  let fixture: ComponentFixture<AssessmentListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssessmentListComponent]
    });
    fixture = TestBed.createComponent(AssessmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
