"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
//import { HeaderComponent } from './header/header.component';
//import { NotesComponent } from './Notes/notes.component';
var app_component_1 = require("./app.component");
var not_found_component_1 = require("./not-found.component");
var http_1 = require("@angular/http");
var auth_module_1 = require("./auth/auth.module");
var oidc_security_service_1 = require("./auth/services/oidc.security.service");
var app_routes_1 = require("./app.routes");
var http_factory_1 = require("./auth/http/http.factory");
var session_service_1 = require("./auth/services/session.service");
var router_1 = require("@angular/router");
var notes_module_1 = require("./Notes/notes.module");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [ng_bootstrap_1.NgbModule.forRoot(), platform_browser_1.BrowserModule, http_1.HttpModule,
            auth_module_1.AuthModule.forRoot(), app_routes_1.routing, notes_module_1.NotesModule
        ],
        declarations: [app_component_1.AppComponent,
            not_found_component_1.PageNotFoundComponent],
        bootstrap: [app_component_1.AppComponent],
        providers: [oidc_security_service_1.OidcSecurityService, {
                provide: http_1.Http,
                useFactory: http_factory_1.httpFactory,
                deps: [http_1.XHRBackend, http_1.RequestOptions, session_service_1.SessionService, router_1.Router]
            }, session_service_1.SessionService]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map