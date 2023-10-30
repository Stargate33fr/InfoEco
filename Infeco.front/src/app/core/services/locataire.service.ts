import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { IAppState } from '@core/store/state/app.state';
import { FilterLocataireAppartement } from '@models/appartement';
import { AjoutModifierLocataire, ILocataire, Locataire, LocataireAjoutRetour } from '@models/locataire';
import { ILocataireAppartement, LocataireAppartement } from '@models/locataireAppartement';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class LocataireService {
  constructor(private http: HttpClient, private store: Store<IAppState>) {}

  obtenirLocataire(id: number) {
    return this.http
      .get<ResponseAPIDto<ILocataire>>(`/locataires/${id}`)
      .pipe(map((response) => new Locataire().deserialize(response.contenu)));
  }

  ajouterLocataire(locataire: AjoutModifierLocataire): Observable<LocataireAjoutRetour> {
    return this.http.post<LocataireAjoutRetour>('/locataires', locataire).pipe(map((response) => response));
  }

  modifierLocataire(id: number, locataire: AjoutModifierLocataire) {
    return this.http.put<LocataireAjoutRetour>(`/locataires/${id}`, locataire);
  }

  supprimerLocataire(locataireAppartementId: number, id: number) {
    return this.http.delete(`/locataires/${locataireAppartementId}/${id}`);
  }
}
