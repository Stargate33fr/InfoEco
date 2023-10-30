import { Routes } from '@angular/router';
import { RoutingKeys } from '@core/rounting/routing-keys';
import { LoggedUsersGuard } from '@core/security/guards/logged-users-guard';
import { UnLoggedUsersGuard } from '@core/security/guards/unlogged-users-guard';
import { PagesComponent } from '@pages/pages.component';

export const appRoutes: Routes = [
  {
    path: '',
    redirectTo: RoutingKeys.dashboard,
    pathMatch: 'full',
  },
  {
    path: RoutingKeys.login,
    loadChildren: () => import('./pages/login/login.module').then((m) => m.LoginModule),
    canActivate: [UnLoggedUsersGuard],
  },
  {
    path: '',
    component: PagesComponent,
    canActivate: [LoggedUsersGuard],
    children: [
      {
        path: RoutingKeys.dashboard,
        loadChildren: () => import('@pages/dashboard/dashboard.module').then((m) => m.DashboardModule),
      },
      {
        path: `${RoutingKeys.locataire}/:locataireAppartementId/:locataireId`,
        loadChildren: () => import('@pages/locataire/locataire.module').then((m) => m.LocataireModule),
      },
    ],
  },
];
