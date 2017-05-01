import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {Inject} from "@angular/core";
import { OidcSecurityService } from '../auth/services/oidc.security.service';
import { AuthConfiguration } from '../auth/auth.configuration';

import { SessionService } from "../auth/services/session.service";

@Component({
    selector: 'notes-header',
    templateUrl: 'app/header/templates/header.html',
    providers: [OidcSecurityService, AuthConfiguration, Router]
})
export class HeaderComponent {
    public user: User

    constructor(@Inject(OidcSecurityService) public securityService: OidcSecurityService, @Inject(SessionService) public session: SessionService) {
        this.user = session.getCurrentUser();
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