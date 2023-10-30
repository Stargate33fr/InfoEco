import { ChangeDetectorRef, Component, OnDestroy, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AppartementService } from '@core/services/appartement.service';
import { DonneesReferencesService } from '@core/services/donnees-references.service';
import { AjoutModifierAppartement, Appartement, AppartementAjoutRetour } from '@models/appartement';
import { ISelect } from '@models/select';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { Observable, Subject, map, takeUntil } from 'rxjs';

interface IModalData {
  appartementId: number;
}

@Component({
  selector: 'organismes-modal-appartement',
  templateUrl: './organismes-modal-appartement.component.html',
  styleUrls: ['./organismes-modal-appartement.component.less'],
})
export class OrganismesModalAppartementComponent implements OnInit, OnDestroy {
  appartementForm: FormGroup;
  loadingSave = false;
  typeAppartement$: Observable<ISelect[]>;
  confirmDisabled: boolean = true;
  appartementId: number;
  nomVille: string;
  readonly nzModalData: IModalData = inject(NZ_MODAL_DATA);

  private destroy$ = new Subject<void>();

  constructor(
    private donneesReferencesService: DonneesReferencesService,
    private appartementService: AppartementService,
    private message: NzMessageService,
    private fb: FormBuilder,
    private modal: NzModalRef,
    private ref: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    if (this.nzModalData && this.nzModalData.appartementId) {
      this.appartementId = this.nzModalData.appartementId ?? null;
    }

    this.typeAppartement$ = this.donneesReferencesService.typeAppartement();

    this.appartementForm = this.fb.group({
      adresse: [null, [Validators.required]],
      complementAdresse: [null],
      villeId: [null, [Validators.required]],
      loyer: [null, [Validators.required]],
      depotDeGarantie: [null, [Validators.required]],
      prixDesCharges: [null, [Validators.required]],
      nomResidence: [null],
      numeroAppartement: [null],
      nombreDeM2: [null, [Validators.required]],
      typeAppartementId: [null, [Validators.required]],
    });

    if (this.appartementId) {
      this.appartementService
        .obtenirAppartement(this.appartementId)
        .pipe(
          takeUntil(this.destroy$),
          map((res: Appartement) => {
            this.appartementForm.controls['adresse'].setValue(res.adresse);
            this.appartementForm.controls['complementAdresse'].setValue(res.complementAdresse);
            this.appartementForm.controls['villeId'].setValue(res.ville.id);
            this.appartementForm.controls['loyer'].setValue(res.loyer);
            this.appartementForm.controls['depotDeGarantie'].setValue(res.depotDeGarantie);
            this.appartementForm.controls['prixDesCharges'].setValue(res.prixDesCharges);
            this.appartementForm.controls['nomResidence'].setValue(res.nomResidence);
            this.appartementForm.controls['numeroAppartement'].setValue(res.numeroAppartement);
            this.appartementForm.controls['nombreDeM2'].setValue(res.nombreDeMetre2);
            this.appartementForm.controls['typeAppartementId'].setValue(res.typeAppartement.id);
            this.nomVille = res.ville.nom;

            this.confirmDisabled = false;
            this.ref.markForCheck();
          }),
        )
        .subscribe();
    }
  }

  valeurValide() {
    this.confirmDisabled = !this.appartementForm.valid;
    if (this.confirmDisabled) {
      Object.values(this.appartementForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
    this.ref.markForCheck();
  }

  confirm() {
    if (!this.confirmDisabled) {
      this.loadingSave = true;
      const valeur = this.appartementForm.value;

      var appartement = new AjoutModifierAppartement();
      appartement.adresse = valeur.adresse;
      appartement.complementAdresse = valeur.complementAdresse;
      appartement.villeId = valeur.villeId;
      appartement.prixDesCharges = valeur.prixDesCharges;
      appartement.loyer = valeur.loyer;
      appartement.depotDeGarantie = valeur.depotDeGarantie;
      appartement.nombreDeMetre2 = valeur.nombreDeM2;
      appartement.typeAppartementId = valeur.typeAppartementId;
      appartement.numeroAppartement = valeur.numeroAppartement;
      appartement.nomResidence = valeur.nomResidence;

      if (this.nomVille) {
        this.appartementService.modifierAppartement(this.appartementId, appartement).subscribe({
          next: () => {
            setTimeout(() => {
              this.message.create('success', 'Enregistrement effectué');
              this.modal.close(true);
            }, 1000);
          },
          error: () => {
            this.message.create('error', "[APPARTEMENT] une erreur s'est produit pendant l'enregistrement");
          },
        });
      } else {
        this.appartementService.ajouterAppartement(appartement).subscribe((res: AppartementAjoutRetour) => {
          this.modal.close(res.id);
        });
      }
    }
  }

  close() {
    this.modal.destroy(false);
  }

  ngOnDestroy(): void {
    this.ref.detach();
    this.destroy$.next();
    this.destroy$.complete();
  }
}
