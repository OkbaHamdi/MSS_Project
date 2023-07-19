import { TestBed } from '@angular/core/testing';

import { AddBankService } from './add-bank.service';

describe('AddBankService', () => {
  let service: AddBankService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddBankService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
