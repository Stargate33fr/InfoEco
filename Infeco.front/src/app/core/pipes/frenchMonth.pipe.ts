import { formatCurrency } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'moisLettre',
})
export class FrenchMonthPipe implements PipeTransform {
  frenchMonths = ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'];

  transform(value: any): string {
    if (value >= 1 && value <= 12) {
      return this.frenchMonths[value - 1];
    } else {
      return 'Mois invalide';
    }
  }
}
