import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from '@core/services/cookie/cookie.service';
import { SetIdentifiantUser } from '@core/store/actions/utilisateur.action';
import { IAppState } from '@core/store/state/app.state';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtDto, Token, tokenAccessKey, UserDto } from 'src/app/shared/models/token';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  [x: string]: any;

  private constructor(private http: HttpClient, private cookieService: CookieService, private store: Store<IAppState>) {}

  isLoggedIn() {
    const token = this.getCookieToken();
    return token ? !new Token().getToken(token.accessToken, token.expireIn, token.tokenType, token.refreshToken).isExpired : false;
  }

  getCookieToken(): Token | undefined {
    const token = this.cookieService.get(tokenAccessKey) as Token;
    if (token) {
      return new Token().getToken(token.accessToken, token.expireIn, token.tokenType, token.refreshToken);
    }
    return undefined;
  }

  login(login: string, password: string): Observable<Token> {
    // eslint-disable-next-line @typescript-eslint/naming-convention
    const headers = new HttpHeaders({ Authorization: 'Basic ' + btoa(login + ':' + password) });
    return this.http.post<JwtDto>('/login', {}, { headers }).pipe(
      map((jwt) => {
        const user = this.decodeToken(jwt.accessToken);
        return new Token().getToken(jwt.accessToken, user.exp, '', jwt.refreshToken);
      }),
    );
  }

  public decodeToken(token: string): UserDto {
    const helper = new JwtHelperService();
    const user = helper.decodeToken(token) as UserDto;

    this.store.dispatch(new SetIdentifiantUser(user.email));
    return user;
  }
}
