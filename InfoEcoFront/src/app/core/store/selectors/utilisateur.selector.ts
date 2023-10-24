import { User } from '@models/user';
import { createSelector } from '@ngrx/store';
import { IAppState } from '../state/app.state';
import { IUserState } from '../state/user.state.';

const selectUser = (state: IAppState) => state.utilisateur;

export const getUtilisateur = createSelector(selectUser, (state: IUserState) => state && new User().deserialize(state.utilisateur));
