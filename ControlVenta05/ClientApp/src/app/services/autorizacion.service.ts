import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Autorizacion } from '../models/autorizacion';

@Injectable({ providedIn: 'root' })

export class AutorizacionService {
  _baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL_BACKEND_AUT') baseUrl: string) {
    this._baseUrl = baseUrl;
  }
   
  ObtenerToken(aUsuario: Autorizacion) {
    const httpOptions = {
      headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Credentials': 'false',
      'Access-Control-Allow-Methods': 'POST,PUT,PATCH,GET,DELETE,OPTIONS',
      'Access-Control-Allow-Headers': '*'
      })
    };  

    console.log('paso token');
    return this.http.post<Autorizacion>(this._baseUrl + "Autenticar", aUsuario, httpOptions);
  }

}
