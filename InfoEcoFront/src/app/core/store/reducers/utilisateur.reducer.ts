/* eslint-disable prefer-arrow/prefer-arrow-functions */
import { UtilisateurActions, UtilisateurActionTypes } from '../actions/utilisateur.action';
import { initialUserState, IUserState } from '../state/user.state.';

export function utilisateurReducer(state = initialUserState, action: UtilisateurActions): IUserState {
  switch (action.type) {
    case UtilisateurActionTypes.setIdentifiantUser:
      return {
        ...state,
        identifiant: action.payload,
      };
    case UtilisateurActionTypes.setUser:
      return {
        ...state,
        utilisateur: action.payload.serialize(),
      };
    case UtilisateurActionTypes.userSuccess:
      return {
        ...state,
        utilisateur: action.payload.serialize(),
      };
    default:
      return state;
  }
}
