import { AuthAction, AuthActionType } from '../actions/auth.action';
import { initialAuthState } from '../state/auth.state';

export const authReducer = (state = initialAuthState, action: AuthAction) => {
  switch (action.type) {
    case AuthActionType.loginSuccess: {
      return {
        ...state,
        isLogged: true,
        token: action.payload.serialize(),
      };
    }
    case AuthActionType.loginFailure: {
      return {
        ...state,
        token: null,
      };
    }
    case AuthActionType.reLoginSuccess: {
      return {
        ...state,
        isLogged: true,
        token: action.payload.serialize(),
      };
    }
    case AuthActionType.logout: {
      return {
        ...state,
        isLogged: false,
        token: null,
      };
    }
    case AuthActionType.expiredSession: {
      return {
        ...state,
        token: null,
      };
    }
    default:
      return state;
  }
};
