import { AbstractControl, ValidatorFn } from '@angular/forms';
import { parsePhoneNumberFromString } from 'libphonenumber-js';

const phone: ValidatorFn = (control: AbstractControl): { [key: string]: boolean } => {
  const _phone: string = control.value;
  if (!_phone) {
    return null;
  }
  // TODO multilang
  const phoneNumber = parsePhoneNumberFromString(_phone, 'FR');
  const error = { phoneNumberValidator: true };

  if (!phoneNumber) {
    return error;
  }

  return phoneNumber.isValid() ? null : error;
};

const email: ValidatorFn = (control: AbstractControl): { [key: string]: boolean } => {
  const _email: string = control.value;
  if (!_email) {
    return null;
  }
  // eslint-disable-next-line max-len
  return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(
    _email,
  )
    ? null
    : { email: true };
};

export const customValidators: any = {
  email,
  phone,
};
