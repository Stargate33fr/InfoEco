import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { MoleculesVilleComponent } from './molecules-ville.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { WithLoadingPipeModule } from '@core/pipes/with-loading.pipe.module';

@NgModule({
  declarations: [MoleculesVilleComponent],
  exports: [MoleculesVilleComponent],
  imports: [CommonModule, WithLoadingPipeModule, NgZorroAntdModule, FormsModule, ReactiveFormsModule],
})
export class MoleculesVilleModule {}
