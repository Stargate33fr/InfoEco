import { IUser } from '@models/user';

export interface IUserState {
  utilisateur: IUser;
  identifiant: string;
}

export const initialUserState: IUserState = {
  utilisateur: null as any,
  identifiant: null as any,
};
