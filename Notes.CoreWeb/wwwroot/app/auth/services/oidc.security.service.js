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
var http_1 = require("@angular/http");
require("rxjs/add/operator/map");
require("rxjs/add/operator/catch");
var Rx_1 = require("rxjs/Rx");
var router_1 = require("@angular/router");
var auth_configuration_1 = require("../auth.configuration");
var oidc_security_validation_1 = require("./oidc.security.validation");
var OidcSecurityService = (function () {
    function OidcSecurityService(_http, _configuration, _router) {
        var _this = this;
        this._http = _http;
        this._configuration = _configuration;
        this._router = _router;
        this.getUserData = function () {
            _this.setHeaders();
            return _this._http.get(_this._configuration.userinfo_url, {
                headers: _this.headers,
                body: ''
            }).map(function (res) { return res.json(); });
        };
        this.oidcSecurityValidation = new oidc_security_validation_1.OidcSecurityValidation();
        this.headers = new http_1.Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');
        this.storage = localStorage;
        if (this.retrieve('_isAuthorized') !== '') {
            this.HasAdminRole = this.retrieve('HasAdminRole');
            this._isAuthorized = this.retrieve('_isAuthorized');
        }
    }
    OidcSecurityService.prototype.IsAuthorized = function () {
        if (this._isAuthorized) {
            if (this.oidcSecurityValidation.IsTokenExpired(this.retrieve('authorizationDataIdToken'))) {
                console.log('IsAuthorized: isTokenExpired');
                this.ResetAuthorizationData();
                return false;
            }
            return true;
        }
        return false;
    };
    OidcSecurityService.prototype.GetToken = function () {
        return this.retrieve('authorizationData');
    };
    OidcSecurityService.prototype.ResetAuthorizationData = function () {
        this.store('authorizationData', '');
        this.store('authorizationDataIdToken', '');
        this._isAuthorized = false;
        this.HasAdminRole = false;
        this.store('HasAdminRole', false);
        this.store('_isAuthorized', false);
    };
    OidcSecurityService.prototype.SetAuthorizationData = function (token, id_token) {
        var _this = this;
        if (this.retrieve('authorizationData') !== '') {
            this.store('authorizationData', '');
        }
        console.log(token);
        console.log(id_token);
        console.log('storing to storage, getting the roles');
        this.store('authorizationData', token);
        this.store('authorizationDataIdToken', id_token);
        this._isAuthorized = true;
        this.store('_isAuthorized', true);
        this.getUserData()
            .subscribe(function (data) { return _this.UserData = data; }, function (error) { return _this.HandleError(error); }, function () {
            for (var i = 0; i < _this.UserData.role.length; i++) {
                if (_this.UserData.role[i] === 'dataEventRecords.admin') {
                    _this.HasAdminRole = true;
                    _this.store('HasAdminRole', true);
                }
                if (_this.UserData.role[i] === 'admin') {
                    _this.HasUserAdminRole = true;
                    _this.store('HasUserAdminRole', true);
                }
            }
        });
    };
    OidcSecurityService.prototype.Authorize = function () {
        this.ResetAuthorizationData();
        console.log('BEGIN Authorize, no auth data');
        var authorizationUrl = this._configuration.server + '/connect/authorize';
        var client_id = this._configuration.client_id;
        var redirect_uri = this._configuration.redirect_url;
        var response_type = this._configuration.response_type;
        var scope = this._configuration.scope;
        var nonce = 'N' + Math.random() + '' + Date.now();
        var state = Date.now() + '' + Math.random();
        this.store('authStateControl', state);
        this.store('authNonce', nonce);
        console.log('AuthorizedController created. adding myautostate: ' + this.retrieve('authStateControl'));
        var url = authorizationUrl + '?' +
            'response_type=' + encodeURI(response_type) + '&' +
            'client_id=' + encodeURI(client_id) + '&' +
            'redirect_uri=' + encodeURI(redirect_uri) + '&' +
            'scope=' + encodeURI(scope) + '&' +
            'nonce=' + encodeURI(nonce) + '&' +
            'state=' + encodeURI(state);
        window.location.href = url;
    };
    OidcSecurityService.prototype.AuthorizedCallback = function () {
        var _this = this;
        console.log('BEGIN AuthorizedCallback, no auth data');
        this.ResetAuthorizationData();
        var hash = window.location.hash.substr(1);
        var result = hash.split('&').reduce(function (result, item) {
            var parts = item.split('=');
            result[parts[0]] = parts[1];
            return result;
        }, {});
        console.log(result);
        console.log('AuthorizedCallback created, begin token validation');
        var token = '';
        var id_token = '';
        var authResponseIsValid = false;
        this.getSigningKeys()
            .subscribe(function (jwtKeys) {
            _this.jwtKeys = jwtKeys;
            if (!result.error) {
                // validate state
                if (_this.oidcSecurityValidation.ValidateStateFromHashCallback(result.state, _this.retrieve('authStateControl'))) {
                    token = result.access_token;
                    id_token = result.id_token;
                    var decoded = void 0;
                    var headerDecoded = void 0;
                    decoded = _this.oidcSecurityValidation.GetPayloadFromToken(id_token, false);
                    headerDecoded = _this.oidcSecurityValidation.GetHeaderFromToken(id_token, false);
                    // validate jwt signature
                    if (_this.oidcSecurityValidation.Validate_signature_id_token(id_token, _this.jwtKeys)) {
                        // validate nonce
                        if (_this.oidcSecurityValidation.Validate_id_token_nonce(decoded, _this.retrieve('authNonce'))) {
                            // validate iss
                            if (_this.oidcSecurityValidation.Validate_id_token_iss(decoded, _this._configuration.iss)) {
                                // validate aud
                                if (_this.oidcSecurityValidation.Validate_id_token_aud(decoded, _this._configuration.client_id)) {
                                    // valiadate at_hash and access_token
                                    if (_this.oidcSecurityValidation.Validate_id_token_at_hash(token, decoded.at_hash) || !token) {
                                        _this.store('authNonce', '');
                                        _this.store('authStateControl', '');
                                        authResponseIsValid = true;
                                        console.log('AuthorizedCallback state, nonce, iss, aud, signature validated, returning token');
                                    }
                                    else {
                                        console.log('AuthorizedCallback incorrect aud');
                                    }
                                }
                                else {
                                    console.log('AuthorizedCallback incorrect aud');
                                }
                            }
                            else {
                                console.log('AuthorizedCallback incorrect iss');
                            }
                        }
                        else {
                            console.log('AuthorizedCallback incorrect nonce');
                        }
                    }
                    else {
                        console.log('AuthorizedCallback incorrect Signature id_token');
                    }
                }
                else {
                    console.log('AuthorizedCallback incorrect state');
                }
            }
            if (authResponseIsValid) {
                _this.SetAuthorizationData(token, id_token);
                console.log(_this.retrieve('authorizationData'));
                // router navigate to Main page
                _this._router.navigate(['/']);
            }
            else {
                _this.ResetAuthorizationData();
                _this._router.navigate(['/Unauthorized']);
            }
        });
    };
    OidcSecurityService.prototype.Logoff = function () {
        // /connect/endsession?id_token_hint=...&post_logout_redirect_uri=https://myapp.com
        console.log('BEGIN Authorize, no auth data');
        var authorizationEndsessionUrl = this._configuration.logoutEndSession_url;
        var id_token_hint = this.retrieve('authorizationDataIdToken');
        var post_logout_redirect_uri = this._configuration.post_logout_redirect_uri;
        var url = authorizationEndsessionUrl + '?' +
            'id_token_hint=' + encodeURI(id_token_hint) + '&' +
            'post_logout_redirect_uri=' + encodeURI(post_logout_redirect_uri);
        this.ResetAuthorizationData();
        window.location.href = url;
    };
    OidcSecurityService.prototype.runGetSigningKeys = function () {
        var _this = this;
        this.getSigningKeys()
            .subscribe(function (jwtKeys) { return _this.jwtKeys = jwtKeys; }, function (error) { return _this.errorMessage = error; });
    };
    OidcSecurityService.prototype.getSigningKeys = function () {
        return this._http.get(this._configuration.jwks_url)
            .map(this.extractData)
            .catch(this.handleError);
    };
    OidcSecurityService.prototype.extractData = function (res) {
        var body = res.json();
        return body;
    };
    OidcSecurityService.prototype.handleError = function (error) {
        // In a real world app, you might use a remote logging infrastructure
        var errMsg;
        if (error instanceof http_1.Response) {
            var body = error.json() || '';
            var err = body.error || JSON.stringify(body);
            errMsg = error.status + " - " + (error.statusText || '') + " " + err;
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Rx_1.Observable.throw(errMsg);
    };
    OidcSecurityService.prototype.HandleError = function (error) {
        console.log(error);
        if (error.status == 403) {
            this._router.navigate(['/Forbidden']);
        }
        else if (error.status == 401) {
            this.ResetAuthorizationData();
            this._router.navigate(['/Unauthorized']);
        }
    };
    OidcSecurityService.prototype.retrieve = function (key) {
        var item = this.storage.getItem(key);
        if (item && item !== 'undefined') {
            return JSON.parse(this.storage.getItem(key));
        }
        return;
    };
    OidcSecurityService.prototype.store = function (key, value) {
        this.storage.setItem(key, JSON.stringify(value));
    };
    OidcSecurityService.prototype.setHeaders = function () {
        this.headers = new http_1.Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');
        var token = this.GetToken();
        if (token !== '') {
            this.headers.append('Authorization', 'Bearer ' + token);
        }
    };
    return OidcSecurityService;
}());
OidcSecurityService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http, auth_configuration_1.AuthConfiguration, router_1.Router])
], OidcSecurityService);
exports.OidcSecurityService = OidcSecurityService;
//# sourceMappingURL=oidc.security.service.js.map