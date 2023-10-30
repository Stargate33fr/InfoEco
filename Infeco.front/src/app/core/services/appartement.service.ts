import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { IAppState } from '@core/store/state/app.state';
import { AjoutModifierAppartement, Appartement, AppartementAjoutRetour, IAppartement } from '@models/appartement';

import { IUser, User } from '@models/user';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AppartementService {
  constructor(private http: HttpClient, private store: Store<IAppState>) {}

  ajouterAppartement(appartement: AjoutModifierAppartement): Observable<AppartementAjoutRetour> {
    return this.http.post<AppartementAjoutRetour>('/appartements', appartement).pipe(map((response) => response));
  }

  modifierAppartement(id: number, appartement: AjoutModifierAppartement) {
    return this.http.put<AppartementAjoutRetour>(`/appartements/${id}`, appartement);
  }

  supprimerAppartement(id: number) {
    return this.http.delete(`/appartements/${id}`);
  }

  obtenirAppartement(id: number) {
    return this.http
      .get<ResponseAPIDto<IAppartement>>(`/appartements/${id}`)
      .pipe(map((response) => new Appartement().deserialize(response.contenu)));
  }
}
