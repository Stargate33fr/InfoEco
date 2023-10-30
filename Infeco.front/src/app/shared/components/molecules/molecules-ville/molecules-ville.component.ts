import { animate, state, style, transition, trigger } from '@angular/animations';
import { ChangeDetectorRef, Component, Input, OnDestroy, OnInit, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { DonneesReferencesService } from '@core/services/donnees-references.service';
import { ISelect } from '@models/select';
import { Observable, Subject, map, takeUntil } from 'rxjs';

@Component({
  selector: 'molecules-ville',
  templateUrl: './molecules-ville.component.html',
  styleUrls: ['./molecules-ville.component.less'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MoleculesVilleComponent),
      multi: true,
    },
  ],
  animations: [
    trigger('selectState', [
      state(
        'normal',
        style({
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'border-width': '1px',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'border-color': 'lightblue',
        }),
      ),
      state(
        'select-selectionne',
        style({
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'border-color': 'lightblue',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'box-shadow': '0 0 0 2px rgba(24, 144, 255, .2)',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'border-right-width': '1px',
          outline: '0',
        }),
      ),
      transition('normal <=> select-selectionne', animate(100)),
    ]),
    trigger('divState', [
      state(
        'normal',
        style({
          // eslint-disable-next-line @typescript-eslint/naming-convention
          transform: 'translateY(-40px) scale(1)',
          color: '#92969a',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'padding-top': '4px',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'z-index': 99,
          // eslint-disable-next-line @typescript-eslint/naming-convention
          position: 'relative',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'text-align': 'left',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'padding-left': '15px',
        }),
      ),
      state(
        'dessus-selectionne',
        style({
          // eslint-disable-next-line @typescript-eslint/naming-convention
          transform: 'translateY(-59px) scale(1)',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'padding-top': '5px',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          // 'margin-bottom': '-8px',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'z-index': 99,
          position: 'relative',
          color: 'rgb(61, 111, 158)',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'text-align': 'left',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'padding-left': '15px',
        }),
      ),
      state(
        'dessus',
        style({
          // eslint-disable-next-line @typescript-eslint/naming-convention
          transform: 'translateY(-59px) scale(1)',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'padding-top': '5px',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          // 'margin-bottom': '5px',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'z-index': 99,
          position: 'relative',
          color: '#92969a',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'text-align': 'left',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'padding-left': '15px',
        }),
      ),
      transition('normal <=> dessus-selectionne', animate(100)),
      transition('dessus-selectionne <=> dessus', animate(100)),
      transition('normal <=> dessus', animate(100)),
    ]),
  ],
})
export class MoleculesVilleComponent implements OnInit, OnDestroy {
  @Input() id: string;
  @Input() placeholder: string;
  @Input() longueur: string;

  displayTips: boolean = true;
  ville$: Observable<ISelect[]>;
  villeItems: ISelect[];
  ville: string;
  statutVille: string = 'error';
  state = 'normal';
  selectState = 'normal';
  private destroy$ = new Subject<void>();

  constructor(private donneesReferencesService: DonneesReferencesService, private ref: ChangeDetectorRef) {}

  ngOnInit() {}

  onInput(e: Event): void {
    e?.preventDefault();

    const value = (e.target as HTMLInputElement).value;
    if (value && value.length >= 2) {
      this.displayTips = false;
      this.donneesReferencesService
        .ville(value)
        .pipe(
          takeUntil(this.destroy$),
          map((villes: ISelect[]) => {
            this.villeItems = villes;
            this.ref.detectChanges();
          }),
        )
        .subscribe();
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
      this.propagateChange(valeur);
    } else {
      this.statutVille = 'error';
    }
  }

  onTouched = () => {};

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  propagateChange = (_: any) => {};

  writeValue(value: number): void {}

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  onAnimate() {
    if (this.state === 'normal') {
      this.state = 'dessus';
    } else {
      this.state = 'normal';
    }
    this.selectState = 'normal';
  }

  compareFn = (o1: any, o2: any): boolean => (o1 && o2 ? o1.value === o2.value : o1 === o2);

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.ref.detach();
  }
}
