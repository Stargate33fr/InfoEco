import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LocataireComponent } from './locataire.component';
import { LocataireRoutingModule } from './locataire-routing.module';
import { TemplatesLocataireEtatDesLieuxModule } from '@templates/templates-locataire-etat-des-lieux/templates-locataire-etat-des-lieux.module';
import { TemplatesLocatairesQuittanceDeLoyerModule } from '@templates/templates-locataires-quittance-de-loyer/templates-locataires-quittance-de-loyer.module';
import { GridsterModule } from 'angular-gridster2';
import { TemplatesLocatairePaiementModule } from '@templates/templates-locataire-paiement/templates-locataire-paiement.module';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';

@NgModule({
  declarations: [LocataireComponent],
  imports: [
    CommonModule,
    LocataireRoutingModule,
    TemplatesLocataireEtatDesLieuxModule,
    TemplatesLocatairePaiementModule,
    TemplatesLocatairesQuittanceDeLoyerModule,
    GridsterModule,
    NgZorroAntdModule,
  ],
})
export class LocataireModule {}
