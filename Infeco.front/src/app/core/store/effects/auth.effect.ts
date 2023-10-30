import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '@core/services/auth.service';
import { CookieService } from '@core/services/cookie/cookie.service';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { NzMessageService } from 'ng-zorro-antd/message';
import { EMPTY, Observable, of } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { Token, tokenAccessKey, UserDto } from 'src/app/shared/models/token';
import { AuthActionType, ExpiredSession, LogIn, LogInFailure, LogInSuccess, Logout, ReLogin, ReLoginSuccess } from '../actions/auth.action';
import { GetUser } from '../actions/utilisateur.action';
import { IAppState } from '../state/app.state';

@Injectable()
export class AuthEffects {
  logIn$ = createEffect(() =>
    this.actions$.pipe(
      ofType<LogIn>(AuthActionType.login),
      switchMap((result) =>
        this.authService.login(result.payload.identifiant, result.payload.password).pipe(
          map((token: Token) => new LogInSuccess(token)),
          catchError(() => of(new LogInFailure(result.payload.identifiant))),
        ),
      ),
    ),
  );

  logInSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType<LogInSuccess>(AuthActionType.loginSuccess),
        tap((action) => {
          this.cookieService.set(tokenAccessKey, action.payload);
          const routeQueryParams = this.activatedRoute.snapshot.queryParams;
          const returnUrl = routeQueryParams['returnUrl'] ? routeQueryParams['returnUrl'] : ['/accueil'];
          this.router.navigateByUrl(returnUrl);
          const helper = new JwtHelperService();
          const user = helper.decodeToken(action.payload.accessToken) as UserDto;
          this.store.dispatch(new GetUser(user.sub));
          this.message.create('success', 'Bienvenue');
        }),
      ),
    { dispatch: false },
  );

  reLogin$ = createEffect(() =>
    this.actions$.pipe(
      ofType<ReLogin>(AuthActionType.reLogin),
      switchMap(() => {
        const token = this.authService.getCookieToken();
        if (!token) {
          return EMPTY;
        } else if (token.isExpired === null || token.isExpired === undefined || token.isExpired) {
          return of(new ExpiredSession());
        } else {
          return of(new ReLoginSuccess(token));
        }
      }),
    ),
  );

  reLoginSuccess = createEffect(
    () =>
      this.actions$.pipe(
        ofType<ReLoginSuccess>(AuthActionType.reLoginSuccess),
        map((action) => {
          const helper = new JwtHelperService();
          const user = helper.decodeToken(action.payload.accessToken) as UserDto;
          this.store.dispatch(new GetUser(user.sub));
        }),
        map(() => this.message.create('success', 'Welcome back')),
      ),
    { dispatch: false },
  );

  logout = createEffect(
    () =>
      this.actions$.pipe(
        ofType<Logout>(AuthActionType.logout),
        map(() => {
          //Auth Basic
          localStorage.removeItem('user');
          //Auth token
          this.cookieService.deleteAll();
          this.router.navigate(['/login']);
          this.message.create('success', 'Vous êtes déconnecté');
        }),
      ),
    { dispatch: false },
  );

  logInFailure: Observable<any> = createEffect(
    () =>
      this.actions$.pipe(
        ofType<LogInFailure>(AuthActionType.loginFailure),
        tap(() => this.message.create('error', 'Identifiant ou mot de passe incorrect')),
      ),
    { dispatch: false },
  );

  expiredSession = createEffect(
    () =>
      this.actions$.pipe(
        ofType<ExpiredSession>(AuthActionType.expiredSession),
        map(() => {
          localStorage.removeItem('user');
          this.cookieService.deleteAll();
          this.router.navigate(['/login']);
          this.message.create('warning', 'Votre session a expiré, veuillez vous reconnecter.');
        }),
      ),
    { dispatch: false },
  );

  constructor(
    private actions$: Actions,
    private router: Router,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private cookieService: CookieService,
    private message: NzMessageService,
    private store: Store<IAppState>,
  ) {}
}
