import { TestBed } from '@angular/core/testing';

import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { AuthHttpInterceptor } from './auth.interceptor';

describe('AuthHttpInterceptor', () => {
  let service: AuthHttpInterceptor;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharedTestingModule],
      providers: [AuthHttpInterceptor],
    }).compileComponents();
  });

  beforeEach(() => {
    service = TestBed.inject(AuthHttpInterceptor);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
