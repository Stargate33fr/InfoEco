import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormatTelPipe } from '@core/pipes/formatTel.pipe';
import { DonneesReferencesService } from '@core/services/donnees-references.service';
import { customValidators } from '@core/validators/custom-validators';
import { AjoutLocataire } from '@models/locataireAppartement';
import { ISelect } from '@models/select';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { Observable } from 'rxjs';

@Component({
  selector: 'organismes-modal-locataire',
  templateUrl: './organismes-modal-locataire.component.html',
  styleUrls: ['./organismes-modal-locataire.component.less'],
})
export class OrganismesModalLocataireComponent implements OnInit {
  locataireForm: FormGroup;
  AppartementForm: FormGroup;
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
  radioValue: string = 'A';
  afficheRecherche: boolean = false;

  @ViewChild('inputElementTelephone', { static: false }) inputElementTelephone: ElementRef;

  constructor(
    private donneesReferencesService: DonneesReferencesService,
    private fb: FormBuilder,
    private modal: NzModalRef,
    private ref: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.civilite$ = this.donneesReferencesService.civilite();
    this.locataireForm = this.fb.group({
      civilite: [null, [Validators.required]],
      nom: [null, [Validators.required]],
      prenom: [null, [Validators.required]],
      mail: [null, [customValidators.email]],
      dateDeNaissance: [null, [Validators.required]],
      telephone: [null, [Validators.required]],
      iban: [null, [Validators.required]],
    });

    this.AppartementForm = this.fb.group({
      idAppartement: [null, [Validators.required]],
      depotDeGarentieRegle: [false, [Validators.required]],
      dateEntree: [new Date(), [Validators.required]],
    });
  }

  valeurValide() {
    const locataireFormValue = this.locataireForm.value;
    if (
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

      this.locataireAjout.adresseMail = locataireFormValue.mail;
      this.locataireAjout.telephone = locataireFormValue.telephone;
      this.locataireAjout.dateDeNaissance = locataireFormValue.dateDeNaissance;
      this.locataireAjout.iban = locataireFormValue.iban;
      this.locataireAjout.nom = locataireFormValue.nom;
      this.locataireAjout.prenom = locataireFormValue.prenom;
      this.locataireAjout.idCivilite = locataireFormValue.civilite;
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
    this.loadingSave = false;

    this.current = 1;
  }

  precedent() {
    this.isSpinningLocataire = true;
    this.current = this.current - 1;
    setTimeout(() => {
      this.locataireForm.controls['civilite'].setValue(this.locataireAjout.idCivilite);
      this.locataireForm.controls['nom'].setValue(this.locataireAjout.nom);
      this.locataireForm.controls['prenom'].setValue(this.locataireAjout.prenom);
      this.locataireForm.controls['iban'].setValue(this.locataireAjout.iban);
      this.locataireForm.controls['mail'].setValue(this.locataireAjout.adresseMail);
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
        break;
      case 'C':
        this.afficheRecherche = false;
        break;
    }
  }

  close() {
    this.modal.destroy(false);
  }

  ngOnDestroy(): void {
    this.ref.detach();
  }
}
