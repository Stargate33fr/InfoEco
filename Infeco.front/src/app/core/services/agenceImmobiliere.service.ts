import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { NumberFormatPipeModule } from '@core/pipes/numberFormat.pipe.module';
import { IAppState } from '@core/store/state/app.state';
import { Appartements, FilterAppartement, IAppartement } from '@models/appartement';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AgenceImmobiliereService {
  constructor(private http: HttpClient, private store: Store<IAppState>) {}

  getAppartementsAgence(
    filter: FilterAppartement,
    limite: number = 10,
    index: number = 0,
    champ: string = 'Id',
    ascendant: boolean = true,
  ): Observable<Appartements> {
    return this.http
      .post<ResponseAPIDto<IAppartement[]>>(
        `/agenceImmobiliere/appartements/recherche?limite=${limite}&index=${index - 1}&champ=${champ}&ascendant=${ascendant}`,
        filter,
      )
      .pipe(map((response) => new Appartements().deserialize(response)));
  }
}
