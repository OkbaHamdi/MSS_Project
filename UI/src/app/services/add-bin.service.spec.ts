import { TestBed } from '@angular/core/testing';

import { AddBinService } from './add-bin.service';

describe('AddBinService', () => {
  let service: AddBinService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddBinService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
