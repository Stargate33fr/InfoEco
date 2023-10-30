import { IDeserializable } from '@core/model/deserializable.interface';
import * as dayjs from 'dayjs';

export interface IToken {
  accessToken: string;
  tokenType: string;
  expireIn: number;
  refreshToken: string;
}

export class Token implements IToken, IDeserializable<Token> {
  accessToken: string;
  tokenType: string;
  refreshToken: string;
  expireIn: number;

  deserialize(token: IToken | null): Token {
    if (token) {
      return this.getToken(token.accessToken, token.expireIn, token.tokenType, token.refreshToken);
    }
    return null as any;
  }

  serialize(): IToken {
    return {
      accessToken: this.accessToken,
      expireIn: this.expireIn,
      tokenType: this.tokenType,
      refreshToken: this.refreshToken,
    };
  }

  // eslint-disable-next-line @typescript-eslint/member-ordering
  get isExpired(): boolean {
    return dayjs().isAfter(dayjs.unix(this.expireIn));
  }

  // eslint-disable-next-line @typescript-eslint/member-ordering
  get expiresIn(): Date {
    return dayjs.unix(this.expireIn).toDate();
  }

  public getToken(accessToken: string, expiresIn: number, tokenType: string, refreshToken: string): Token {
    const token = new Token();
    token.accessToken = accessToken;
    token.tokenType = tokenType;
    token.expireIn = expiresIn;
    token.refreshToken = refreshToken;

    return token;
  }
}

export const tokenAccessKey = 'ISATV6';
export const tokenRefreshToken = 'ISRTV6';

export interface JwtDto {
  accessToken: string;
  refreshToken: string;
}

export interface UserDto {
  email: string;
  name: string;
  role: string;
  nbf: string;
  aud: string;
  exp: number;
  iss: string;
  scope: string;
  sub: string;
}
