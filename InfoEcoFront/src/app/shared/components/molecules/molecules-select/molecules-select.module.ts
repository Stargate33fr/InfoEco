import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { MoleculesSelectComponent } from './molecules-select.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { WithLoadingPipeModule } from '@core/pipes/with-loading.pipe.module';

@NgModule({
  declarations: [MoleculesSelectComponent],
  exports: [MoleculesSelectComponent],
  imports: [CommonModule, WithLoadingPipeModule, NgZorroAntdModule, FormsModule, ReactiveFormsModule],
})
export class MoleculesSelectModule {}
