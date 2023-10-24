import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@core/core.module';
import { WithLoadingPipeModule } from '@core/pipes/with-loading.pipe.module';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { OrganismesHeaderComponent } from './organismes-header.component';

@NgModule({
  declarations: [OrganismesHeaderComponent],
  exports: [OrganismesHeaderComponent],
  imports: [CommonModule, NgZorroAntdModule, RouterModule, CoreModule, FormsModule, WithLoadingPipeModule, ReactiveFormsModule],
})
export class OrganismesHeaderModule {}
