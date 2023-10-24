import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgZorroAntdModule } from 'src/app/ngZorroAntdmodule';
import { TemplatesDashboardComponent } from './templates-dashboard.component';
import { RouterModule } from '@angular/router';
import { OrganismesDashboardModule } from '@organismes/organismes-dashboard/organismes-dashboard.module';

@NgModule({
  declarations: [TemplatesDashboardComponent],
  exports: [TemplatesDashboardComponent],
  imports: [CommonModule, NgZorroAntdModule, RouterModule, OrganismesDashboardModule],
})
export class TemplatesDashboardModule {}
