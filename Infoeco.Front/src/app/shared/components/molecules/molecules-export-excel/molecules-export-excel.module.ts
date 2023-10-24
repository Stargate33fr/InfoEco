import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { MoleculesExportExcelComponent } from './molecules-export-excel.component';
import { CustomCurrencyPipeModule } from '@core/pipes/customCurrency.pipe.module';
import { MypercentPipeModule } from '@core/pipes/custom.percent.pipe.module';

@NgModule({
  declarations: [MoleculesExportExcelComponent],
  exports: [MoleculesExportExcelComponent],
  imports: [CommonModule, NgZorroAntdModule, FormsModule, CustomCurrencyPipeModule, MypercentPipeModule],
  providers: [DatePipe],
})
export class MoleculesExportExcelModule {}
