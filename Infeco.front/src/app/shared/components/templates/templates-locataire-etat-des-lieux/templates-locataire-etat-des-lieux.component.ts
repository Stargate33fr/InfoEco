import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { EtatDesLieuxService } from '@core/services/etatDesLieux.service';
import { AjoutEtatDesLieux, EtatDesLieux, EtatDesLieuxAjoutRetour, IEtatDesLieux } from '@models/etatDesLieux';
import { Subject, map, takeUntil } from 'rxjs';

@Component({
  selector: 'templates-locataire-etat-des-lieux',
  templateUrl: './templates-locataire-etat-des-lieux.component.html',
  styleUrls: ['./templates-locataire-etat-des-lieux.component.less'],
})
export class TemplatesLocataireEtatDesLieuxComponent implements OnInit {
  @Input() locataireAppartementId?: number;
  loading: boolean = false;
  ajoutButtonVisible: boolean = false;
  modifieButtonVisible: boolean = false;
  disabled: boolean = true;
  dateEtatDesLieux: Date;
  commentaire: string;

  private destroy$ = new Subject<void>();

  constructor(private etatDesLieuxService: EtatDesLieuxService, private ref: ChangeDetectorRef) {}

  ngOnInit() {
    this.etatDesLieux();
  }

  etatDesLieux() {
    if (this.locataireAppartementId) {
      this.etatDesLieuxService
        .obtenirEtatDesLieuxLocataire(this.locataireAppartementId)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (res: IEtatDesLieux) => {
            if (res) {
              this.dateEtatDesLieux = res.dateEtatDesLieux;
              this.commentaire = res.remarque;
              this.ref.markForCheck();
            } else {
              this.disabled = false;
            }
          },
          error: (erreur) => {
            console.log(erreur.error.error);
            this.ajoutButtonVisible = true;
          },
        });
    }
  }

  activeModifier() {
    this.disabled = false;
  }

  enregistrer() {
    if (!this.disabled && this.locataireAppartementId) {
      const ajouterEtatDeslieux = new AjoutEtatDesLieux();
      ajouterEtatDeslieux.dateEtatDesLieux = this.dateEtatDesLieux;
      ajouterEtatDeslieux.remarque = this.commentaire;
      ajouterEtatDeslieux.locataireAppartementId = this.locataireAppartementId;

      this.etatDesLieuxService
        .creerEtatDesLieux(ajouterEtatDeslieux)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (res: EtatDesLieuxAjoutRetour) => {
            if (res) {
              this.etatDesLieux();
              this.ref.markForCheck();
            } else {
              this.disabled = false;
            }
          },
          error: (erreur) => {
            console.log(erreur.error.error);
            this.ajoutButtonVisible = true;
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
