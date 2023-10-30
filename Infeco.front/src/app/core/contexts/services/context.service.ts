import { Injectable } from '@angular/core';
import { ReLogin } from '@core/store/actions/auth.action';
import { IAppState } from '@core/store/state/app.state';
import { Store } from '@ngrx/store';

@Injectable({
  providedIn: 'root',
})
export class ContextService {
  constructor(private store: Store<IAppState>) {}

  initContext(): Promise<boolean> {
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    return new Promise<boolean>((resolve, _reject) => {
      this.store.dispatch(new ReLogin());
      // TODO refresh API
      resolve(true);
    });
  }
}
