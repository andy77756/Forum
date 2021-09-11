import { TestBed } from '@angular/core/testing';

import { LevelOneAuthGuard } from './level-one-auth.guard';

describe('LevelOneAuthGuard', () => {
  let guard: LevelOneAuthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(LevelOneAuthGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
