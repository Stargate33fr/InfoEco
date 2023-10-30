import { ChangeDetectorRef, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { PaiementService } from '@core/services/paiement.service';
import { Paiement } from '@models/paiement';
import { OrganismesModalPaiementComponent } from '@organismes/organismes-modal-paiement/organismes-modal-paiement.component';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Subject, map, takeUntil } from 'rxjs';

@Component({
  selector: 'templates-locataire-paiement',
  templateUrl: './templates-locataire-paiement.component.html',
  styleUrls: ['./templates-locataire-paiement.component.less'],
})
export class TemplatesLocatairePaiementComponent implements OnInit, OnDestroy {
  @Output() refreshQuittanceLoyer = new EventEmitter<boolean>();
  @Input() locataireAppartementId?: number;

  loading: boolean = true;
  listOfData: Paiement[] = [];

  private destroy$ = new Subject<void>();

  constructor(
    private paiementService: PaiementService,
    private message: NzMessageService,
    private modalService: NzModalService,
    private ref: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.paiements();
  }

  paiements() {
    if (this.locataireAppartementId) {
      this.paiementService
        .obtenirTousPaiementLocataire(this.locataireAppartementId)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (res: Paiement[]) => {
            this.listOfData = res.sort(
              (a: Paiement, b: Paiement) => new Date(b.datePaiement).getTime() - new Date(a.datePaiement).getTime(),
            );
            this.loading = false;
            this.ref.markForCheck();
          },
          error: (erreur: any) => {
            console.log(erreur.error.error);
          },
        });
    }
  }

  ajouterUnPaiement() {
    const modal = this.modalService.create({
      nzTitle: "Ajout d'un paiement",
      nzContent: OrganismesModalPaiementComponent,
      nzFooter: null,
      nzClosable: false,
      nzMaskClosable: false,
      nzWidth: '700px',
      nzData: {
        locataireAppartementId: this.locataireAppartementId,
      },
    });

    modal.afterClose.subscribe((res: number) => {
      this.listOfData = [];
      this.loading = true;
      setTimeout(() => {
        this.refreshQuittanceLoyer.emit(true);
        this.paiements();

        this.ref.markForCheck();
      }, 150);
    });
  }

  supprimerPaiement(id: number) {
    if (this.locataireAppartementId) {
      this.paiementService
        .supprimerPaiement(this.locataireAppartementId, id)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            this.paiements();
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

  ngOnDestroy(): void {
    this.ref.detach();
    this.destroy$.next();
    this.destroy$.complete();
  }
}
