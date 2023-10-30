import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { TemplatesLocataireEtatDesLieuxComponent } from './templates-locataire-etat-des-lieux.component';
import { RouterModule } from '@angular/router';
import { MoleculesDatePickerModule } from '../../molecules/molecules-datePicker/molecules-datePicker.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [TemplatesLocataireEtatDesLieuxComponent],
  exports: [TemplatesLocataireEtatDesLieuxComponent],
  imports: [CommonModule, NgZorroAntdModule, RouterModule, MoleculesDatePickerModule, FormsModule],
})
export class TemplatesLocataireEtatDesLieuxModule {}
