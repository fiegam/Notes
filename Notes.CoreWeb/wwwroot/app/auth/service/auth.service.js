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
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var session_service_1 = require("./session.service");
//import {Oidc} from "oidc-client"
//@Injectable() specifies class is available to an injector for instantiation and an injector will display an error when trying to instantiate a class that is not marked as @Injectable()
var AuthService = (function () {
    function AuthService(session) {
        var _this = this;
        this.session = session;
        this.config = {
            authority: "http://localhost:5000",
            client_id: "notes.web",
            redirect_uri: "http://localhost:5003/callback.html",
            response_type: "id_token token",
            scope: "openid profile notes.api",
            post_logout_redirect_uri: "http://localhost:5003/index.html",
        };
        this.mgr = new Oidc.UserManager(this.config);
        this.mgr.getUser()
            .then(function (user) {
            if (user !== null)
                _this.session.userLoggedin({ name: user.profile.name, email: user.profile.email });
        });
    }
    AuthService.prototype.login = function () {
        this.mgr.signinRedirect();
    };
    AuthService.prototype.logout = function () {
        this.mgr.signoutRedirect();
    };
    AuthService = __decorate([
        core_1.Injectable(),
        __param(0, core_2.Inject(session_service_1.SessionService)), 
        __metadata('design:paramtypes', [session_service_1.SessionService])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map