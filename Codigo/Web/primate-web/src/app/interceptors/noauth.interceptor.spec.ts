import { TestBed } from '@angular/core/testing';

import { NoauthInterceptor } from './noauth.interceptor';

describe('NoauthInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      NoauthInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: NoauthInterceptor = TestBed.inject(NoauthInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
