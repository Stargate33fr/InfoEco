/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LocataireComponent } from './locataire.component';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { TemplatesLocataireEtatDesLieuxModule } from '@templates/templates-locataire-etat-des-lieux/templates-locataire-etat-des-lieux.module';
import { TemplatesLocatairePaiementModule } from '@templates/templates-locataire-paiement/templates-locataire-paiement.module';
import { TemplatesLocatairesQuittanceDeLoyerModule } from '@templates/templates-locataires-quittance-de-loyer/templates-locataires-quittance-de-loyer.module';
import { GridsterModule } from 'angular-gridster2';

describe('LocataireComponent', () => {
  let component: LocataireComponent;
  let fixture: ComponentFixture<LocataireComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LocataireComponent],
      imports: [
        TemplatesLocataireEtatDesLieuxModule,
        TemplatesLocatairePaiementModule,
        TemplatesLocatairesQuittanceDeLoyerModule,
        GridsterModule,
        SharedTestingModule,
      ],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LocataireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
