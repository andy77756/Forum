import { TestBed } from '@angular/core/testing';

import { LevelTwoAuthGuard } from './level-two-auth.guard';

describe('LevelTwoAuthGuard', () => {
  let guard: LevelTwoAuthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(LevelTwoAuthGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
