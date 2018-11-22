import { TestBed } from '@angular/core/testing';

import { RegistratedUserService } from './registrated-user.service';

describe('RegistratedUserService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RegistratedUserService = TestBed.get(RegistratedUserService);
    expect(service).toBeTruthy();
  });
});
