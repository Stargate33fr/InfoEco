import { HttpResponse } from '@angular/common/http';

export interface CacheEntry {
  url: string;
  response: HttpResponse<any>;
  entryTime: number;
}

export const xkeyNotCACHE = 'X-NO-CACHE';
export const CACHE_URLS = ['donneesReference'];
