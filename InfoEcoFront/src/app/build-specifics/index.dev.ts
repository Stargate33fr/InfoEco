import { StoreDevtoolsModule } from '@ngrx/store-devtools';

export const extModules = [
  StoreDevtoolsModule.instrument({
    name: 'Store DevTools',
    maxAge: 20,
  }),
];
