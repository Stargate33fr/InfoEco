import { BehaviorSubject, Observable } from 'rxjs';

export class TestStore<T> {
  private state: BehaviorSubject<T | null> = new BehaviorSubject<T | null>(null);

  setState(data: T) {
    this.state.next(data);
  }
  /* eslint-disable @typescript-eslint/no-unused-vars */
  select(_selector?: any): Observable<T | null> {
    return this.state.asObservable();
  }
  /* eslint-disable @typescript-eslint/no-unused-vars */
  dispatch(_action: any) {}
}
