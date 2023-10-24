import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { MoleculesInputNumberComponent } from './molecules-input-number.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NumberFormatPipeModule } from '@core/pipes/numberFormat.pipe.module';
import { FormatTelPipeModule } from '@core/pipes/formatTel.pipe.module';

@NgModule({
  declarations: [MoleculesInputNumberComponent],
  exports: [MoleculesInputNumberComponent],
  imports: [CommonModule, NgZorroAntdModule, FormsModule, ReactiveFormsModule, NumberFormatPipeModule],
})
export class MoleculesInputNumberModule {}
