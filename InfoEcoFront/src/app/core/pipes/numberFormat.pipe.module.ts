import { NgModule } from '@angular/core';
import { NumberFormatPipe } from './numberFormat.pipe';

@NgModule({
  exports: [NumberFormatPipe],
  declarations: [NumberFormatPipe],
  providers: [NumberFormatPipe],
})
export class NumberFormatPipeModule {}
