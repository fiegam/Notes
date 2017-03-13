import {Injectable} from "@angular/core";
import {Inject} from "@angular/core";
import {SessionService} from "./session.service";
import { OAuthService } from 'angular-oauth2-oidc';
//import {Oidc} from "oidc-client"

//@Injectable() specifies class is available to an injector for instantiation and an injector will display an error when trying to instantiate a class that is not marked as @Injectable()

@Injectable()

export class AuthService {
    /*private config = {
        authority: "http://localhost:5000",
        client_id: "notes.web",
        redirect_uri: "http://localhost:5003/callback.html",
        response_type: "id_token token",
        scope: "openid profile notes.api",
        post_logout_redirect_uri: "http://localhost:5003/index.html",
    };
    private mgr: Oidc.UserManager = new Oidc.UserManager(this.config);
    */
    constructor( @Inject(SessionService) private session: SessionService, private oauthService: OAuthService) {

        // URL of the SPA to redirect the user to after login
        this.oauthService.redirectUri = window.location.origin + "/index.html";

        // The SPA's id. The SPA is registerd with this id at the auth-server
        this.oauthService.clientId = "notes.web";

        // set the scope for the permissions the client should request
        // The first three are defined by OIDC. The 4th is a usecase-specific one
        this.oauthService.scope = "openid profile email notes.api";

        // set to true, to receive also an id_token via OpenId Connect (OIDC) in addition to the
        // OAuth2-based access_token
        this.oauthService.oidc = true; // ID_Token

        // Use setStorage to use sessionStorage or another implementation of the TS-type Storage
        // instead of localStorage
        this.oauthService.setStorage(sessionStorage);

        // Discovery Document of your AuthServer as defined by OIDC
        let url = 'http://localhost:5000/.well-known/openid-configuration';

        // Load Discovery Document and then try to login the user
        this.oauthService.loadDiscoveryDocument(url).then(() => {

            // This method just tries to parse the token(s) within the url when
            // the auth-server redirects the user back to the web-app
            // It dosn't send the user the the login page
            this.oauthService.tryLogin({});

        });
       

       /* this.mgr.getUser()
            .then(user => {
                if (user !== null)
                    this.session.userLoggedin({ name: user.profile.name, email: user.profile.email })
            });*/
    }

    public login() {
        this.oauthService.initImplicitFlow();
    }

    public logout() {
        this.oauthService.logOut();
    }

    public get userName() {

        var claims = this.oauthService.getIdentityClaims();
        if (!claims) return null;

        return claims.given_name;
    }

}