import { formatCurrency } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'customCurrency',
})
export class CustomCurrencyPipe implements PipeTransform {
  transform(
    value: number,
    locale: string = 'fr-FR',
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    _display: 'code' | 'symbol' | 'symbol-narrow' | string | boolean = 'symbol',
    currency: string = '€',
    currencyCode: string = 'EUR',
    digitsInfo: string = '2.2-2',
  ): string | null {
    return formatCurrency(value, locale, currency, currencyCode, digitsInfo);
  }
}
