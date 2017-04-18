import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from './header/header.component';
import { NotesComponent } from './notes/notes.component';
import {NotesService} from "./Notes/services/NotesService";
import { HttpModule } from '@angular/http';
//import { OAuthModule } from 'angular-oauth2-oidc';
@NgModule({
    imports: [NgbModule.forRoot(), BrowserModule, HttpModule,
        // OAuthModule.forRoot()
    ],
    declarations: [NotesComponent, HeaderComponent],
    bootstrap: [NotesComponent, HeaderComponent],
})
export class NotesModule { }