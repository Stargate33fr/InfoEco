import { NgModule } from '@angular/core';

import { WithLoadingPipe } from './with-loading.pipe';

@NgModule({
  exports: [WithLoadingPipe],
  declarations: [WithLoadingPipe],
  providers: [WithLoadingPipe],
})
export class WithLoadingPipeModule {}
