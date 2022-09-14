import { TestBed } from '@angular/core/testing';

import { EcommerceAPIService } from './ecommerce-api.service';

describe('EcommerceAPIService', () => {
  let service: EcommerceAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EcommerceAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
