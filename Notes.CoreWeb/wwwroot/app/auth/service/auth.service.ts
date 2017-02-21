import {Injectable} from "@angular/core";
import {Inject} from "@angular/core";
import {SessionService} from "./session.service";
//import {Oidc} from "oidc-client"

//@Injectable() specifies class is available to an injector for instantiation and an injector will display an error when trying to instantiate a class that is not marked as @Injectable()

@Injectable()

export class AuthService {
    private config = {
        authority: "http://localhost:5000",
        client_id: "notes.web",
        redirect_uri: "http://localhost:5003/callback.html",
        response_type: "id_token token",
        scope: "openid profile notes.api",
        post_logout_redirect_uri: "http://localhost:5003/index.html",
    };
    private mgr: Oidc.UserManager = new Oidc.UserManager(this.config);

    constructor( @Inject(SessionService) private session: SessionService) {
        this.mgr.getUser()
            .then(user => {
                if (user !== null)
                    this.session.userLoggedin({ name: user.profile.name, email: user.profile.email })
            });
    }

    public login() {
        this.mgr.signinRedirect();
    }

    public logout() {
        this.mgr.signoutRedirect();
    }
}