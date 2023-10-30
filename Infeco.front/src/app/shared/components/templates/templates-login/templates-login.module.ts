import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { TemplatesLoginComponent } from './templates-login.component';

@NgModule({
  declarations: [TemplatesLoginComponent],
  exports: [TemplatesLoginComponent],
  imports: [CommonModule, NgZorroAntdModule, ReactiveFormsModule],
})
export class TemplatesLoginModule {}
