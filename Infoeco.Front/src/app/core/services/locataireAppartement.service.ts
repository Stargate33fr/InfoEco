import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { NumberFormatPipeModule } from '@core/pipes/numberFormat.pipe.module';
import { IAppState } from '@core/store/state/app.state';
import { Appartements, FilterAppartement, FilterLocataireAppartement, IAppartement } from '@models/appartement';
import { ILocataireAppartement, LocataireAppartement } from '@models/locataireAppartement';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class LocataireAppartementService {
  constructor(private http: HttpClient, private store: Store<IAppState>) {}

  rechercheDetailAppartements(filter: FilterLocataireAppartement): Observable<ILocataireAppartement[]> {
    return this.http
      .post<ResponseAPIDto<ILocataireAppartement[]>>(`/LocatairesAppartement/recherche`, filter)
      .pipe(map((response) => new LocataireAppartement().deserializeList(response.contenu)));
  }
}
