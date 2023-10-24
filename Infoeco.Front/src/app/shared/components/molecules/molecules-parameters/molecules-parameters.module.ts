import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { MoleculesParametersComponent } from './molecules-parameters.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NumberFormatPipeModule } from '@core/pipes/numberFormat.pipe.module';
import { MoleculesInputNumberModule } from '../molecules-input-number/molecules-input-number.module';
import { MoleculesSelectModule } from '../molecules-select/molecules-select.module';

@NgModule({
  declarations: [MoleculesParametersComponent],
  exports: [MoleculesParametersComponent],
  imports: [
    CommonModule,
    NgZorroAntdModule,
    FormsModule,
    ReactiveFormsModule,
    NumberFormatPipeModule,
    MoleculesInputNumberModule,
    MoleculesSelectModule,
  ],
})
export class MoleculesParametersModule {}
