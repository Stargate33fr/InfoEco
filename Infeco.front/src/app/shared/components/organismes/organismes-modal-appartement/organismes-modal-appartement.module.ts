import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { WithLoadingPipeModule } from '@core/pipes/with-loading.pipe.module';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { OrganismesModalAppartementComponent } from './organismes-modal-appartement.component';
import { MoleculesInputModule } from '../../molecules/molecules-input/molecules-input.module';
import { MoleculesSelectModule } from '../../molecules/molecules-select/molecules-select.module';
import { MoleculesDatePickerModule } from '../../molecules/molecules-datePicker/molecules-datePicker.module';
import { MoleculesInputNumberModule } from '../../molecules/molecules-input-number/molecules-input-number.module';
import { MoleculesRechercheAppartementModule } from '../../molecules/molecules-recherche-appartement/molecules-recherche-appartement.module';
import { MoleculesVilleModule } from '../../molecules/molecules-ville/molecules-ville.module';

@NgModule({
  declarations: [OrganismesModalAppartementComponent],
  exports: [OrganismesModalAppartementComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgZorroAntdModule,
    WithLoadingPipeModule,
    RouterModule,
    MoleculesInputModule,
    MoleculesSelectModule,
    MoleculesDatePickerModule,
    MoleculesInputNumberModule,
    MoleculesRechercheAppartementModule,
    MoleculesVilleModule,
  ],
  providers: [DatePipe],
})
export class OrganismesModalAppartementModule {}
