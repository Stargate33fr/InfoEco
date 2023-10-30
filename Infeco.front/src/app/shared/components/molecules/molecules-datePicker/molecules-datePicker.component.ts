import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
// @Component({
//   selector: 'molecules-datePicker',
//   templateUrl: './molecules-datePicker.component.html',
//   styleUrls: ['./molecules-datePicker.component.less'],
//   providers: [
//     {
//       provide: NG_VALUE_ACCESSOR,
//       useExisting: forwardRef(() => MoleculesDapickerComponent),
//       multi: true,
//     },
//   ],
// })
// export class MoleculesDapickerComponent implements ControlValueAccessor  {

//   @Output() changeValeur: EventEmitter<any> = new EventEmitter<any>();

//   state = 'normal';
//   selectState = 'normal';
//   selectedValue: Date;

//   constructor() {}

//   ngOnInit() {}

//   writeValue(value: any): void {
//     if (value) {
//       this.selectedValue = value;

//       this.changeValeur.emit(value);
//       this.onAnimateDessusSelectionne();
//     }
//   }

//   onAnimate() {
//     if (this.state === 'normal' || this.selectedValue) {
//       this.state = 'dessus';
//     } else {
//       this.state = 'normal';
//     }
//     this.selectState = 'normal';
//   }

//   onAnimateDessusSelectionne() {
//     if (this.state === 'normal' || this.selectedValue) {
//       this.state = 'dessus-selectionne';
//       this.selectState = 'select-selectionne';
//     } else {
//       this.state = 'normal';
//       this.selectState = 'normal';
//     }
//   }

//   selectValue() {
//     if (this.selectedValue) {
//       this.state = 'dessus';
//     } else {
//       this.state = 'normal';
//     }
//     this.selectState = 'normal';
//   }

//   onChange(result: Date) {
//     this.writeValue(result);
//     if (!this.selectedValue) {
//       this.state = 'normal';
//     }
//   }
// }

@Component({
  selector: 'molecules-datePicker',
  templateUrl: './molecules-datePicker.component.html',
  styleUrls: ['./molecules-datePicker.component.less'],
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
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MoleculesDatePickerComponent),
      multi: true,
    },
  ],
})
// b) Add "implements ControlValueAccessor"
export class MoleculesDatePickerComponent implements ControlValueAccessor {
  @Input() id: string;
  @Input() placeholder: string;
  @Input() longueur: string;
  @Input() format: string;
  @Input() disabled: boolean = false;

  state = 'normal';
  selectState = 'normal';
  selectedValue: Date;

  onChange: any = () => {
    this.onAnimateDessusSelectionne();
  };

  onTouch: any = () => {};

  propagateChange = () => {};

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  writeValue(input: Date) {
    this.selectedValue = input;
    this.state = 'normal';
    if (input) {
      this.onAnimate();
    }
  }

  onAnimate() {
    if (this.state === 'normal' || this.selectedValue) {
      this.state = 'dessus';
    } else {
      this.state = 'normal';
    }
    this.selectState = 'normal';
  }

  onAnimateDessusSelectionne() {
    if (this.selectedValue && this.selectedValue) {
      this.state = 'dessus-selectionne';
      this.selectState = 'select-selectionne';
    } else {
      this.state = 'normal';
      this.selectState = 'normal';
    }
  }
}
