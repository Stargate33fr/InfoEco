import { IContext } from '@core/contexts/models/app-context.interface';
import { environment } from '@environment';

const context = environment.context;

export interface IContextState {
  context: IContext;
}

export const initialContextState: IContextState = {
  context: {
    app: { id: context.app.id, version: context.app.version },
    defaultWebApiUrlKey: context.defaultWebApiUrlKey,
    webApiUrlsByKeys: context.urls,
  } as IContext,
};
