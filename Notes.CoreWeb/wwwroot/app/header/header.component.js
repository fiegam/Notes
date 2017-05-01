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
var router_1 = require("@angular/router");
var core_2 = require("@angular/core");
var oidc_security_service_1 = require("../auth/services/oidc.security.service");
var auth_configuration_1 = require("../auth/auth.configuration");
var session_service_1 = require("../auth/services/session.service");
var HeaderComponent = (function () {
    function HeaderComponent(securityService, session) {
        this.securityService = securityService;
        this.session = session;
        this.user = session.getCurrentUser();
    }
    HeaderComponent.prototype.ngOnInit = function () {
        console.log('ngOnInit _securityService.AuthorizedCallback');
        if (window.location.hash) {
            this.securityService.AuthorizedCallback();
        }
    };
    HeaderComponent.prototype.Login = function () {
        console.log('Do login logic');
        this.securityService.Authorize();
    };
    HeaderComponent.prototype.Logout = function () {
        console.log('Do logout logic');
        this.securityService.Logoff();
    };
    return HeaderComponent;
}());
HeaderComponent = __decorate([
    core_1.Component({
        selector: 'notes-header',
        templateUrl: 'app/header/templates/header.html',
        providers: [oidc_security_service_1.OidcSecurityService, auth_configuration_1.AuthConfiguration, router_1.Router]
    }),
    __param(0, core_2.Inject(oidc_security_service_1.OidcSecurityService)), __param(1, core_2.Inject(session_service_1.SessionService)),
    __metadata("design:paramtypes", [oidc_security_service_1.OidcSecurityService, session_service_1.SessionService])
], HeaderComponent);
exports.HeaderComponent = HeaderComponent;
//# sourceMappingURL=header.component.js.map