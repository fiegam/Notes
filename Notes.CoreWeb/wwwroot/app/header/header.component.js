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
var core_1 = require('@angular/core');
var core_2 = require("@angular/core");
var session_service_1 = require("../auth/service/session.service");
var auth_service_1 = require("../auth/service/auth.service");
var HeaderComponent = (function () {
    function HeaderComponent(session, auth) {
        this.session = session;
        this.auth = auth;
        this.user = session.getCurrentUser();
    }
    HeaderComponent = __decorate([
        core_1.Component({
            selector: 'notes-header',
            templateUrl: 'app/header/templates/header.html',
            providers: [auth_service_1.AuthService, session_service_1.SessionService],
        }),
        __param(0, core_2.Inject(session_service_1.SessionService)),
        __param(1, core_2.Inject(auth_service_1.AuthService)), 
        __metadata('design:paramtypes', [session_service_1.SessionService, auth_service_1.AuthService])
    ], HeaderComponent);
    return HeaderComponent;
}());
exports.HeaderComponent = HeaderComponent;
//# sourceMappingURL=header.component.js.map