import { formatPercent } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'mypercent',
})
export class MypercentPipe implements PipeTransform {
  transform(value: number, digitsInfo: string = '1.2-2', locale: string = 'fr'): string | null {
    return formatPercent(value / 100, locale, digitsInfo);
  }
}
