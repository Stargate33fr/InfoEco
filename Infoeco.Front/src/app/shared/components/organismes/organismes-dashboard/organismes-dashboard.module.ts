import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { OrganismesDashboardComponent } from './organismes-dashboard.component';
import { RouterModule } from '@angular/router';
import { OrganismesModalLocataireModule } from '@organismes/organismes-modal-locataire/organismes-modal-locataire.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MoleculesSelectModule } from '../../molecules/molecules-select/molecules-select.module';
import { MoleculesInputModule } from '../../molecules/molecules-input/molecules-input.module';
import { OrganismesModalAppartementModule } from '@organismes/organismes-modal-appartement/organismes-modal-appartement.module';

@NgModule({
  declarations: [OrganismesDashboardComponent],
  exports: [OrganismesDashboardComponent],
  imports: [
    CommonModule,
    NgZorroAntdModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    OrganismesModalLocataireModule,
    OrganismesModalAppartementModule,
    MoleculesSelectModule,
    MoleculesInputModule,
  ],
})
export class OrganismesDashboardModule {}
