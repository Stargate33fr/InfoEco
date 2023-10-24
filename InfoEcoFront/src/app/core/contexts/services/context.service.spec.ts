import { TestBed } from '@angular/core/testing';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';

import { ContextService } from './context.service';

describe('ContextService', () => {
  let service: ContextService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharedTestingModule],
      providers: [ContextService],
    }).compileComponents();
  });

  beforeEach(() => {
    service = TestBed.get(ContextService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
