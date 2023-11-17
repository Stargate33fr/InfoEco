/* tslint:disable:no-unused-variable */
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganismesDashboardComponent } from './organismes-dashboard.component';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';
import { OrganismesModalLocataireModule } from '@organismes/organismes-modal-locataire/organismes-modal-locataire.module';
import { OrganismesModalAppartementModule } from '@organismes/organismes-modal-appartement/organismes-modal-appartement.module';
import { MoleculesSelectModule } from '../../molecules/molecules-select/molecules-select.module';
import { MoleculesInputModule } from '../../molecules/molecules-input/molecules-input.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('OrganismesDashboardComponent', () => {
  let component: OrganismesDashboardComponent;
  let fixture: ComponentFixture<OrganismesDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganismesDashboardComponent],
      imports: [
        SharedTestingModule,
        FormsModule,
        ReactiveFormsModule,
        OrganismesModalLocataireModule,
        OrganismesModalAppartementModule,
        MoleculesSelectModule,
        MoleculesInputModule,
      ],
    });
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganismesDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
