import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, CanLoad, Route, Router, RouterStateSnapshot } from '@angular/router';
import { RoutingKeys } from '@core/rounting/routing-keys';
import { isLogged } from '@core/store/selectors/auth.selector';
import { IAppState } from '@core/store/state/app.state';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { filter, take } from 'rxjs/operators';

@Injectable()
export class LoggedUsersGuard implements CanActivate, CanActivateChild, CanLoad {
  constructor(private store: Store<IAppState>, private router: Router) {}

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const url = state ? state.url : RoutingKeys.dashboard;
    const returnUrl = route && route.data ? route.data['returnUrl'] : null;

    return this.check(returnUrl ? returnUrl : url);
  }

  public canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const url = state ? state.url : RoutingKeys.dashboard;
    const returnUrl = route && route.data ? route.data['returnUrl'] : null;

    return this.check(returnUrl ? returnUrl : url);
  }

  public canLoad(route: Route): Observable<boolean> {
    const returnUrl = route ? this.getReturnUrl(route.path?.toString()) : null;
    if (returnUrl) {
      return this.check(returnUrl);
    }

    const url = route ? this.router.createUrlTree([route.path]).toString() : RoutingKeys.dashboard;
    return this.check(url);
  }

  private check(url: string): Observable<boolean> {
    // TODO Ne fonctionne pas bien ! Peut etre utiliser is expired
    const _isLogged = this.store.pipe(select(isLogged), take(1));

    _isLogged.pipe(filter((logged) => !logged)).subscribe(() => {
      this.router.navigate([RoutingKeys.login], { queryParams: { returnUrl: url } });
    });
    return _isLogged;
  }

  private getReturnUrl(path: string | undefined): string {
    const routeConfig = this.router.config.find((config) => config.path === path);
    if (routeConfig) {
      const data = routeConfig.data;
      if (data) {
        const returnUrl = data['returnUrl'];
        return returnUrl;
      }
    }

    return null as any;
  }
}
