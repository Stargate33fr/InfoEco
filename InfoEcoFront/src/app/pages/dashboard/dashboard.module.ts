import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TemplatesDashboardModule } from '@templates/templates-dashboard/templates-dashboard.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';

@NgModule({
  declarations: [DashboardComponent],
  imports: [CommonModule, DashboardRoutingModule, TemplatesDashboardModule],
})
export class DashboardModule {}
