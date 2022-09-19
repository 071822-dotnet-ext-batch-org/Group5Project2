import { TestBed } from '@angular/core/testing';

import { GetMyCartService } from './get-my-cart.service';

describe('GetMyCartService', () => {
  let service: GetMyCartService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetMyCartService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
