import { NgModule } from '@angular/core';

import { LoggedUsersGuard } from './guards/logged-users-guard';
import { UnLoggedUsersGuard } from './guards/unlogged-users-guard';

@NgModule({
  imports: [],
  exports: [],
  declarations: [],
  providers: [UnLoggedUsersGuard, LoggedUsersGuard],
})
export class SecurityModule {}
