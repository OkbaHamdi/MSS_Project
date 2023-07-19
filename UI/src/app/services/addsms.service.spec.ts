import { TestBed } from '@angular/core/testing';

import { AddsmsService } from './addsms.service';

describe('AddsmsService', () => {
  let service: AddsmsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddsmsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
