import { NgModule } from '@angular/core';
import { CustomCurrencyPipe } from './customCurrency.pipe';

@NgModule({
  exports: [CustomCurrencyPipe],
  declarations: [CustomCurrencyPipe],
  providers: [CustomCurrencyPipe],
})
export class CustomCurrencyPipeModule {}
