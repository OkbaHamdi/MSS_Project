import { TestBed } from '@angular/core/testing';

import { AddTokenService } from './add-token.service';

describe('AddTokenService', () => {
  let service: AddTokenService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddTokenService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
