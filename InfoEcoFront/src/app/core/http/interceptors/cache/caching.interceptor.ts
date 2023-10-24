import { HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CACHE_URLS, xkeyNotCACHE } from '@core/services/cache/cache-entry';
import { CacheService } from '@core/services/cache/cache.service';
import { of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable()
export class CachingInterceptor implements HttpInterceptor {
  constructor(private cacheService: CacheService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (this.isCachable(req)) {
      const cachedResponse = this.cacheService.get(req);
      if (cachedResponse !== null) {
        return of(cachedResponse);
      }
      return next.handle(req).pipe(
        tap((event) => {
          if (event instanceof HttpResponse) {
            this.cacheService.put(req, event);
          }
        }),
      );
    } else {
      return next.handle(req);
    }
  }

  /** Is this request cachable? */
  private isCachable(req: HttpRequest<any>) {
    // Only GET requests are cachable
    return req.method === 'GET' && !req.headers.has(xkeyNotCACHE) && CACHE_URLS.findIndex((x) => req.url.indexOf(x) > -1) > -1;
  }
}
