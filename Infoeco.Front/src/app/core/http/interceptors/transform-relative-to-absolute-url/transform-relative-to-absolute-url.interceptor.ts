import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { IContext } from '@core/contexts/models/app-context.interface';
import { getContext } from '@core/store/selectors/context.selector';
import { IAppState } from '@core/store/state/app.state';
import { select, Store } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Injectable()
export class TransformRelativeToAbsoluteUrlInterceptor implements HttpInterceptor, OnDestroy {
  private context!: IContext;
  private destroy$ = new Subject<void>();

  constructor(private store: Store<IAppState>) {
    this.store.pipe(takeUntil(this.destroy$), select(getContext)).subscribe((context: IContext) => {
      this.context = context;
    });
  }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const absoluteUrl = this.transform(req.url);
    if (absoluteUrl) {
      const clonedReq = req.clone({
        url: absoluteUrl,
      });
      return next.handle(clonedReq);
    }
    return new Observable<HttpEvent<any>>();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private transform(url: string) {
    if (this.context) {
      return this.context.getAbsoluteUrl(url);
    }
    return null;
  }
}
