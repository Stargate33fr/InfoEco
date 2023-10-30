import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LocataireComponent } from './locataire.component';
import { RoutingKeys } from '@core/rounting/routing-keys';

const routes: Routes = [
  {
    path: '',
    component: LocataireComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LocataireRoutingModule {}
