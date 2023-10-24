import { environment } from '@environment';
import { IContext } from './app-context.interface';

export class Context implements IContext {
  app!: { id: string; version: string };
  defaultWebApiUrlKey!: string;
  defaultWebApiUrlMoteur!: string;
  webApiUrlsByKeys: any;
  url!: string;
  est: boolean = true;
  constructor() {}

  public getWebApiUrl(key: string): string {
    if (key) {
      if (this.webApiUrlsByKeys[key]) {
        this.url = this.webApiUrlsByKeys[key];

        if (this.url) {
          this.url = this.url.trim();
        }
      }
      if (key.endsWith('/') && this.url) {
        if (!this.url.endsWith('/')) {
          this.url += '/';
        }
      }
    }
    return this.url;
  }

  public getWebApiUrlSpecial(key: string): string {
    if (key) {
      if (this.webApiUrlsByKeys[key]) {
        this.url = this.webApiUrlsByKeys[key];

        if (this.url) {
          this.url = this.url.trim();
        }
      }
      if (key.endsWith('/') && this.url) {
        if (!this.url.endsWith('/')) {
          this.url += '/';
        }
      }
    }
    return this.url;
  }

  public getWebApiKeys(): string[] {
    const keys = [] as string[];

    for (const key in this.webApiUrlsByKeys) {
      if (this.webApiUrlsByKeys.hasOwnProperty(key)) {
        keys.push(key);
      }
    }
    return keys;
  }

  public getAbsoluteUrl(url: string): string {
    let webApiUrl = url;
    let urlTransformed = false;
    this.getWebApiKeys().forEach((key: any) => {
      if (!urlTransformed && url.startsWith(key)) {
        webApiUrl = this.getWebApiUrl(key) + url.replace(key, '');
        urlTransformed = true;
      }
    });

    // TODO: would be better if this case is dropped, always use a prefix
    if (!urlTransformed) {
      if (url.startsWith('/')) {
        webApiUrl = this.getWebApiUrl(this.defaultWebApiUrlKey);
        if (webApiUrl) {
          if (webApiUrl.endsWith('/')) {
            webApiUrl = webApiUrl.substring(0, webApiUrl.length - 1);
          }
          webApiUrl += url;
        } else {
          webApiUrl = url;
        }
      }
    }

    return webApiUrl;
  }
}
