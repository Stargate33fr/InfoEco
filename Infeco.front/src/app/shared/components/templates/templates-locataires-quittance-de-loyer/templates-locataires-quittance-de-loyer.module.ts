import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { TemplatesLocatairesQuittanceDeLoyerComponent } from './templates-locataires-quittance-de-loyer.component';
import { RouterModule } from '@angular/router';
import { frenchMonthPipeModule } from '@core/pipes/frenchMonth.pipe.module';

@NgModule({
  declarations: [TemplatesLocatairesQuittanceDeLoyerComponent],
  exports: [TemplatesLocatairesQuittanceDeLoyerComponent],
  imports: [CommonModule, NgZorroAntdModule, RouterModule, frenchMonthPipeModule],
  providers: [DatePipe],
})
export class TemplatesLocatairesQuittanceDeLoyerModule {}
