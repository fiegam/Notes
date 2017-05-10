import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

//import { HeaderComponent } from './header/header.component';
//import { NotesComponent } from './Notes/notes.component';

import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './not-found.component';
import { HttpModule, Http, XHRBackend, RequestOptions } from '@angular/http';
import { AuthModule } from './auth/auth.module';
import { OidcSecurityService } from './auth/services/oidc.security.service';
import { routing } from './app.routes';
import { httpFactory } from "./auth/http/http.factory";
import { SessionService } from './auth/services/session.service';
import { Router } from '@angular/router';
import {NotesModule} from './Notes/notes.module'

@NgModule({
    imports: [NgbModule.forRoot(), BrowserModule, HttpModule,
        AuthModule.forRoot(), routing, NotesModule
    ],
    declarations: [AppComponent,
        PageNotFoundComponent],
    bootstrap: [ AppComponent],
    providers: [OidcSecurityService, {
            provide: Http,
            useFactory: httpFactory,
            deps: [XHRBackend, RequestOptions, SessionService, Router]
        }, SessionService]
})
export class AppModule { }