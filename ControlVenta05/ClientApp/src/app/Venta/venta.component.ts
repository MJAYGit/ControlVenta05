import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { forkJoin, throwError, pipe, BehaviorSubject, Observable, from } from 'rxjs';
import { tap, catchError, flatMap } from 'rxjs/operators';
import { Venta } from '../models/venta';
import { Autorizacion } from '../models/autorizacion';
import { VentaService } from '../services/venta.service';
import { AutorizacionService } from '../services/autorizacion.service';

@Component({
  selector: 'app-venta-component',
  templateUrl: './venta.component.html'
})

@Injectable({ providedIn: 'root' })


export class VentaComponent {

  public vVentasCli: Venta[];
  public vAutorizacion: Autorizacion;
  private auth_token: string = "";
  public xpArray: number[] = [1];
  public sHost: string = null;
  public sPuerto: string = null;
  public bCarga = false;
  public sError: string = "";

  constructor(private aVentaService: VentaService, private aAutorizacionService: AutorizacionService) {
    this.sHost = "localhost";
  }

  CargarData() {
    this.bCarga = true;
    this.sError = "";
    this.vVentasCli = null;
    new Observable((observer) => { observer.next(1) })
      .pipe(
        flatMap(() => this.ObtenerToken()),
        flatMap(() => this.ListarVentas())
      )
      .subscribe(
        res => {
          console.log('Se ejecuto todo con exito', res);
        },
        err => {
          console.error('Ocurrio un error', err);
          this.sError = "Ocurrio un error: "+err.message;
          this.bCarga = false; 
        },
        ()=> {this.bCarga = false; }
    );
    
  }

  ObtenerToken() {
    let oAutorizacion: Autorizacion = new Autorizacion();
    oAutorizacion.usuarioNombre = "Admin";

    return forkJoin(
      this.xpArray.map(xpItem =>
        this.aAutorizacionService.ObtenerToken(oAutorizacion, this.sHost, this.sPuerto)
          .pipe(tap((result: Autorizacion) => this.auth_token = result.token))
      )
    );
  }

  ListarVentas() {
    return forkJoin( 
      this.xpArray.map(xpItem =>
        this.aVentaService.ListarVentas(this.auth_token, this.sHost, this.sPuerto)
          .pipe(tap((result) => this.vVentasCli = result))
      ) 
    );
  } 
}

 
