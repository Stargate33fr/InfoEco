import { IToken } from 'src/app/shared/models/token';

export interface IAuthState {
  isLogged: boolean;
  token: IToken | null;
}

export const initialAuthState: IAuthState = {
  isLogged: false,
  token: null,
};
