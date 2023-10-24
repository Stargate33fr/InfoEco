import { User } from '@models/user';
import { Action } from '@ngrx/store';

// eslint-disable-next-line no-shadow
export enum UtilisateurActionTypes {
  getUser = '[Utilisateur] Get User',
  setUser = '[Utilisateur] Set User',
  userFailure = '[Utilisateur] User Failure',
  userSuccess = '[Utilisateur] User Success',
  setIdentifiantUser = '[Utilisateur] User Idenfiant',
}

export class GetUser implements Action {
  readonly type = UtilisateurActionTypes.getUser;
  constructor(public payload: string) {}
}

export class SetIdentifiantUser implements Action {
  public readonly type = UtilisateurActionTypes.setIdentifiantUser;
  constructor(public payload: string) {}
}

export class SetUser implements Action {
  public readonly type = UtilisateurActionTypes.setUser;
  constructor(public payload: User) {}
}

export class GetUtilisateurFailure implements Action {
  public readonly type = UtilisateurActionTypes.userFailure;
  constructor(public payload: string) {}
}

export class GetUtilisateurSuccess implements Action {
  public readonly type = UtilisateurActionTypes.userSuccess;
  constructor(public payload: User) {}
}

export type UtilisateurActions = GetUser | SetUser | GetUtilisateurFailure | GetUtilisateurSuccess | SetIdentifiantUser;
