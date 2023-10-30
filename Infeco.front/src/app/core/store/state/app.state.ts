import { IAuthState, initialAuthState } from './auth.state';
import { IContextState, initialContextState } from './context.state';
import { initialUserState, IUserState } from './user.state.';

export interface IAppState {
  auth: IAuthState;
  context: IContextState;
  utilisateur: IUserState;
}

export const initialAppState: IAppState = {
  auth: initialAuthState,
  context: initialContextState,
  utilisateur: initialUserState,
};

export const getInitialState = () => {
  return initialAppState;
};
