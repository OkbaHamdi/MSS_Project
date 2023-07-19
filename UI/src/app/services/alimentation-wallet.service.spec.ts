import { TestBed } from '@angular/core/testing';

import { AlimentationWalletService } from './alimentation-wallet.service';

describe('AlimentationWalletService', () => {
  let service: AlimentationWalletService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlimentationWalletService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
