"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getBaseUrlBackEnd = exports.getBaseUrlBackEndAutorizacion = exports.getBaseUrl = void 0;
var core_1 = require("@angular/core");
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var app_module_1 = require("./app/app.module");
var environment_1 = require("./environments/environment");
function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
exports.getBaseUrl = getBaseUrl;
function getBaseUrlBackEndAutorizacion() {
    return environment_1.environment.apiUrlauth;
}
exports.getBaseUrlBackEndAutorizacion = getBaseUrlBackEndAutorizacion;
function getBaseUrlBackEnd() {
    return environment_1.environment.apiUrlventa;
}
exports.getBaseUrlBackEnd = getBaseUrlBackEnd;
var providers = [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
    { provide: 'BASE_URL_BACKEND_AUT', useFactory: getBaseUrlBackEndAutorizacion, deps: [] },
    { provide: 'BASE_URL_BACKEND', useFactory: getBaseUrlBackEnd, deps: [] } // mjay  
];
if (environment_1.environment.production) {
    (0, core_1.enableProdMode)();
}
(0, platform_browser_dynamic_1.platformBrowserDynamic)(providers).bootstrapModule(app_module_1.AppModule)
    .catch(function (err) { return console.log(err); });
//# sourceMappingURL=main.js.map