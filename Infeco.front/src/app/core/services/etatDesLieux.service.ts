import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { IAppState } from '@core/store/state/app.state';
import { AjoutEtatDesLieux, EtatDesLieux, EtatDesLieuxAjoutRetour, IEtatDesLieux } from '@models/etatDesLieux';

import { Store } from '@ngrx/store';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class EtatDesLieuxService {
  constructor(private http: HttpClient, private store: Store<IAppState>) {}

  obtenirEtatDesLieuxLocataire(locataireAppartementId: number) {
    return this.http
      .get<ResponseAPIDto<IEtatDesLieux>>(`/etatDesLieux/${locataireAppartementId}`)
      .pipe(map((response) => new EtatDesLieux().deserialize(response.contenu)));
  }

  creerEtatDesLieux(data: AjoutEtatDesLieux) {
    return this.http.post<EtatDesLieuxAjoutRetour>('/etatDesLieux', data).pipe(map((response) => response));
  }
}
