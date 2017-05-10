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
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var oidc_security_service_1 = require("./auth/services/oidc.security.service");
var session_service_1 = require("./auth/services/session.service");
//import { NotesComponent } from './Notes/notes.component';
//import { HeaderComponent } from './header/header.component';
//import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
//import { SecureFilesComponent } from './securefile/securefiles.component';
//import './app.component.css';
var AppComponent = (function () {
    function AppComponent(securityService, _sessionService) {
        var _this = this;
        this.securityService = securityService;
        this._sessionService = _sessionService;
        this._sessionService.unauthorized.subscribe(function () { return _this.Login(); });
    }
    AppComponent.prototype.ngOnInit = function () {
        console.log('ngOnInit _securityService.AuthorizedCallback');
        if (window.location.hash) {
            this.securityService.AuthorizedCallback();
        }
    };
    AppComponent.prototype.Login = function () {
        console.log('Do login logic');
        this.securityService.Authorize();
    };
    AppComponent.prototype.Logout = function () {
        console.log('Do logout logic');
        this.securityService.Logoff();
    };
    return AppComponent;
}());
AppComponent = __decorate([
    core_1.Component({
        selector: 'my-app',
        templateUrl: 'app/app.component.html',
    }),
    __metadata("design:paramtypes", [oidc_security_service_1.OidcSecurityService, session_service_1.SessionService])
], AppComponent);
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map