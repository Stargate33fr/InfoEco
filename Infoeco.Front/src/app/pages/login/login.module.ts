import { NgModule } from '@angular/core';
import { TemplatesLoginModule } from '@templates/templates-login/templates-login.module';
import { LoginRoutingModule } from './login-routing.module.';

import { LoginComponent } from './login.component';

@NgModule({
  declarations: [LoginComponent],
  imports: [LoginRoutingModule, TemplatesLoginModule],
})
export class LoginModule {}
