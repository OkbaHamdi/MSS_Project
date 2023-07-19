import { TestBed } from '@angular/core/testing';

import { CashoutService } from './cashout.service';

describe('CashoutService', () => {
  let service: CashoutService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CashoutService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
