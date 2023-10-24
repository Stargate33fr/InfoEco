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
import { NumberFormatPipe } from '@core/pipes/numberFormat.pipe';
import { Subject } from 'rxjs';

@Component({
  selector: 'molecules-input-number',
  templateUrl: './molecules-input-number.component.html',
  styleUrls: ['./molecules-input-number.component.less'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MoleculesInputNumberComponent),
      multi: true,
    },
  ],
})
export class MoleculesInputNumberComponent implements OnInit, OnDestroy {
  @Input() id: string;
  @Input() placeholder: string;
  @Input() uniteDeMesure: string;
  @Input() longueur: string;
  @Input() formatNumber: boolean = false;
  @Input() avecDecimal: boolean = false;
  @Input() disabled: boolean = false;
  @Input() valeurReel: number;

  @Output() changeValeur: EventEmitter<any> = new EventEmitter<any>();

  @ViewChild('inputElement', { static: false }) inputElement: ElementRef;

  valuePourcentage = '';
  valueAutre = '';
  valueAutreformate: number;
  audessusvisible = false;

  private destroy$: Subject<void> = new Subject<void>();

  constructor(private ref: ChangeDetectorRef, private formatPipe: NumberFormatPipe) {}

  ngOnInit() {
    this.valueAutre = this.valeurReel > 0 || this.valeurReel === 0 ? this.formatValeurForce(this.valeurReel.toString()) : '';
  }

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
            this.inputElement.nativeElement.value = valeur;
          }

          this.ref.markForCheck();
        } else {
          this.propagateChange(+valeur.target.value);
          if (this.inputElement && this.inputElement.nativeElement) {
            this.inputElement.nativeElement.value = valeur.target.value;
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
        this.propagateChange(valeur.target.value);
      }
    }
  }

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  formatValeur(event: any) {
    if (this.formatNumber === true) {
      this.valueAutre = this.formatPipe.transform(event.target.value, this.avecDecimal).toString();
    }
  }

  formatValeurForce(valeur: string): string {
    if (this.formatNumber === true) {
      return this.formatPipe.transform(valeur, this.avecDecimal).toString();
    } else {
      return valeur;
    }
  }

  focus() {
    this.audessusvisible = true;
  }

  public registerOnTouched() {}

  onChange(value: string): void {
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
