import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { RoutingKeys } from '@core/rounting/routing-keys';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    // children: [
    //   {
    //     path: RoutingKeys.calculatrices,
    //     loadChildren: () => import('../calculatrices/calculatrices.module').then((m) => m.CalculatricesModule),
    //   },
    //   {
    //     path: RoutingKeys.simulations,
    //     loadChildren: () => import('../simulations/simulations.module').then((m) => m.SimulationsModule),
    //   },
    // ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
