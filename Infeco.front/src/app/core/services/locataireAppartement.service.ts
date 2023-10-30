import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { IAppState } from '@core/store/state/app.state';
import { FilterLocataireAppartement } from '@models/appartement';
import {
  AssignerLocataire,
  ILocataireAppartement,
  LocataireAppartement,
  LocataireAppartementAjoutRetour,
} from '@models/locataireAppartement';

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
      .post<ResponseAPIDto<ILocataireAppartement[]>>(`/locatairesAppartement/recherche`, filter)
      .pipe(map((response) => new LocataireAppartement().deserializeList(response.contenu)));
  }

  assignerLocataireAUnAppartement(data: AssignerLocataire): Observable<LocataireAppartementAjoutRetour> {
    return this.http
      .post<LocataireAppartementAjoutRetour>(`/locatairesAppartement/assignerAppartement`, data)
      .pipe(map((response) => response));
  }
}
