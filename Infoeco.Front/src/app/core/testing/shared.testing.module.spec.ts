import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgModule } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { TransferHttpModule } from '@core/http/modules/transfer-http.module';
import { provideMockActions } from '@ngrx/effects/testing';
import { Action } from '@ngrx/store';
import { provideMockStore } from '@ngrx/store/testing';
import { EMPTY, Observable } from 'rxjs';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';

const actions$: Observable<Action> = EMPTY;

@NgModule({
  imports: [RouterTestingModule, HttpClientTestingModule, NoopAnimationsModule, NgZorroAntdModule, TransferHttpModule],
  exports: [NgZorroAntdModule, RouterTestingModule, NoopAnimationsModule, TransferHttpModule],
  providers: [provideMockStore(), provideMockActions(() => actions$)],
})
export class SharedTestingModule {}
