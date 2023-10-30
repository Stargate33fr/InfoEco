import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ResponseAPIDto } from '@core/dto/response-api';
import { IAppState } from '@core/store/state/app.state';

import { IUser, User } from '@models/user';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class UtilisateurService {
  constructor(private http: HttpClient, private store: Store<IAppState>) {}

  getUserByEmail(email: string): Observable<User> {
    return this.http.get<ResponseAPIDto<IUser>>(`/utilisateur/${email}`).pipe(map((response) => new User().deserialize(response.contenu)));
  }
}
