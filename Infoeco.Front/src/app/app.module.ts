import { CommonModule, DOCUMENT, registerLocaleData } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { ContextService } from '@core/contexts/services/context.service';
import { TransferHttpModule } from '@core/http/modules/transfer-http.module';
import { AuthEffects } from '@core/store/effects/auth.effect';
import { appReducers } from '@core/store/reducers/app.reducer';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { PagesComponent } from '@pages/pages.component';

import fr from '@angular/common/locales/fr';
import { FormsModule } from '@angular/forms';
import { UtilisateurEffects } from '@core/store/effects/utilisateur.effect';
import { OrganismesHeaderModule } from '@organismes/organismes-header/organismes-header.module';
import { fr_FR, NZ_I18N } from 'ng-zorro-antd/i18n';
import { AppComponent } from './app.component';
import { appRoutes } from './app.routes';
import { extModules } from './build-specifics';
import { SecurityModule } from './core/security/security.module';
import { NgZorroAntdModule } from './ngZorroAntdmodule';
import { WithLoadingPipeModule } from '@core/pipes/with-loading.pipe.module';
import { NumberFormatPipe } from '@core/pipes/numberFormat.pipe';

registerLocaleData(fr);

@NgModule({
  declarations: [AppComponent, PagesComponent],
  imports: [
    BrowserModule,
    CommonModule,
    RouterModule.forRoot(appRoutes),
    StoreModule.forRoot(appReducers, {
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true,
        strictStateSerializability: true,
        strictActionSerializability: false,
      },
    }),
    // Instrumentation must be imported after importing StoreModule
    extModules,
    BrowserAnimationsModule,
    TransferHttpModule,
    NgZorroAntdModule,
    EffectsModule.forRoot([AuthEffects, UtilisateurEffects]),
    SecurityModule,
    HttpClientModule,
    FormsModule,
    OrganismesHeaderModule,
    WithLoadingPipeModule,
  ],
  providers: [
    NumberFormatPipe,
    {
      provide: APP_INITIALIZER,
      useFactory: onAppInit,
      multi: true,
      deps: [ContextService, DOCUMENT],
    },
    { provide: NZ_I18N, useValue: fr_FR },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

// eslint-disable-next-line prefer-arrow/prefer-arrow-functions
export function onAppInit(contextService: ContextService, document: Document): () => Promise<any> {
  const appInit = (): Promise<any> =>
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    new Promise((resolve, _reject) => {
      // init browser history in case of direct navigation
      let location = document?.defaultView?.location.pathname;
      if (document.defaultView?.location.search) {
        location += document.defaultView.location.search;
      }

      document.defaultView?.history.replaceState(null, '', '/');
      document.defaultView?.history.pushState({ directcall: true }, '', location);

      // If there is this variable it means that we are in tests environment
      // In test environment we don'molecules need to init settings on app startup
      const karma = window[0];
      if (karma) {
        resolve(null);
      } else {
        // By this check we sure that it is browser/server environment
        // In browser/server environment we need to do some basic staff
        contextService.initContext().then(() => {
          resolve(null);
        });
      }
    });
  return appInit;
}
