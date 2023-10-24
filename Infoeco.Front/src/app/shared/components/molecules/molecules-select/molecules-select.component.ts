import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ISelect } from '@models/select';
import { Observable } from 'rxjs';

@Component({
  selector: 'molecules-select',
  templateUrl: './molecules-select.component.html',
  styleUrls: ['./molecules-select.component.less'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MoleculesSelectComponent),
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
          'padding-top': '5px',
          // eslint-disable-next-line @typescript-eslint/naming-convention
          'z-index': 0,
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
          'z-index': 3,
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
          'z-index': 3,
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
export class MoleculesSelectComponent implements OnInit, ControlValueAccessor {
  @Input() id: string;
  @Input() data: Observable<ISelect[]>;
  @Input() placeholder: string;
  @Input() longueur: string;
  @Input() disabled: boolean = false;
  @Input() valeurReel: number;

  state = 'normal';
  selectState = 'normal';
  labelVisible = false;
  selectedValue: any = null;

  constructor() {}
  ngOnInit(): void {
    if (this.valeurReel) {
      this.selectedValue = this.valeurReel;
      this.state = 'dessus';
    }
  }

  onTouched = () => {};

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  propagateChange = (_: any) => {};

  writeValue(value: string): void {
    if (value && value.length > 0) {
      this.labelVisible = true;
    } else {
      if (value && typeof +value === 'number') {
        this.state = 'dessus';
      } else {
        this.state = 'normal';
      }
    }
    this.selectedValue = value;
  }

  registerOnChange(onChange: (value: string) => void): void {
    this.onChange = onChange;
  }

  registerOnTouched(onTouched: () => void): void {
    this.onTouched = onTouched;
  }

  onChange(value: string): void {
    this.selectedValue = value;
    if (this.selectedValue && this.selectedValue > 0) {
      this.state = 'dessus';
    } else {
      this.state = 'normal';
    }
    this.selectState = 'normal';
    this.onChange(value);
    this.propagateChange(value);
  }

  onAnimate() {
    if (this.state === 'normal' || (this.selectedValue && this.selectedValue > 0)) {
      this.state = 'dessus';
    } else {
      this.state = 'normal';
    }
    this.selectState = 'normal';
  }

  compareFn = (o1: any, o2: any): boolean => (o1 && o2 ? o1.value === o2.value : o1 === o2);

  onAnimateDessusSelectionne() {
    if (this.selectedValue && this.selectedValue > 0) {
      this.state = 'dessus-selectionne';
      this.selectState = 'select-selectionne';
    } else {
      this.state = 'normal';
      this.selectState = 'normal';
    }
  }
}
