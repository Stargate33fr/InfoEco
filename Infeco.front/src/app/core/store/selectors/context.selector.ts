import { Context } from '@core/contexts/models/app-context';
import { createSelector } from '@ngrx/store';

import { IAppState } from '../state/app.state';
import { IContextState } from '../state/context.state';

const selectContext = (state: IAppState) => state.context;

export const getContext = createSelector(selectContext, (state: IContextState) => state && Object.assign(new Context(), state.context));
