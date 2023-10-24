import { NgModule } from '@angular/core';
import { FormatTelPipe } from './formatTel.pipe';

@NgModule({
  exports: [FormatTelPipe],
  declarations: [FormatTelPipe],
  providers: [FormatTelPipe],
})
export class FormatTelPipeModule {}
