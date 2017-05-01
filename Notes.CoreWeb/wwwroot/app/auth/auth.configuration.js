"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var AuthConfiguration = (function () {
    function AuthConfiguration() {
        // The Issuer Identifier for the OpenID Provider (which is typically obtained during Discovery) MUST exactly match the value of the iss (issuer) Claim.
        this.iss = 'http://localhost:5000';
        this.server = 'http://localhost:5000';
        this.redirect_url = 'http://localhost:5003';
        // This is required to get the signing keys so that the signiture of the Jwt can be validated.
        this.jwks_url = 'http://localhost:5000/.well-known/openid-configuration/jwks';
        this.userinfo_url = 'http://localhost:5000/connect/userinfo';
        this.logoutEndSession_url = 'http://localhost:5000/connect/endsession';
        // The Client MUST validate that the aud (audience) Claim contains its client_id value registered at the Issuer identified by the iss (issuer) Claim as an audience.
        // The ID Token MUST be rejected if the ID Token does not list the Client as a valid audience, or if it contains additional audiences not trusted by the Client.
        this.client_id = 'notes.web';
        this.response_type = 'id_token token';
        this.scope = 'openid profile notes.api';
        this.post_logout_redirect_uri = 'http://localhost:5003/index.html';
    }
    return AuthConfiguration;
}());
AuthConfiguration = __decorate([
    core_1.Injectable()
], AuthConfiguration);
exports.AuthConfiguration = AuthConfiguration;
//# sourceMappingURL=auth.configuration.js.map