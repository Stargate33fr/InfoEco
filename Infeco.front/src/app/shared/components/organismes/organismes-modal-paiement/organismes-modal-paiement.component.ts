import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FrenchMonthPipe } from '@core/pipes/frenchMonth.pipe';
import { DonneesReferencesService } from '@core/services/donnees-references.service';
import { LocataireAppartementService } from '@core/services/locataireAppartement.service';
import { PaiementService } from '@core/services/paiement.service';
import { FilterLocataireAppartement } from '@models/appartement';
import { ILocataireAppartement } from '@models/locataireAppartement';
import { LocatairePaiementAjout, Paiement } from '@models/paiement';
import { ISelect, Select } from '@models/select';
import { DataItem } from '@models/tableau';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { Observable, Subject, map, of, switchMap, takeUntil, tap } from 'rxjs';

interface IModalData {
  locataireAppartementId: number;
}

@Component({
  selector: 'app-organismes-modal-paiement',
  templateUrl: './organismes-modal-paiement.component.html',
  styleUrls: ['./organismes-modal-paiement.component.less'],
})
export class OrganismesModalPaiementComponent implements OnInit {
  readonly nzModalData: IModalData = inject(NZ_MODAL_DATA);

  typePaiement$: Observable<ISelect[]>;
  moisPaiement$: Observable<ISelect[]>;
  anneePaiement$: Observable<ISelect[]>;
  paiementForm: FormGroup;
  loadingSave: boolean;

  annee: ISelect[] = [];
  mois: ISelect[] = [];
  currentYear: number = new Date().getFullYear();
  maxYear: number = 2100;
  yearList: number[] = [];
  locataireAppartementId: number;
  genererQuittanceLoyer: boolean = false;
  montantPaiement: number;
  montantCaf: number;
  disabled: boolean = true;

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private modal: NzModalRef,
    private donneesReferencesService: DonneesReferencesService,
    private locataireAppartementService: LocataireAppartementService,
    private ref: ChangeDetectorRef,
    private frenchMonthPipe: FrenchMonthPipe,
    private paiementService: PaiementService,
    private message: NzMessageService,
  ) {}

  ngOnInit() {
    this.locataireAppartementId = this.nzModalData.locataireAppartementId;

    for (let year = this.currentYear; year <= this.maxYear; year++) {
      this.yearList = [...this.yearList, year];
    }
    this.yearList.map((u: number) => {
      const select = new Select();
      select.id = u;
      select.nom = u.toString();
      this.annee = [...this.annee, select];
    });

    [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12].map((u: number) => {
      const select = new Select();
      select.id = u;
      select.nom = this.frenchMonthPipe.transform(u);
      this.mois = [...this.mois, select];
    });

    this.moisPaiement$ = of(this.mois);
    this.anneePaiement$ = of(this.annee);

    this.typePaiement$ = this.donneesReferencesService.typePaiement();
    this.paiementForm = this.fb.group({
      montant: [null, [Validators.required]],
      typePaiementId: [null, [Validators.required]],
      mois: [null, [Validators.required]],
      annee: [null, [Validators.required]],
      genereQuittanceLoyer: [false, [Validators.required]],
    });
  }

  close() {
    this.modal.destroy(false);
  }

  remplirMontantDepotDeGarantieLoyer(id: number) {
    const paiementValue = this.paiementForm.value;
    const filter = new FilterLocataireAppartement();
    filter.id = this.locataireAppartementId;

    this.typePaiement$.subscribe((res) => {
      if (res) {
        res.map((u: ISelect) => {
          if (u.id == id && u.nom === 'Dépot de garantie' && this.locataireAppartementId) {
            this.locataireAppartementService
              .rechercheDetailAppartements(filter)
              .pipe(takeUntil(this.destroy$))
              .subscribe((res: ILocataireAppartement[]) => {
                this.paiementForm.controls['montant'].setValue(res[0].appartement.depotDeGarantie);
              });
          }
          if (u.id == id && u.nom === 'Loyer') {
            this.montantCaf = 0;
            this.paiementService
              .obtenirTousPaiementLocataire(this.locataireAppartementId)
              .pipe(
                takeUntil(this.destroy$),
                map((res: Paiement[]) => {
                  res.map((u: Paiement) => {
                    if (
                      u.typePaiement.nom == 'CAF' &&
                      new Date(u.datePaiement).getMonth() + 1 == paiementValue.mois &&
                      new Date(u.datePaiement).getFullYear()
                    ) {
                      this.montantCaf += u.montant;
                    }
                  });
                }),
                switchMap(() => this.locataireAppartementService.rechercheDetailAppartements(filter)),
                tap((res: ILocataireAppartement[]) => {
                  var montant =
                    res[0].appartement.loyer - this.montantCaf + (res[0].appartement.loyer * 8) / 100 + res[0].appartement.prixDesCharges;

                  this.paiementForm.controls['montant'].setValue(montant);
                  this.genererQuittanceLoyer = true;

                  this.message.create(
                    'warning',
                    "le prix du loyer a été calculé automatiquement en ajoutant le prix des charges et les frais d'agence et en enlevant le montant de la CAF",
                  );
                  this.ref.markForCheck();
                }),
              )
              .subscribe();
          }
        });
      }
    });
  }

  ajouter() {
    const paiementValue = this.paiementForm.value;
    this.loadingSave = true;

    const locatairePaiementAjout = new LocatairePaiementAjout();
    locatairePaiementAjout.datePaiement = new Date();
    locatairePaiementAjout.locataireAppartementId = this.locataireAppartementId;
    locatairePaiementAjout.montant = paiementValue.montant;
    locatairePaiementAjout.typePaiementId = paiementValue.typePaiementId;
    locatairePaiementAjout.genererQuittanceLoyer = paiementValue.genereQuittanceLoyer;
    locatairePaiementAjout.mois = +paiementValue.mois;
    locatairePaiementAjout.annee = +paiementValue.annee;
    this.paiementService
      .ajouterPaiement(locatairePaiementAjout)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          setTimeout(() => {
            this.message.create('success', 'Enregistrement effectué');
            this.loadingSave = false;

            this.modal.close(true);
          }, 1000);
        },
        error: () => {
          this.message.create('error', "[PAIEMENT] une erreur s'est produit pendant l'enregistrement");
        },
      });
  }

  valideMoisAnnee() {
    const paiementValue = this.paiementForm.value;
    if (paiementValue.mois && paiementValue.annee) {
      this.disabled = false;
    }
  }

  ngOnDestroy(): void {
    this.ref.detach();
    this.destroy$.next();
    this.destroy$.complete();
  }
}
