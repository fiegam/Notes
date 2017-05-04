import { NgModule, ModuleWithProviders, Injectable } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Http, Response, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';

import { OidcSecurityService } from './services/oidc.security.service';
import { AuthConfiguration } from './auth.configuration';
import { OidcSecurityValidation } from './services/oidc.security.validation';
import { SessionService } from './services/session.service';
import { TempDataStore } from './services/tempData.store';
import { InterceptedHttp } from './http/http.interceptor';

@NgModule({
    imports: [
        CommonModule
    ]
})

export class AuthModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: AuthModule,
            providers: [
                OidcSecurityService,
                OidcSecurityValidation,
                AuthConfiguration,
                SessionService,
                TempDataStore,
                InterceptedHttp
            ]
        };
    }
}