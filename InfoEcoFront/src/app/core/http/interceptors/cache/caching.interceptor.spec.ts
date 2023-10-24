import { TestBed } from '@angular/core/testing';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { CachingInterceptor } from './caching.interceptor';

describe('CachingInterceptor', () => {
  let service: CachingInterceptor;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharedTestingModule],
      providers: [CachingInterceptor],
    }).compileComponents();
  });

  beforeEach(() => {
    service = TestBed.inject(CachingInterceptor);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
