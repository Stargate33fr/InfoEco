import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AuthHttpInterceptor } from '@core/http/interceptors/auth/auth.interceptor';
import { CachingInterceptor } from '../interceptors/cache/caching.interceptor';
import { TransformRelativeToAbsoluteUrlInterceptor } from '../interceptors/transform-relative-to-absolute-url/transform-relative-to-absolute-url.interceptor';

@NgModule({
  imports: [],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CachingInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthHttpInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TransformRelativeToAbsoluteUrlInterceptor,
      multi: true,
    },
  ],
  exports: [],
})
export class TransferHttpModule {}
