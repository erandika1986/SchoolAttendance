import { TestBed } from '@angular/core/testing';

import { LessonDesignService } from './lesson-design.service';

describe('LessonDesignService', () => {
  let service: LessonDesignService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LessonDesignService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
