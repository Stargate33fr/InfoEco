import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { RoutingKeys } from '@core/rounting/routing-keys';
import { isLogged } from '@core/store/selectors/auth.selector';
import { IAppState } from '@core/store/state/app.state';
import { select, Store } from '@ngrx/store';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { Observable } from 'rxjs';
import { filter, map, switchMap, take } from 'rxjs/operators';

@Injectable()
export class UnLoggedUsersGuard implements CanActivate {
  constructor(private store: Store<IAppState>, private router: Router, private notification: NzNotificationService) {}

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  public canActivate(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot): Observable<boolean> {
    const _isLogged = this.store.pipe(
      select(isLogged),
      take(1),
      map((logged) => {
        return !logged;
      }),
    );

    _isLogged
      .pipe(
        filter((logged) => !logged),
        switchMap(() => {
          return "vous n'avez pas acces";
        }),
      )
      .subscribe((message) => {
        this.notification.warning('Attention', message);
        this.router.navigate([RoutingKeys.dashboard]);
      });
    return _isLogged;
  }
}
