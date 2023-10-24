import { ActionReducerMap } from '@ngrx/store';
import { IAppState } from '../state/app.state';
import { authReducer } from './auth.reducer';
import { contextReducer } from './context.reducer';
import { utilisateurReducer } from './utilisateur.reducer';

export const appReducers: ActionReducerMap<IAppState, any> = {
  auth: authReducer,
  context: contextReducer,
  utilisateur: utilisateurReducer,
};
