import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { MoleculesRechercheAppartementComponent } from './molecules-recherche-appartement.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NumberFormatPipeModule } from '@core/pipes/numberFormat.pipe.module';
import { MoleculesInputNumberModule } from '../molecules-input-number/molecules-input-number.module';
import { MoleculesSelectModule } from '../molecules-select/molecules-select.module';
import { SharedTestingModule } from '@core/testing/shared.testing.module.spec';

@NgModule({
  declarations: [MoleculesRechercheAppartementComponent],
  exports: [MoleculesRechercheAppartementComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgZorroAntdModule,
    NumberFormatPipeModule,
    MoleculesInputNumberModule,
    MoleculesSelectModule,
  ],
})
export class MoleculesRechercheAppartementModule {}
