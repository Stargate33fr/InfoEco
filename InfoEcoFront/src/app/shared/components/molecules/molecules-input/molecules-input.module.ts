import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { MoleculesInputComponent } from './molecules-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormatTelPipeModule } from '@core/pipes/formatTel.pipe.module';

@NgModule({
  declarations: [MoleculesInputComponent],
  exports: [MoleculesInputComponent],
  imports: [CommonModule, NgZorroAntdModule, FormsModule, ReactiveFormsModule, FormatTelPipeModule],
})
export class MoleculesInputModule {}
