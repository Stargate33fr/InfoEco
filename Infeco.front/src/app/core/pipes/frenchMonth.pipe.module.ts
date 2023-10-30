import { NgModule } from '@angular/core';
import { FrenchMonthPipe } from './frenchMonth.pipe';

@NgModule({
  exports: [FrenchMonthPipe],
  declarations: [FrenchMonthPipe],
  providers: [FrenchMonthPipe],
})
export class frenchMonthPipeModule {}
