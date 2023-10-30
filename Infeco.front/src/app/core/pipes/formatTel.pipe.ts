import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
  name: 'formatTel',
})
export class FormatTelPipe implements PipeTransform {
  transform(value: string): string | null {
    let valuePortable = '';
    if (value) {
      const regexPoint = /[.]/gi;
      const regexTiret = /[-]/gi;
      const regexEspace = /[ ]/gi;
      let estFormate = false;

      value = value.toString().replace(regexPoint, '').replace(regexTiret, '').replace(regexEspace, '');

      if (value.length >= 10) {
        if (value.substr(0, 1) !== '+') {
          valuePortable = value.replace(/(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})/, '+33 $1 $2 $3 $4 $5');
          if (valuePortable.substring(0, 5) === '+33 0') {
            valuePortable = valuePortable.replace('+33 0', '+33 ');
          }
        } else {
          if (value.length >= 11) {
            if (
              (value.substr(0, 4) === '+331' ||
                value.substr(0, 4) === '+332' ||
                value.substr(0, 4) === '+333' ||
                value.substr(0, 4) === '+334' ||
                value.substr(0, 4) === '+335' ||
                value.substr(0, 4) === '+336' ||
                value.substr(0, 4) === '+337' ||
                value.substr(0, 4) === '+338' ||
                value.substr(0, 4) === '+339') &&
              value.length === 12
            ) {
              valuePortable = value.replace(/(\d{2})(\d{1})(\d{2})(\d{2})(\d{2})(\d{2})/, '$1 $2 $3 $4 $5 $6');
              estFormate = true;
            } else {
              if (!estFormate) {
                valuePortable = value;
              }
            }
            if (value.substr(0, 4) === '+330' && value.length === 13) {
              valuePortable = value.replace(/(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})/, '$1 $2 $3 $4 $5 $6');
              if (valuePortable.substring(0, 5) === '+33 0') {
                valuePortable = valuePortable.replace('+33 0', '+33 ');
              }
            } else {
              if (!estFormate) {
                valuePortable = value;
              }
            }
          } else {
            valuePortable = value;
          }
        }
      } else {
        valuePortable = value;
      }
    }

    return valuePortable;
  }
}
