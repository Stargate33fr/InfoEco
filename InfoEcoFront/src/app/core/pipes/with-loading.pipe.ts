import { Pipe, PipeTransform } from '@angular/core';
import { ISelect } from '@models/select';
import { Observable, isObservable, of } from 'rxjs';
import { map, startWith, catchError } from 'rxjs/operators';

@Pipe({
  name: 'withLoading',
})
export class WithLoadingPipe implements PipeTransform {
  transform(val: Observable<ISelect[]>) {
    return isObservable(val)
      ? val.pipe(
          map((value: ISelect[]) => ({ loading: false, valeur: value })),
          startWith({ loading: true, valeur: [] }),
          catchError((error) => of({ loading: false, valeur: [], error })),
        )
      : val;
  }
}
