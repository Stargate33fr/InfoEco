import { ChangeDetectorRef, Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { RoutingKeys } from '@core/rounting/routing-keys';
import { AgenceImmobiliereService } from '@core/services/agenceImmobiliere.service';
import { AppartementService } from '@core/services/appartement.service';
import { DonneesReferencesService } from '@core/services/donnees-references.service';
import { LocataireAppartementService } from '@core/services/locataireAppartement.service';
import { getUtilisateur } from '@core/store/selectors/utilisateur.selector';
import { IAppState } from '@core/store/state/app.state';
import { Appartement, Appartements, FilterAppartement, FilterLocataireAppartement } from '@models/appartement';
import { ILocataire, Locataire } from '@models/locataire';
import { ILocataireAppartement, LocataireAppartement } from '@models/locataireAppartement';
import { ISelect } from '@models/select';
import { ColumnItem, DataItem } from '@models/tableau';
import { IUser, User } from '@models/user';
import { Store, select } from '@ngrx/store';
import { OrganismesModalAppartementComponent } from '@organismes/organismes-modal-appartement/organismes-modal-appartement.component';
import { OrganismesModalLocataireComponent } from '@organismes/organismes-modal-locataire/organismes-modal-locataire.component';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzTableComponent } from 'ng-zorro-antd/table';
import { Observable, Subject, map, switchMap, takeUntil, tap } from 'rxjs';

@Component({
  selector: 'organismes-dashboard',
  templateUrl: './organismes-dashboard.component.html',
  styleUrls: ['./organismes-dashboard.component.less'],
})
export class OrganismesDashboardComponent implements OnInit, OnDestroy {
  @Input() utilisateur: User;
  @ViewChild('virtualTable', { static: false }) nzTableComponent?: NzTableComponent<DataItem>;

  typeAppartement$: Observable<ISelect[]>;
  rechercheForm: FormGroup;
  loading: boolean = true;
  listOfColumns: ColumnItem[] = [];
  listOfData: DataItem[] = [];
  nombreAppartement: number;
  nombreElementParPage: number = 20;
  page: number = 1;
  checked = false;
  setOfCheckedId = new Set<number>();
  totalUtilisateur: number;
  totalAffiche: number = 0;
  nombreUtilisateur: number;
  indeterminate = false;
  allChecked: boolean = false;
  filter: FilterAppartement;
  expandSet = new Set<number>();
  detailsAppartement: ILocataireAppartement[] = [];
  isCollapse = true;

  private destroy$ = new Subject<void>();

  constructor(
    private router: Router,
    private donneesReferencesService: DonneesReferencesService,
    private agenceImmobiliereService: AgenceImmobiliereService,
    private locataireAppartementService: LocataireAppartementService,
    private appartementService: AppartementService,
    protected store: Store<IAppState>,
    private ref: ChangeDetectorRef,
    private message: NzMessageService,
    private modalService: NzModalService,
    private fb: FormBuilder,
  ) {}

  ngOnInit() {
    this.typeAppartement$ = this.donneesReferencesService.typeAppartement();
    this.rechercheForm = this.fb.group({
      ville: [null],
      nomLocataire: [null],
      typeAppartement: [null],
    });
    this.remplirColumn('RESIDENCE', '200px');
    this.remplirColumn('ADRESSE', '200px');
    this.remplirColumn('CODE POSTAL', '150px');
    this.remplirColumn('VILLE', '200px');
    this.remplirColumn('N° APPART.', '100px');
    this.remplirColumn('TYPE APPARTEMENT', '140px');
    this.remplirColumn('NOMBRE DE m²', '160px');
    this.remplirColumn('ID', '90px');
    this.remplirColumn('', '90px', false, false, false);
    this.filter = new FilterAppartement();
    this.filter.agenceImmobiliereId = this.utilisateur.agenceImmobiliereId;
    this.getAppartements();
  }

  getAppartements() {
    this.loading = true;
    this.ref.markForCheck();
    this.agenceImmobiliereService
      .getAppartementsAgence(this.filter, this.nombreElementParPage * this.page, this.page, 'id', false)
      .pipe(
        takeUntil(this.destroy$),
        map((res: Appartements) => {
          this.nombreAppartement = Number(res.total);
          res.valeur.map((u) => {
            const data = new DataItem();
            data.checked = false;
            data.id = u.id;
            data.adresse = u.adresse;
            data.loyer = u.loyer;
            data.complementAdresse = u.complementAdresse;
            data.ville = u.ville.nom;
            data.typeAppartement = u.typeAppartement.nom;
            data.codePostal = u.ville.codePostal;
            data.nombreDeMetre2 = u.nombreDeMetre2;
            data.numeroAppartement = u.numeroAppartement;
            data.nomResidence = u.nomResidence;

            this.listOfData = [...this.listOfData, data];
            this.totalAffiche = res.valeur.length;

            this.loading = false;
            this.ref.markForCheck();
          });
        }),
      )
      .subscribe();
  }

  ouvrirPaiement(locataireAppartementId: number, locataireId: number) {
    this.router.navigate([RoutingKeys.locataire, locataireAppartementId, locataireId]);
  }

  onExpandChange(id: number, checked: boolean): void {
    if (checked) {
      const filter = new FilterLocataireAppartement();
      filter.appartementId = id;

      this.locataireAppartementService
        .rechercheDetailAppartements(filter)
        .pipe(takeUntil(this.destroy$))
        .subscribe((res) => {
          var locataire = new Locataire();
          locataire.id = -id;
          if (res && res.length > 0) {
            this.detailsAppartement = this.detailsAppartement.filter((u) => u.appartementId != id);
            res[0].locataires = [...res[0].locataires, locataire];
            this.detailsAppartement = [...this.detailsAppartement, res[0]];
          } else {
            var locataireAppartement = new LocataireAppartement();
            locataireAppartement.appartementId = id;
            locataireAppartement.locataires = [];

            locataireAppartement.locataires = [...locataireAppartement.locataires, locataire];
            this.detailsAppartement = this.detailsAppartement.filter((u) => u.appartementId != id);
            this.detailsAppartement = [...this.detailsAppartement, locataireAppartement];
          }
          this.expandSet.add(id);
          this.ref.markForCheck();
        });
    } else {
      this.expandSet.delete(id);
    }
  }

  public nomLocataire(idAppartement: number) {
    if (this.detailsAppartement.length > 0 && idAppartement) {
      return this.detailsAppartement.find((u) => u.appartementId == idAppartement)?.locataires;
    }
    return null;
  }

  public getAppartementLocataireId(idLocataire: number, idAppartement: number) {
    if (this.detailsAppartement.length > 0 && idLocataire && idAppartement) {
      if (this.detailsAppartement.find((u) => u.appartementId == idAppartement && u.locataires.find((l) => l.id == idLocataire))) {
        const test = this.detailsAppartement.find((u) => u.appartementId == idAppartement && u.locataires.find((l) => l.id == idLocataire));
        if (test) {
          return test.id;
        } else {
          return -1;
        }
      } else {
        return -1;
      }
    }
    return -1;
  }

  public remplirColumn(
    nomColonne: string,
    width: string,
    estFixeAGauche: boolean = false,
    estFixeADroite: boolean = false,
    activeTri: boolean = true,
  ) {
    const elementList = new ColumnItem();
    elementList.name = nomColonne;
    elementList.sortOrder = null;
    switch (nomColonne) {
      case 'PRENOM':
        elementList.sortFn = (a: DataItem, b: DataItem) => a.adresse.localeCompare(b.adresse);
        break;
      case 'LOYER':
        elementList.sortFn = (a: DataItem, b: DataItem) => a.loyer - b.loyer;
        break;
      case 'TELEPHONE':
        elementList.sortFn = (a: DataItem, b: DataItem) => a.depotDeGarantie - b.depotDeGarantie;
        break;
      case 'ID':
        elementList.sortFn = (a: DataItem, b: DataItem) => a.id - b.id;
        break;
      case 'COMPLEMENT ADRESSE':
        elementList.sortFn = (a: DataItem, b: DataItem) => a.complementAdresse.localeCompare(b.complementAdresse);
        break;
      case 'NOMBRE DE M²':
        elementList.sortFn = (a: DataItem, b: DataItem) => a.nombreDeMetre2 - b.nombreDeMetre2;
        break;
    }
    if (activeTri) {
      elementList.sortDirections = ['ascend', 'descend'];
    } else {
      elementList.sortFn = null;
      elementList.sortOrder = null;
    }

    elementList.filterMultiple = false;
    elementList.width = width;
    elementList.left = estFixeAGauche;
    elementList.right = estFixeADroite;
    this.listOfColumns = [...this.listOfColumns, elementList];
  }

  creerNouveauLocataire(appartement: DataItem) {
    const modal = this.modalService.create({
      nzTitle: "Ajout d'un locataire",
      nzContent: OrganismesModalLocataireComponent,
      nzFooter: null,
      nzClosable: false,
      nzMaskClosable: false,
      nzWidth: '700px',
      nzData: {
        appartement: appartement,
      },
    });

    modal.afterClose.subscribe((res: number) => {
      this.listOfData = [];
      this.loading = true;
      setTimeout(() => {
        this.getAppartements();
        this.onExpandChange(res, true);

        this.ref.markForCheck();
      }, 150);
    });
  }

  modifierLocataire(appartement: DataItem, locataireId: number) {
    const modal = this.modalService.create({
      nzTitle: 'Modification des informations du locataire',
      nzContent: OrganismesModalLocataireComponent,
      nzFooter: null,
      nzClosable: false,
      nzMaskClosable: false,
      nzWidth: '800px',
      nzData: {
        appartement: appartement,
        locataireId: locataireId,
      },
    });
    modal.afterClose.subscribe(() => {
      this.listOfData = [];
      this.loading = true;
      setTimeout(() => {
        this.getAppartements();
        this.onExpandChange(appartement.id, true);

        this.ref.markForCheck();
      }, 150);
    });
  }

  supprimerAppartement(id: number) {
    this.appartementService.supprimerAppartement(id).subscribe({
      next: () => {
        setTimeout(() => {
          this.message.create('success', 'Suppression effectué');
          this.listOfData = [];
          this.loading = true;
          this.getAppartements();
          this.ref.markForCheck();
        }, 1000);
      },
      error: (erreur) => {
        this.message.create('error', erreur.error.error);
      },
    });
  }

  supprimerLocataire(id: number) {
    this.message.info('La suppression est effectuée');
    this.listOfData = [];
    this.loading = true;
    setTimeout(() => {
      this.getAppartements();
      this.ref.markForCheck();
    }, 150);
  }

  cancel(): void {
    this.message.info('La suppression est annulée');
  }
  modifierAppartement(id: number) {
    const modal = this.modalService.create({
      nzTitle: "Modification d'un appartement",
      nzContent: OrganismesModalAppartementComponent,
      nzFooter: null,
      nzClosable: false,
      nzMaskClosable: false,
      nzWidth: '620px',
      nzData: {
        appartementId: id ?? null,
      },
    });
    modal.afterClose.subscribe((res: boolean) => {
      if (res) {
        this.listOfData = [];
        this.loading = true;
        setTimeout(() => {
          this.getAppartements();
          this.ref.markForCheck();
        }, 150);
      }
    });
  }

  creerNouveauAppartement() {
    const modal = this.modalService.create({
      nzTitle: "Ajout d'un appartement",
      nzContent: OrganismesModalAppartementComponent,
      nzFooter: null,
      nzClosable: false,
      nzMaskClosable: false,
      nzWidth: '620px',
    });
    modal.afterClose.subscribe((res: number) => {
      if (res > 0) {
        this.listOfData = [];
        this.loading = true;
        setTimeout(() => {
          this.getAppartements();
          this.ref.markForCheck();
        }, 150);
      }
    });
  }

  onAllChecked(value: boolean): void {
    this.listOfData.forEach((item) => this.updateCheckedSet(item.id, value));
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    this.checked = this.listOfData.every((item) => this.setOfCheckedId.has(item.id));
    this.indeterminate = this.listOfData.some((item) => this.setOfCheckedId.has(item.id)) && !this.checked;
  }

  updateCheckedSet(id: number, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
    }
  }

  onItemChecked(id: number, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  recherche() {
    this.loading = true;
    this.listOfData = [];
    this.page = 1;
    this.filter = new FilterAppartement();
    this.filter.agenceImmobiliereId = this.utilisateur.agenceImmobiliereId;
    const rechercheFormValue = this.rechercheForm.value;
    if (rechercheFormValue.ville != null) {
      this.filter.nomVille = rechercheFormValue.ville;
    }
    if (rechercheFormValue.nomLocataire != null) {
      this.filter.nomLocataire = rechercheFormValue.nomLocataire;
    }
    if (rechercheFormValue.typeAppartement != null) {
      this.filter.typeAppartementId = rechercheFormValue.typeAppartement;
    }
    this.agenceImmobiliereService
      .getAppartementsAgence(this.filter, this.nombreElementParPage, this.page, 'id', false)
      .pipe(
        takeUntil(this.destroy$),
        map((res: Appartements) => {
          this.nombreAppartement = Number(res.total);
          if (res.valeur.length == 0) {
            this.loading = false;
            this.ref.markForCheck();
          } else {
            res.valeur.map((u) => {
              const data = new DataItem();
              data.checked = false;
              data.id = u.id;
              data.adresse = u.adresse;
              data.loyer = u.loyer;
              data.complementAdresse = u.complementAdresse;
              data.ville = u.ville.nom;
              data.typeAppartement = u.typeAppartement.nom;
              data.codePostal = u.ville.codePostal;
              data.nombreDeMetre2 = u.nombreDeMetre2;
              data.numeroAppartement = u.numeroAppartement;
              data.nomResidence = u.nomResidence;

              this.listOfData = [...this.listOfData, data];
              this.totalAffiche = res.valeur.length;

              this.loading = false;
              this.ref.markForCheck();
            });
          }
        }),
      )
      .subscribe();
  }

  ngAfterViewInit(): void {
    this.nzTableComponent?.cdkVirtualScrollViewport?.scrolledIndexChange.pipe(takeUntil(this.destroy$)).subscribe((data: number) => {
      //console.log('index', data);
      if (
        this.totalAffiche < Number(this.nombreAppartement) &&
        data > 0 &&
        ((this.page > 0 && data > this.page * this.nombreElementParPage) || (this.page == 0 && data > 15))
      ) {
        this.loading = true;
        this.ref.markForCheck();
        this.page = this.page + 1;
        const filter = new FilterAppartement();
        filter.agenceImmobiliereId = 1;
        this.agenceImmobiliereService
          .getAppartementsAgence(filter, this.nombreElementParPage, this.page, 'Id', false)
          .pipe(takeUntil(this.destroy$))
          .subscribe((res) => {
            this.nombreUtilisateur = Number(res.total);
            this.totalAffiche += res.valeur.length;
            res.valeur.forEach((u) => {
              const data = new DataItem();
              data.checked = false;
              data.id = u.id;
              data.adresse = u.adresse;
              data.loyer = u.loyer;
              data.complementAdresse = u.complementAdresse;
              data.ville = u.ville.nom;
              data.typeAppartement = u.typeAppartement.nom;
              data.codePostal = u.ville.codePostal;
              data.nombreDeMetre2 = u.nombreDeMetre2;
              data.numeroAppartement = u.numeroAppartement;
              data.nomResidence = u.nomResidence;
              this.listOfData = [...this.listOfData, data];
            });
            this.loading = false;
            this.ref.markForCheck();
          });
      }
    });
  }

  resetForm(): void {
    this.rechercheForm.reset();
  }

  toggleCollapse(): void {
    this.isCollapse = !this.isCollapse;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
