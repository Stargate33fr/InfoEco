import { TestBed } from '@angular/core/testing';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';

import { CacheService } from './cache.service';

describe('CacheService', () => {
  let service: CacheService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharedTestingModule],
      providers: [CacheService],
    }).compileComponents();
  });

  beforeEach(() => {
    service = TestBed.get(CacheService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
