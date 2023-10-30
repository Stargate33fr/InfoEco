import { DatePipe } from '@angular/common';
import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { FrenchMonthPipe } from '@core/pipes/frenchMonth.pipe';
import { AppartementService } from '@core/services/appartement.service';
import { LocataireService } from '@core/services/locataire.service';
import { LocataireAppartementService } from '@core/services/locataireAppartement.service';
import { PaiementService } from '@core/services/paiement.service';
import { QuittanceLoyerService } from '@core/services/quittanceLoyer.service';
import { FilterLocataireAppartement } from '@models/appartement';
import { Locataire } from '@models/locataire';
import { ILocataireAppartement } from '@models/locataireAppartement';
import { IPaiement } from '@models/paiement';
import { QuittanceLoyer } from '@models/quittanceLoyer';
import { CellConfig, jsPDF } from 'jspdf';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Subject, map, switchMap, takeUntil, tap } from 'rxjs';

@Component({
  selector: 'templates-locataires-quittance-de-loyer',
  templateUrl: './templates-locataires-quittance-de-loyer.component.html',
  styleUrls: ['./templates-locataires-quittance-de-loyer.component.less'],
})
export class TemplatesLocatairesQuittanceDeLoyerComponent implements OnInit, OnDestroy, OnChanges {
  @Input() locataireAppartementId: number;
  @Input() locataireId: number;
  @Input() refreshListeQuittance: boolean;

  loading: boolean = true;
  listOfData: QuittanceLoyer[] = [];
  paiements: IPaiement[];
  total: number = 0;
  adresse: string;
  montantLoyer: number;
  montantCharge: number;
  codePostal: string;
  ville: string;
  position: number = 20;
  private destroy$ = new Subject<void>();

  constructor(
    private quittanceLoyerService: QuittanceLoyerService,
    private locataireService: LocataireService,
    private locataireAppartementService: LocataireAppartementService,
    private ref: ChangeDetectorRef,
    private frenchMonthPipe: FrenchMonthPipe,
    private paimentService: PaiementService,
    private datePipe: DatePipe,
    private message: NzMessageService,
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (this.refreshListeQuittance) {
      this.quittanceLoyer();
    }
  }

  ngOnInit() {
    this.quittanceLoyer();
  }

  quittanceLoyer() {
    if (this.locataireAppartementId) {
      this.quittanceLoyerService
        .obtenirQuittanceLoyers(this.locataireAppartementId)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (res: QuittanceLoyer[]) => {
            this.listOfData = res;
            this.loading = false;
            this.ref.markForCheck();
          },
          error: (erreur: any) => {
            console.log(erreur.error.error);
          },
        });
    }
  }

  cancel(): void {
    this.message.info('La suppression est annulée');
  }

  exportPDF(id: number) {
    this.position = 20;
    const dataQuittance = this.listOfData.find((u) => u.id == id);
    if (this.locataireId && dataQuittance && this.locataireAppartementId) {
      const mois = dataQuittance.mois;
      const annee = dataQuittance.annee;
      const montant = dataQuittance.montant;

      const filter = new FilterLocataireAppartement();
      filter.locataireId = this.locataireId;

      this.paimentService
        .obtenirTousPaiementLocataire(this.locataireAppartementId)
        .pipe(
          takeUntil(this.destroy$),
          map((paiements: IPaiement[]) => {
            this.paiements = paiements.filter(
              (u) => new Date(u.datePaiement).getMonth() == mois && new Date(u.datePaiement).getFullYear() === annee,
            );
          }),
          switchMap(() => this.locataireAppartementService.rechercheDetailAppartements(filter)),
          tap((res: ILocataireAppartement[]) => {
            const adr = res.find((u) => u.id == this.locataireAppartementId)?.appartement.adresse;
            if (adr) {
              this.adresse = adr;
            }
            const appt = res.find((u) => u.id == this.locataireAppartementId)?.appartement;
            if (appt) {
              this.montantCharge = appt.prixDesCharges;
              this.montantLoyer = appt.loyer;
            }
            const adrVille = res.find((u) => u.id == this.locataireAppartementId)?.appartement.ville;
            if (adrVille) {
              this.codePostal = adrVille.codePostal;
              this.ville = adrVille.nom;
            }
          }),
          switchMap(() => this.locataireService.obtenirLocataire(this.locataireId)),
          tap((res: Locataire) => {
            const fileName = `quittance_loyer_${res.prenom}_${res.nom}_${annee}_${mois}.pdf`;
            const doc = new jsPDF();
            var img = new Image();
            img.src = 'assets/img/logo_idimmo.png';

            doc.setFontSize(20);
            doc.setFont('helvetica', 'bold');
            const textTitre = `Quittance loyer`;
            const titleXPos = doc.internal.pageSize.getWidth() / 2 - doc.getTextWidth(textTitre) / 2;
            doc.addImage(img, 'png', 0, 0, 30, 17);
            doc.text(textTitre, titleXPos, this.position);
            this.position += 20;
            doc.text(`${res.civilite.nom} ${res.nom}  ${res.prenom}`, 10, this.position);
            this.position += 10;
            doc.text(`Adresse :`, 10, this.position);
            this.position += 10;
            doc.text(`${this.adresse}`, 10, this.position);
            this.position += 10;
            doc.text(`${this.codePostal} ${this.ville}`, 10, this.position);
            this.position += 10;
            doc.text(`Année : ${annee}  Mois : ${this.frenchMonthPipe.transform(mois)}`, 10, this.position);
            doc.setFont('helvetica', 'normal');
            doc.setFontSize(1);
            if (mois && annee && montant) {
              this.position += 10;
              doc.table(
                10,
                this.position,
                this._getDataForPdfTable(this.paiements),
                this._createHeadersForPdfTable(['date', 'origine', 'montant']),
                {
                  autoSize: false,
                },
              );
            }
            doc.text(`dont ${this.montantLoyer}€ de loyer et ${this.montantCharge}€ de charges`, 10, 130);

            doc.text(`Des frais d'agence de 8% s'appliquent sur le loyer.`, 10, 140);

            this.total = this.montantLoyer + this.montantCharge + (this.montantLoyer * 8) / 100;

            doc.text(`Montant total: ${this.total}€`, 10, 150);
            doc.save(fileName);
          }),
        )
        .subscribe();
    }
  }

  private _createHeadersForPdfTable(keys: string[]) {
    const result: CellConfig[] = [];
    for (let i = 0; i < keys.length; i += 1) {
      result.push({
        name: keys[i],
        prompt: keys[i],
        width: 55,
        align: 'center',
        padding: 10,
      });
    }
    return result;
  }

  private _getDataForPdfTable(paiements: IPaiement[]) {
    const data: any = [];
    paiements.map((paiement: IPaiement) => {
      if (paiement.typePaiement.nom !== 'Dépot de garantie') {
        data.push({
          date: this.datePipe.transform(paiement.datePaiement, 'dd/MM/yyyy'),
          origine: paiement.typePaiement.nom.toString(),
          montant: `${paiement.montant.toString()}€`,
        });
      }
    });

    return data;
  }

  supprimerQuittance(id: number) {
    if (this.locataireAppartementId) {
      this.quittanceLoyerService
        .supprimerQuittanceLoyer(id, this.locataireAppartementId)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            this.quittanceLoyer();
            this.loading = false;
            this.ref.markForCheck();
          },
          error: (erreur: any) => {
            console.log(erreur.error.error);
          },
        });
    }
  }

  ngOnDestroy(): void {
    this.ref.detach();
    this.destroy$.next();
    this.destroy$.complete();
  }
}
