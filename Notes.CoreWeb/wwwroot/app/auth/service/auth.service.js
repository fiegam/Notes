"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var session_service_1 = require("./session.service");
//import { OAuthService } from 'angular-oauth2-oidc';
//import {Oidc} from "oidc-client"
//@Injectable() specifies class is available to an injector for instantiation and an injector will display an error when trying to instantiate a class that is not marked as @Injectable()
var AuthService = (function () {
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
    function AuthService(session //, private oauthService: OAuthService
    ) {
        // URL of the SPA to redirect the user to after login
        // this.oauthService.redirectUri = window.location.origin + "/index.html";
        this.session = session; //, private oauthService: OAuthService
        // The SPA's id. The SPA is registerd with this id at the auth-server
        //   this.oauthService.clientId = "notes.web";
        // set the scope for the permissions the client should request
        // The first three are defined by OIDC. The 4th is a usecase-specific one
        //  this.oauthService.scope = "openid profile email notes.api";
        // set to true, to receive also an id_token via OpenId Connect (OIDC) in addition to the
        // OAuth2-based access_token
        //  this.oauthService.oidc = true; // ID_Token
        // Use setStorage to use sessionStorage or another implementation of the TS-type Storage
        // instead of localStorage
        //  this.oauthService.setStorage(sessionStorage);
        // Discovery Document of your AuthServer as defined by OIDC
        var url = 'http://localhost:5000/.well-known/openid-configuration';
        // Load Discovery Document and then try to login the user
        //  this.oauthService.loadDiscoveryDocument(url).then(() => {
        // This method just tries to parse the token(s) within the url when
        // the auth-server redirects the user back to the web-app
        // It dosn't send the user the the login page
        //    this.oauthService.tryLogin({});
        //  });
        /* this.mgr.getUser()
             .then(user => {
                 if (user !== null)
                     this.session.userLoggedin({ name: user.profile.name, email: user.profile.email })
             });*/
    }
    AuthService.prototype.login = function () {
        //   this.oauthService.initImplicitFlow();
    };
    AuthService.prototype.logout = function () {
        //   this.oauthService.logOut();
    };
    Object.defineProperty(AuthService.prototype, "userName", {
        get: function () {
            // var claims = this.oauthService.getIdentityClaims();
            // if (!claims) return null;
            //return claims.given_name;
        },
        enumerable: true,
        configurable: true
    });
    return AuthService;
}());
AuthService = __decorate([
    core_1.Injectable(),
    __param(0, core_2.Inject(session_service_1.SessionService)),
    __metadata("design:paramtypes", [session_service_1.SessionService //, private oauthService: OAuthService
    ])
], AuthService);
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map