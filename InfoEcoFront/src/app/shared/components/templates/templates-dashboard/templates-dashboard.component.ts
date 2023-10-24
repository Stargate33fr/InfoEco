import { ChangeDetectionStrategy, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { RoutingKeys } from '@core/rounting/routing-keys';
import { getUtilisateur } from '@core/store/selectors/utilisateur.selector';
import { IAppState } from '@core/store/state/app.state';
import { User } from '@models/user';
import { Store, select } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';

@Component({
  selector: 'templates-dashboard',
  templateUrl: './templates-dashboard.component.html',
  styleUrls: ['./templates-dashboard.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TemplatesDashboardComponent implements OnInit, OnDestroy {
  utilisateur$: Observable<User>;
  destroy$: Subject<void> = new Subject<void>();
  routingKeys = RoutingKeys;

  constructor(private router: Router, protected store: Store<IAppState>) {}

  ngOnInit(): void {
    this.utilisateur$ = this.store.pipe(select(getUtilisateur));
  }

  ouvrirChoixCalculatrices() {
    this.router.navigate([this.routingKeys.calculatrices]);
  }

  ouvrirChoixSimulations() {
    this.router.navigate([this.routingKeys.simulations]);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
