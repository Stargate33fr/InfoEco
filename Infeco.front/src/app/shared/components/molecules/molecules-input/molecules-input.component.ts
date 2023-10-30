import { animate, state, style, transition, trigger } from '@angular/animations';
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  forwardRef,
} from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { FormatTelPipe } from '@core/pipes/formatTel.pipe';
import { Subject } from 'rxjs';

@Component({
  selector: 'molecules-input',
  templateUrl: './molecules-input.component.html',
  styleUrls: ['./molecules-input.component.less'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MoleculesInputComponent),
      multi: true,
    },
  ],
})
export class MoleculesInputComponent implements OnInit, OnDestroy {
  @Input() id: string;
  @Input() placeholder: string;
  @Input() uniteDeMesure: string;
  @Input() longueur: string;
  @Input() formatNumber: boolean = false;
  @Input() avecDecimal: boolean = false;
  @Input() disabled: boolean = false;
  @Input() valeurReel: number;
  @Input() estUnTelephone: boolean = false;

  @Output() changeValeur: EventEmitter<any> = new EventEmitter<any>();

  @ViewChild('inputElement', { static: false }) inputElement: ElementRef;

  valuePourcentage = '';
  valueAutre = '';
  valueAutreformate: number;
  audessusvisible = false;

  private destroy$: Subject<void> = new Subject<void>();

  constructor(private ref: ChangeDetectorRef, private formatTelPipe: FormatTelPipe) {}

  ngOnInit() {}

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  propagateChange = (_: any) => {};

  writeValue(valeur: any): void {
    if (
      valeur &&
      ((typeof valeur === 'number' && !valeur.toString().endsWith('.') && !valeur.toString().endsWith(',')) ||
        (valeur.target &&
          valeur.target.value &&
          !valeur.target.value.toString().endsWith('.') &&
          !valeur.target.value.toString().endsWith(',')))
    ) {
      if (this.uniteDeMesure === '%' && valeur > 100) {
        valeur = 100;
      }

      if (valeur && (typeof valeur === 'number' || (valeur.target && valeur.target.value))) {
        if (typeof valeur === 'number') {
          this.propagateChange(valeur);
          if (this.inputElement && this.inputElement.nativeElement) {
            if (this.estUnTelephone) {
              const valeurTransforme = this.formatTelPipe.transform(valeur.toString());
              if (valeurTransforme) {
                valeur = valeurTransforme;
              }
            }
            this.inputElement.nativeElement.value = valeur;
          }

          this.ref.markForCheck();
        } else {
          if (this.inputElement && this.inputElement.nativeElement) {
            if (this.estUnTelephone) {
              const valeurTransforme = this.formatTelPipe.transform(valeur.target.value.toString());
              if (valeurTransforme) {
                valeur.target.value = valeurTransforme;
              }
            }
            this.inputElement.nativeElement.value = valeur.target.value;
            this.propagateChange(+valeur.target.value);
          }
          this.ref.markForCheck();
        }

        this.audessusvisible = true;
      } else {
        this.audessusvisible = false;
        this.inputElement.nativeElement.value = null;
      }
    } else {
      if (!valeur) {
        this.valueAutre = '';
        this.propagateChange(this.valueAutre);
        if (this.inputElement) {
          this.inputElement.nativeElement.value = this.valueAutre;
        }
      } else {
        this.propagateChange(valeur);
        if (this.inputElement) {
          this.inputElement.nativeElement.value = valeur;
        }
      }
    }
  }

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  focus() {
    this.audessusvisible = true;
  }

  public registerOnTouched() {}

  onChange(value: string): void {
    if (this.estUnTelephone) {
      const valeurTransforme = this.formatTelPipe.transform(value);
      if (valeurTransforme) {
        value = valeurTransforme;
      }
    }
    this.propagateChange(value);
    this.updateValue(value);
  }

  updateValue(value: string): void {
    this.changeValeur.emit(value);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
