import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ExpiredSession } from '@core/store/actions/auth.action';
import { getToken } from '@core/store/selectors/auth.selector';
import { IAppState } from '@core/store/state/app.state';
import { select, Store } from '@ngrx/store';
import { Observable, Subject, takeUntil, tap } from 'rxjs';
import { Token } from 'src/app/shared/models/token';
import { AuthService } from '../../../services/auth.service';

@Injectable()
class AuthHttpInterceptor implements HttpInterceptor {
  private readonly xKey = 'Authorization';
  private destroy$ = new Subject<void>();
  private token?: Token;

  constructor(private store: Store<IAppState>, private authService: AuthService, private router: Router) {
    this.store.pipe(takeUntil(this.destroy$), select(getToken)).subscribe((token: Token) => {
      this.token = token;
    });
  }

  processResponse = (event: any) => {
    if (event instanceof HttpResponse) {
      if (event.ok) {
        if (event.headers && event.headers.get(this.xKey) && typeof event.headers.get(this.xKey) === 'string') {
          // Do not update if request might be coming from cache. This would possibly push an unlogged cookie to a logged user.
          if (!event.headers.get('Cache-Control') || event?.headers?.get('Cache-Control')?.indexOf('max-age') === -1) {
            // TODO refresh Token ?
          }
        }
      }
    }
    // eslint-disable-next-line
  };

  processError = (event: any) => {
    switch (event.status) {
      case 401: {
        if (this.token?.isExpired) {
          this.store.dispatch(new ExpiredSession());
        }
        break;
      }
      case 410: {
        // redirect to 410 error page
        // this.router.navigate([RoutingKeys.error410], { skipLocationChange: true });
        break;
      }
      // event.status 0 dev test
      case event.status >= 500 || event.status === 0: {
        // redirect to 500 error page
        // this.router.navigate([RoutingKeys.error500], { skipLocationChange: true });
        break;
      }
    }
    // eslint-disable-next-line
  };

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Set auth header only for internal requests
    // Get the auth header from the service.
    const requestHeader = req.headers.get(this.xKey);
    // Add to API default token
    const defaultHeader = this.token && this.token.accessToken ? this.token.accessToken : null;
    // || (defaultHeader && !req?.url.startsWith('{moteurCalculWeb}'))
    if ((requestHeader && !this.isBasic(requestHeader)) || (defaultHeader && !req?.url.startsWith('{moteurCalculWeb}'))) {
      // Clone the request to add the new header.
      const headers = req.headers.set(this.xKey, requestHeader ? `Bearer ${requestHeader}` : `Bearer ${defaultHeader}`);

      const authReq = req.clone({
        headers,
      });

      // Pass on the cloned request instead of the original request.
      return next.handle(authReq).pipe(tap(this.processResponse, this.processError));
    } else {
      return next.handle(req);
    }
  }

  redirectTo(uri: any): void {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => this.router.navigate([uri]));
  }

  private isBasic(headerKey: string): boolean {
    if (!headerKey) {
      return false;
    }
    return headerKey.includes('Basic');
  }
}

export { AuthHttpInterceptor };
