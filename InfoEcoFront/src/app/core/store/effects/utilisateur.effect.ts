import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UtilisateurService } from '@core/services/utilisateur.service';
import { UpdateUser, User } from '@models/user';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { GetUser, GetUtilisateurFailure, GetUtilisateurSuccess, UtilisateurActionTypes } from '../actions/utilisateur.action';

@Injectable()
export class UtilisateurEffects {
  logIn$ = createEffect(() =>
    this.actions$.pipe(
      ofType<GetUser>(UtilisateurActionTypes.getUser),
      switchMap((result) =>
        this.utilisateurService.getUserByEmail(result.payload).pipe(
          map((user: User) => {
            return new GetUtilisateurSuccess(user);
          }),
          catchError(() => of(new GetUtilisateurFailure(result.payload))),
        ),
      ),
    ),
  );

  constructor(private actions$: Actions, private router: Router, private utilisateurService: UtilisateurService) {}
}
