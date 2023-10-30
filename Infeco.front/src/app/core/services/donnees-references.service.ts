import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseAPIDto } from '@core/dto/response-api';
import { ISelect } from '@models/select';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DonneesReferencesService {
  constructor(private http: HttpClient) {}

  civilite(): Observable<ISelect[]> {
    return this.http.get<ResponseAPIDto<ISelect[]>>('/donneesReference/civilites').pipe(map((response) => response.contenu));
  }

  typeAppartement(): Observable<ISelect[]> {
    return this.http.get<ResponseAPIDto<ISelect[]>>('/donneesReference/typeAppartement').pipe(map((response) => response.contenu));
  }

  typePaiement(): Observable<ISelect[]> {
    return this.http.get<ResponseAPIDto<ISelect[]>>('/donneesReference/typePaiement').pipe(map((response) => response.contenu));
  }

  ville(recherche: string) {
    return this.http.get<ResponseAPIDto<ISelect[]>>(`/donneesReference/ville/${recherche}`).pipe(map((response) => response.contenu));
  }
}
