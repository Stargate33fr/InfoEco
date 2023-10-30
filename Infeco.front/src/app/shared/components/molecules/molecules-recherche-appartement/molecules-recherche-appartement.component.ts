import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DonneesReferencesService } from '@core/services/donnees-references.service';
import { ISelect } from '@models/select';
import { Observable, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'molecules-recherche-appartement',
  templateUrl: './molecules-recherche-appartement.component.html',
  styleUrls: ['./molecules-recherche-appartement.component.less'],
})
export class MoleculesRechercheAppartementComponent implements OnInit, OnDestroy {
  formRechercheAppartement: FormGroup;
  ville$: Observable<ISelect[]>;
  displayTips: boolean = true;
  villeItems: ISelect[];
  ville: string;
  statutVille: string = 'error';

  private destroy$ = new Subject<void>();

  constructor(private fb: FormBuilder, private ref: ChangeDetectorRef, private donneesReferencesService: DonneesReferencesService) {}

  ngOnInit() {
    this.formRechercheAppartement = this.fb.group({
      ville: [null],
      typeAppartementId: [null],
      surface: [null],
    });
  }

  onInput(e: Event): void {
    e?.preventDefault();

    const value = (e.target as HTMLInputElement).value;
    if (value && value.length >= 2) {
      this.displayTips = false;
      this.donneesReferencesService
        .ville(value)
        .pipe(takeUntil(this.destroy$))
        .subscribe(
          (villes: ISelect[]) => {
            this.villeItems = villes;
            this.ref.detectChanges();
          },
          (error) => console.log(error),
        );
    } else {
      this.displayTips = true;
      this.villeItems = [];
      this.ref.detectChanges();
    }
  }

  selectOption(valeur: number) {
    if (valeur !== undefined && typeof valeur === 'number') {
      const lavaleur = this.villeItems.find((u) => u.id === valeur);
      if (lavaleur) {
        this.ville = lavaleur.nom;
      }
    } else {
      this.statutVille = 'error';
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.ref.detach();
  }
}
