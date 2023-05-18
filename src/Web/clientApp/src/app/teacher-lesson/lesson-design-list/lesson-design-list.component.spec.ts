import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonDesignListComponent } from './lesson-design-list.component';

describe('LessonDesignListComponent', () => {
  let component: LessonDesignListComponent;
  let fixture: ComponentFixture<LessonDesignListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonDesignListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonDesignListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
