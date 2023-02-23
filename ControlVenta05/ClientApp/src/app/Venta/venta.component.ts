import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { forkJoin, throwError, pipe, BehaviorSubject, Observable, from } from 'rxjs';
import { tap, catchError, flatMap } from 'rxjs/operators';


@Component({
  selector: 'app-venta-component',
  templateUrl: './venta.component.html'
})

@Injectable({ providedIn: 'root' })


export class VentaComponent {

  public vVentasCli: VentaCli[];
  private auth_token: string = "";
  public ls_Authorization: string = "";
  public xpArray: number[] = [1];
  ;

  constructor(http: HttpClient, @Inject('BASE_URL_BACKEND') baseUrl: string) {

    let oUsuario: cUsuario = new cUsuario();
    oUsuario.UsuarioNombre = "Admin";

    new Observable((observer) => { observer.next(1) })
      .pipe(
        flatMap(() => this.ObtenerToken(http, baseUrl, oUsuario)),
        flatMap(() => this.ListarVentas(http, baseUrl))
      )
      .subscribe(
        res => {
          console.log('Se ejecuto todo con exito', res);
        },
        err => {
          console.error('Ocurrio un error', err);
        }
      );
  }


  ListarVentas(http: HttpClient, @Inject('BASE_URL_BACKEND') baseUrl: string) {

    this.ls_Authorization = "Bearer " + this.auth_token;

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': this.ls_Authorization,
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Credentials': 'false',
      'Access-Control-Allow-Methods': 'POST,PUT,PATCH,GET,DELETE,OPTIONS',
      'Access-Control-Allow-Headers': '*'
    });

    const requestOptions = { headers: headers };
    console.log('paso listar');
    return forkJoin(

      this.xpArray.map(xpItem =>
        http.get<VentaCli[]>(baseUrl, requestOptions).pipe(tap((result) => this.vVentasCli = result))
      )

    );
  }


  ObtenerToken(http: HttpClient, baseUrl: string, aUsuario: cUsuario) {

    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Credentials': 'false',
      'Access-Control-Allow-Methods': 'POST,PUT,PATCH,GET,DELETE,OPTIONS',
      'Access-Control-Allow-Headers': '*'
    });

    const requestOptions = { headers: httpOptions };
    console.log('paso token');
    return forkJoin(
      this.xpArray.map(xpItem =>
        http.post<cToken>(baseUrl + "Autenticar", aUsuario, requestOptions)
          .pipe(tap((result: cToken) => this.auth_token = result.token))
      )
    )
  }
}

interface VentaCli {
  id: number;
  assesorComercial: string;
  fecha: string;
  producto: string;
  cantidad: number;
  precio: number;
}

class cUsuario {
  UsuarioNombre: string;
}

class cToken {
  token: string;
}
