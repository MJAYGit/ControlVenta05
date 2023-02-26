import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

export function getBaseUrlBackEndAutorizacion() {
  return environment.apiUrlauth;
}

export function getBaseUrlBackEnd() {
  return environment.apiUrlventa;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'BASE_URL_BACKEND_AUT', useFactory: getBaseUrlBackEndAutorizacion, deps: [] }, // mjay
  { provide: 'BASE_URL_BACKEND', useFactory: getBaseUrlBackEnd, deps: [] } // mjay  
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
