import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { TemplatesLocatairePaiementComponent } from './templates-locataire-paiement.component';
import { RouterModule } from '@angular/router';
import { OrganismesModalPaiementModule } from '@organismes/organismes-modal-paiement/organismes-modal-paiement.module';

@NgModule({
  declarations: [TemplatesLocatairePaiementComponent],
  exports: [TemplatesLocatairePaiementComponent],
  imports: [CommonModule, NgZorroAntdModule, RouterModule, OrganismesModalPaiementModule],
})
export class TemplatesLocatairePaiementModule {}
