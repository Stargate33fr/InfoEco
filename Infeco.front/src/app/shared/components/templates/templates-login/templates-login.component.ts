import { ChangeDetectionStrategy, Component, OnDestroy, OnInit } from '@angular/core';
import { environment } from '@environment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RoutingKeys } from '@core/rounting/routing-keys';
import { LogIn } from '@core/store/actions/auth.action';
import { getAuthState } from '@core/store/selectors/auth.selector';
import { IAppState } from '@core/store/state/app.state';
import { select, Store } from '@ngrx/store';
import { BehaviorSubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { customValidators } from '@core/validators/custom-validators';

@Component({
  selector: 'templates-login',
  templateUrl: './templates-login.component.html',
  styleUrls: ['./templates-login.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TemplatesLoginComponent implements OnInit, OnDestroy {
  loginForm!: FormGroup;
  loading$ = new BehaviorSubject<boolean>(false);

  routingKeys = RoutingKeys;

  annee: number;
  headerClass = `login-header-${environment.context.app.env}`;

  private destroy$ = new Subject<void>();

  constructor(private fb: FormBuilder, private store: Store<IAppState>, private router: Router) {}

  ngOnInit() {
    this.annee = new Date().getFullYear();
    this.loginForm = this.fb.group({
      email: [null, [customValidators.email, Validators.required]],
      password: [null, [Validators.minLength(1), Validators.required]],
    });

    this.store
      .pipe(select(getAuthState))
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.loading$.next(false);
      });
  }

  valid(): void {
    this.loading$.next(true);
    const value = this.loginForm.value;
    this.store.dispatch(new LogIn({ identifiant: value.email, password: value.password }));
  }

  // motDePasseOublie() {
  //   this.router.navigate([`/${this.routingKeys.forgottenPassword}`]);
  // }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
