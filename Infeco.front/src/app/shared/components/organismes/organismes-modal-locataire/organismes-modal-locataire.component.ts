import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DonneesReferencesService } from '@core/services/donnees-references.service';
import { LocataireService } from '@core/services/locataire.service';
import { LocataireAppartementService } from '@core/services/locataireAppartement.service';
import { customValidators } from '@core/validators/custom-validators';
import { FilterLocataireAppartement } from '@models/appartement';
import { AjoutModifierLocataire, Locataire, LocataireAjoutRetour } from '@models/locataire';
import { AjoutLocataire, AssignerLocataire, ILocataireAppartement, LocataireAppartementAjoutRetour } from '@models/locataireAppartement';
import { ChoixOuiNon, ISelect, Select } from '@models/select';
import { DataItem } from '@models/tableau';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NZ_MODAL_DATA, NzModalRef } from 'ng-zorro-antd/modal';
import { Observable, Subject, map, of, switchMap, takeUntil, tap } from 'rxjs';

interface IModalData {
  appartement: DataItem;
  locataireId: number;
}

@Component({
  selector: 'organismes-modal-locataire',
  templateUrl: './organismes-modal-locataire.component.html',
  styleUrls: ['./organismes-modal-locataire.component.less'],
})
export class OrganismesModalLocataireComponent implements OnInit {
  appartementId: number;
  appartement: DataItem;

  readonly nzModalData: IModalData = inject(NZ_MODAL_DATA);

  locataireForm: FormGroup;
  appartementForm: FormGroup;
  erreurDejaPresent: boolean = false;
  statutMail: string = 'error';
  statutPrenom: string = 'error';
  statutNom: string = 'error';
  civilite$: Observable<ISelect[]>;
  loadingSave = false;
  current = 0;
  nomLocataire: string = '';
  locataireAjout: AjoutLocataire;
  isSpinningLocataire: boolean = false;
  radioValue = 'A';
  afficheRecherche: boolean = false;
  choixOuiNon: ISelect[] = [];
  choixOuiNon$: Observable<ISelect[]>;
  assignerLocataire: AssignerLocataire;
  locataireId?: number;

  @ViewChild('inputElementTelephone', { static: false }) inputElementTelephone: ElementRef;

  private destroy$ = new Subject<void>();

  constructor(
    private donneesReferencesService: DonneesReferencesService,
    private locataireService: LocataireService,
    private locataireAppartementService: LocataireAppartementService,
    private message: NzMessageService,
    private fb: FormBuilder,
    private modal: NzModalRef,
    private ref: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.civilite$ = this.donneesReferencesService.civilite();
    this.appartement = this.nzModalData.appartement;
    this.locataireId = this.nzModalData.locataireId ?? null;
    [ChoixOuiNon.oui, ChoixOuiNon.non].map((u) => {
      const choix = new Select();
      choix.id = u;
      choix.nom = u == 0 ? 'Non' : 'Oui';

      this.choixOuiNon = [...this.choixOuiNon, choix];
    });
    this.locataireForm = this.fb.group({
      civilite: [null, [Validators.required]],
      nom: [null, [Validators.required]],
      prenom: [null, [Validators.required]],
      mail: [null, [customValidators.email]],
      dateDeNaissance: [null, [Validators.required]],
      telephone: [null, [Validators.required]],
      iban: [null, [Validators.required]],
    });
    this.choixOuiNon$ = of(this.choixOuiNon);
    this.appartementId = this.nzModalData.appartement.id;
    if (this.locataireId) {
      this.locataireService
        .obtenirLocataire(this.locataireId)
        .pipe(
          takeUntil(this.destroy$),
          map((res: Locataire) => {
            this.locataireForm.controls['civilite'].setValue(res.civiliteId);
            this.locataireForm.controls['nom'].setValue(res.nom);
            this.locataireForm.controls['prenom'].setValue(res.prenom);
            this.locataireForm.controls['mail'].setValue(res.mail);
            this.locataireForm.controls['dateDeNaissance'].setValue(res.dateNaissance);
            this.locataireForm.controls['telephone'].setValue(res.telephone);
            this.locataireForm.controls['iban'].setValue(res.iban);
            this.ref.markForCheck();
          }),
        )
        .subscribe();
    }

    this.appartementForm = this.fb.group({
      idAppartement: [this.appartementId, [Validators.required]],
      depotDeGarentieRegle: [null, [Validators.required]],
      dateEntree: [null, [Validators.required]],
    });

    if (this.locataireId) {
      const filter = new FilterLocataireAppartement();
      filter.locataireId = this.locataireId;
      this.locataireAppartementService
        .rechercheDetailAppartements(filter)
        .pipe(
          takeUntil(this.destroy$),
          map((res: ILocataireAppartement[]) => {
            if (res && res.length > 0) {
              this.appartementForm.controls['depotDeGarentieRegle'].setValue(
                res[0].depotDeGarantieRegle ? ChoixOuiNon.oui : ChoixOuiNon.non,
              );
              this.appartementForm.controls['dateEntree'].setValue(res[0].locataires[0].dateEntree);
            }
            this.ref.markForCheck();
          }),
        )
        .subscribe();
    }
  }

  valeurValide() {
    const locataireFormValue = this.locataireForm.value;
    const appartementFormValue = this.appartementForm.value;

    if (
      this.current === 1 &&
      appartementFormValue.idAppartement != '' &&
      appartementFormValue.idAppartement != null &&
      appartementFormValue.dateEntree != '' &&
      appartementFormValue.dateEntree != null &&
      appartementFormValue.depotDeGarentieRegle != '' &&
      appartementFormValue.depotDeGarentieRegle != null
    ) {
      this.assignerLocataire = new AssignerLocataire();
      this.assignerLocataire.depotDeGarantieRegle = appartementFormValue.depotDeGarentieRegle;
      this.assignerLocataire.appartementId = this.appartementId;
      this.assignerLocataire.dateEntree = appartementFormValue.dateEntree;
    }

    if (
      this.current === 0 &&
      locataireFormValue.telephone != '' &&
      locataireFormValue.telephone != null &&
      locataireFormValue.civilite != '' &&
      locataireFormValue.civilite != null &&
      locataireFormValue.prenom != '' &&
      locataireFormValue.prenom != null &&
      locataireFormValue.nom != '' &&
      locataireFormValue.nom != null &&
      locataireFormValue.mail != '' &&
      locataireFormValue.mail != null &&
      locataireFormValue.dateDeNaissance != '' &&
      locataireFormValue.dateDeNaissance != null &&
      locataireFormValue.iban != '' &&
      locataireFormValue.iban != null
    ) {
      this.locataireAjout = new AjoutLocataire();

      this.locataireAjout.mail = locataireFormValue.mail;
      this.locataireAjout.telephone = locataireFormValue.telephone;
      this.locataireAjout.dateDeNaissance = locataireFormValue.dateDeNaissance;
      this.locataireAjout.iban = locataireFormValue.iban;
      this.locataireAjout.nom = locataireFormValue.nom;
      this.locataireAjout.prenom = locataireFormValue.prenom;
      this.locataireAjout.civiliteId = locataireFormValue.civilite;
    }
  }

  getStatutMail() {
    if (this.locataireForm.controls['email'].value !== '' && !this.locataireForm.controls['email'].valid) {
      this.statutMail = 'error';
    } else {
      this.statutMail = 'success';
    }
    return this.statutMail;
  }

  getStatutNom() {
    const contactForm = this.locataireForm.value;
    if (contactForm.nom === null || contactForm.nom === '') {
      this.statutNom = 'error';
    } else {
      this.statutNom = 'success';
    }
    return this.statutPrenom;
  }

  confirm() {
    // this.loadingSave = true;

    this.current += 1;
  }

  enregistrement() {
    this.loadingSave = true;
    const ajoutModifierLocataire = new AjoutModifierLocataire();
    ajoutModifierLocataire.civiliteId = this.locataireAjout.civiliteId;
    ajoutModifierLocataire.nom = this.locataireAjout.nom;
    ajoutModifierLocataire.prenom = this.locataireAjout.prenom;
    ajoutModifierLocataire.iban = this.locataireAjout.iban;
    ajoutModifierLocataire.mail = this.locataireAjout.mail;
    ajoutModifierLocataire.telephone = this.locataireAjout.telephone;
    ajoutModifierLocataire.dateNaissance = this.locataireAjout.dateDeNaissance;

    if (this.locataireId) {
      this.locataireService
        .modifierLocataire(this.locataireId, ajoutModifierLocataire)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            setTimeout(() => {
              this.message.create('success', 'Enregistrement effectué');
              this.modal.close(true);
            }, 1000);
          },
          error: () => {
            this.message.create('error', "[LOCATAIRE] une erreur s'est produit pendant l'enregistrement");
          },
        });
    } else {
      this.locataireService
        .ajouterLocataire(ajoutModifierLocataire)
        .pipe(
          takeUntil(this.destroy$),
          map((res: LocataireAjoutRetour) => {
            this.assignerLocataire.locataireId = res.id;
          }),
          switchMap(() => this.locataireAppartementService.assignerLocataireAUnAppartement(this.assignerLocataire)),
          tap((res: LocataireAppartementAjoutRetour) => {
            if (res.id) {
              this.message.create('success', 'Enregistrement effectué');
              this.modal.close(res.id);
            }
          }),
        )
        .subscribe();
    }
  }

  precedent() {
    this.isSpinningLocataire = true;
    this.current = this.current - 1;
    setTimeout(() => {
      this.locataireForm.controls['civilite'].setValue(this.locataireAjout.civiliteId);
      this.locataireForm.controls['nom'].setValue(this.locataireAjout.nom);
      this.locataireForm.controls['prenom'].setValue(this.locataireAjout.prenom);
      this.locataireForm.controls['iban'].setValue(this.locataireAjout.iban);
      this.locataireForm.controls['mail'].setValue(this.locataireAjout.mail);
      this.locataireForm.controls['dateDeNaissance'].setValue(this.locataireAjout.dateDeNaissance);
      this.locataireForm.controls['telephone'].setValue(this.locataireAjout.telephone);
      this.isSpinningLocataire = false;
      this.ref.markForCheck();
    }, 1000);
  }

  affichageAppartementChoix($event: string) {
    switch ($event) {
      case 'A':
        this.afficheRecherche = true;
        this.ref.markForCheck();
        break;
      case 'B':
        this.afficheRecherche = false;
        this.ref.markForCheck();
        break;
      case 'C':
        this.afficheRecherche = false;
        this.ref.markForCheck();
        break;
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
