import { TestBed } from '@angular/core/testing';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';

import { TransformRelativeToAbsoluteUrlInterceptor } from './transform-relative-to-absolute-url.interceptor';

describe('TransformRelativeToAbsoluteUrlInterceptor', () => {
  let service: TransformRelativeToAbsoluteUrlInterceptor;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharedTestingModule],
      providers: [TransformRelativeToAbsoluteUrlInterceptor],
    }).compileComponents();
  });

  beforeEach(() => {
    service = TestBed.inject(TransformRelativeToAbsoluteUrlInterceptor);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
