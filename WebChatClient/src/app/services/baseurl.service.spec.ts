import { TestBed } from '@angular/core/testing';

import { BaseurlService } from './baseurl.service';

describe('BaseurlService', () => {
  let service: BaseurlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BaseurlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
