import { createSelector } from '@ngrx/store';
import { Token } from 'src/app/shared/models/token';
import { IAppState } from '../state/app.state';
import { IAuthState } from '../state/auth.state';

const selectAuthUser = (state: IAppState) => state.auth;

export const getAuthState = createSelector(selectAuthUser, (state: IAuthState) => state);

export const getToken = createSelector(selectAuthUser, (state: IAuthState) => state && new Token().deserialize(state.token));

export const isLogged = createSelector(selectAuthUser, (state: IAuthState) => state && state.isLogged);
