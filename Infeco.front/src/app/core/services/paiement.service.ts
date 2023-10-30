import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { IAppState } from '@core/store/state/app.state';
import { IPaiement, LocatairePaiementAjout, LocatairePaiementRetour, Paiement } from '@models/paiement';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class PaiementService {
  constructor(private http: HttpClient, private store: Store<IAppState>) {}

  obtenirTousPaiementLocataire(locataireAppartementId: number) {
    return this.http
      .get<ResponseAPIDto<IPaiement[]>>(`/paiement/tous/${locataireAppartementId}`)
      .pipe(map((response) => new Paiement().deserializeList(response.contenu)));
  }

  ajouterPaiement(paiement: LocatairePaiementAjout): Observable<LocatairePaiementRetour> {
    return this.http.post<LocatairePaiementRetour>('/paiement', paiement).pipe(map((response) => response));
  }

  supprimerPaiement(locataireAppartementid: number, id: number) {
    return this.http.delete<LocatairePaiementRetour>(`/paiement/${locataireAppartementid}/${id}`);
  }
}
