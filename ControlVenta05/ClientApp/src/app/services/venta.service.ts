import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Venta } from '../models/venta';

@Injectable({ providedIn: 'root' })

export class VentaService {
   _baseUrl: string;

constructor(private http: HttpClient, @Inject('BASE_URL_BACKEND') baseUrl: string) {
  this._baseUrl = baseUrl;
}

  ListarVentas( asToken: string) {
 
    const sAuthorization = "Bearer " + asToken;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': sAuthorization,
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': 'false',
        'Access-Control-Allow-Methods': 'POST,PUT,PATCH,GET,DELETE,OPTIONS',
        'Access-Control-Allow-Headers': '*'
      })
    };  

    console.log('paso listar');
    return this.http.get<Venta[]>(this._baseUrl, httpOptions);
  }

}
