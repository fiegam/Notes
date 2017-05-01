"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
require("rxjs/add/operator/map");
var oidc_security_service_1 = require("./services/oidc.security.service");
var auth_configuration_1 = require("./auth.configuration");
var oidc_security_validation_1 = require("./services/oidc.security.validation");
var AuthModule = AuthModule_1 = (function () {
    function AuthModule() {
    }
    AuthModule.forRoot = function () {
        return {
            ngModule: AuthModule_1,
            providers: [
                oidc_security_service_1.OidcSecurityService,
                oidc_security_validation_1.OidcSecurityValidation,
                auth_configuration_1.AuthConfiguration
            ]
        };
    };
    return AuthModule;
}());
AuthModule = AuthModule_1 = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule
        ]
    })
], AuthModule);
exports.AuthModule = AuthModule;
var AuthModule_1;
//# sourceMappingURL=auth.module.js.map