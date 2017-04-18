import { Component } from '@angular/core';
import {Inject} from "@angular/core";
//import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {SessionService} from "../auth/service/session.service";
import {AuthService} from "../auth/service/auth.service";

@Component({
    selector: 'notes-header',
    templateUrl: 'app/header/templates/header.html',
    providers: [AuthService, SessionService],
})
export class HeaderComponent {
    public user: User

    constructor( @Inject(SessionService) public session: SessionService, @Inject(AuthService) public auth: AuthService) {
        this.user = session.getCurrentUser();
    }


}