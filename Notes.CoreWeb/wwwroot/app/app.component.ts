import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


import { OidcSecurityService } from './auth/services/oidc.security.service';

//import { NotesComponent } from './Notes/notes.component';
//import { HeaderComponent } from './header/header.component';
//import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
//import { SecureFilesComponent } from './securefile/securefiles.component';


//import './app.component.css';

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html'
})

export class AppComponent implements OnInit {

    constructor(public securityService: OidcSecurityService) {
    }

    ngOnInit() {
        console.log('ngOnInit _securityService.AuthorizedCallback');

        if (window.location.hash) {
            this.securityService.AuthorizedCallback();
        }
    }

    public Login() {
        console.log('Do login logic');
        this.securityService.Authorize();
    }

    public Logout() {
        console.log('Do logout logic');
        this.securityService.Logoff();
    }
}