import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { MoleculesDatePickerComponent } from './molecules-datePicker.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [MoleculesDatePickerComponent],
  exports: [MoleculesDatePickerComponent],
  imports: [CommonModule, NgZorroAntdModule, FormsModule, ReactiveFormsModule],
})
export class MoleculesDatePickerModule {}
