import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { IAppState } from '@core/store/state/app.state';
import { IQuittanceLoyer, QuittanceLoyer } from '@models/quittanceLoyer';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class QuittanceLoyerService {
  constructor(private http: HttpClient, private store: Store<IAppState>) {}

  obtenirQuittanceLoyers(locataireAppartementId: number) {
    return this.http
      .get<ResponseAPIDto<IQuittanceLoyer[]>>(`/quittanceLoyer/${locataireAppartementId}`)
      .pipe(map((response) => new QuittanceLoyer().deserializeList(response.contenu)));
  }

  supprimerQuittanceLoyer(id: number, locataireAppartementId: number) {
    return this.http.delete(`/quittanceLoyer/${locataireAppartementId}/${id}`);
  }
}
