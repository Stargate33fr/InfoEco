import { NgModule } from '@angular/core';
import { MypercentPipe } from './custom.percent.pipe';

@NgModule({
  exports: [MypercentPipe],
  declarations: [MypercentPipe],
  providers: [MypercentPipe],
})
export class MypercentPipeModule {}
