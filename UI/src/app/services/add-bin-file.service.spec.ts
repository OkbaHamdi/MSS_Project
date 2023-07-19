import { TestBed } from '@angular/core/testing';

import { AddBinFileService } from './add-bin-file.service';

describe('AddBinFileService', () => {
  let service: AddBinFileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddBinFileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
