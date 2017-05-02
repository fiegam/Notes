﻿import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
//import { HeaderComponent } from './header/header.component';
import { NotesComponent } from './Notes/notes.component';
import { AppComponent } from './app.component';
//import {NotesService} from "./Notes/services/NotesService";
import { HttpModule, Http, XHRBackend, RequestOptions } from '@angular/http';
import { AuthModule } from './auth/auth.module';
import { OidcSecurityService } from './auth/services/oidc.security.service';
import { routing } from './app.routes';
import {httpFactory} from "./auth/http/http.factory";

@NgModule({
    imports: [NgbModule.forRoot(), BrowserModule, HttpModule,
        AuthModule.forRoot(), routing,
    ],
    declarations: [AppComponent, NotesComponent],
    bootstrap: [ AppComponent, NotesComponent],
    providers: [OidcSecurityService, {
            provide: Http,
            useFactory: httpFactory,
            deps: [XHRBackend, RequestOptions]
        }]
})
export class AppModule { }