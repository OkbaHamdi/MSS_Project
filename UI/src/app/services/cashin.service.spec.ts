import { TestBed } from '@angular/core/testing';

import { CashinService } from './cashin.service';

describe('CashinService', () => {
  let service: CashinService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CashinService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
