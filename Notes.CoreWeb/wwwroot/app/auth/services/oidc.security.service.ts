import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Rx';
import { Router } from '@angular/router';
import { AuthConfiguration } from '../auth.configuration';
import { OidcSecurityValidation } from './oidc.security.validation';
import { JwtKeys } from './jwtkeys';
import { SessionService } from './session.service';
import { TempDataStore } from './tempData.store';

@Injectable()
export class OidcSecurityService {

    public UserData: any;

    private _isAuthorized: boolean;
    private headers: Headers;
    private oidcSecurityValidation: OidcSecurityValidation;

    private errorMessage: string;
    private jwtKeys: JwtKeys;

    constructor(private _http: Http, private _configuration: AuthConfiguration, private _router: Router, private _sessionService: SessionService, private _tempData: TempDataStore) {

        this.oidcSecurityValidation = new OidcSecurityValidation();

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');

        this._isAuthorized = this._sessionService.getSessionInfo().isAuthorized;
    }

    public IsAuthorized(): boolean {
        if (this._isAuthorized) {
            if (this.oidcSecurityValidation.IsTokenExpired(this._sessionService.getSessionInfo().authorizationDataIdToken)) {
                console.log('IsAuthorized: isTokenExpired');
                this.ResetAuthorizationData();
                return false;
            }
            return true;
        }
        return false;
    }

    public GetToken(): any {
        return this._sessionService.getSessionInfo().authorizationData;
    }

    public ResetAuthorizationData() {
        this._sessionService.deleteSessionData();

        this._isAuthorized = false;
    }

    public SetAuthorizationData(token: any, id_token: any) {
        console.log(token);
        console.log(id_token);
        console.log('storing to storage, getting the roles');
        this._sessionService.saveSessionData({
            isAuthorized: true,
            authorizationDataIdToken: id_token,
            authorizationData: token,
            user: null
        });
        this._isAuthorized = true;

        this.getUserData()
            .subscribe(data => this.UserData = data,
            error => this.HandleError(error),
            () => {
                this._sessionService.setCurrentUser({ name: 'name', email: 'email' })
            });
    }

    public Authorize() {
        this.ResetAuthorizationData();

        console.log('BEGIN Authorize, no auth data');

        let authorizationUrl = this._configuration.server + '/connect/authorize';
        let client_id = this._configuration.client_id;
        let redirect_uri = this._configuration.redirect_url;
        let response_type = this._configuration.response_type;
        let scope = this._configuration.scope;
        let nonce = 'N' + Math.random() + '' + Date.now();
        let state = Date.now() + '' + Math.random();

        this._tempData.store('authStateControl', state);
        this._tempData.store('authNonce', nonce);
        console.log('AuthorizedController created. adding myautostate: ' + state);

        let url =
            authorizationUrl + '?' +
            'response_type=' + encodeURI(response_type) + '&' +
            'client_id=' + encodeURI(client_id) + '&' +
            'redirect_uri=' + encodeURI(redirect_uri) + '&' +
            'scope=' + encodeURI(scope) + '&' +
            'nonce=' + encodeURI(nonce) + '&' +
            'state=' + encodeURI(state);

        window.location.href = url;
    }

    public AuthorizedCallback() {
        console.log('BEGIN AuthorizedCallback, no auth data');
        this.ResetAuthorizationData();

        let hash = window.location.hash.substr(1);

        let result: any = hash.split('&').reduce(function (result: any, item: string) {
            let parts = item.split('=');
            result[parts[0]] = parts[1];
            return result;
        }, {});

        console.log(result);
        console.log('AuthorizedCallback created, begin token validation');

        let token = '';
        let id_token = '';
        let authResponseIsValid = false;

        this.getSigningKeys()
            .subscribe(jwtKeys => {
                this.jwtKeys = jwtKeys;

                if (!result.error) {

                    // validate state
                    if (this.oidcSecurityValidation.ValidateStateFromHashCallback(result.state, this._tempData.retrieve('authStateControl'))) {
                        token = result.access_token;
                        id_token = result.id_token;
                        let decoded: any;
                        let headerDecoded;
                        decoded = this.oidcSecurityValidation.GetPayloadFromToken(id_token, false);
                        headerDecoded = this.oidcSecurityValidation.GetHeaderFromToken(id_token, false);

                        // validate jwt signature
                        if (this.oidcSecurityValidation.Validate_signature_id_token(id_token, this.jwtKeys)) {
                            // validate nonce
                            if (this.oidcSecurityValidation.Validate_id_token_nonce(decoded, this._tempData.retrieve('authNonce'))) {
                                // validate iss
                                if (this.oidcSecurityValidation.Validate_id_token_iss(decoded, this._configuration.iss)) {
                                    // validate aud
                                    if (this.oidcSecurityValidation.Validate_id_token_aud(decoded, this._configuration.client_id)) {
                                        // valiadate at_hash and access_token
                                        if (this.oidcSecurityValidation.Validate_id_token_at_hash(token, decoded.at_hash) || !token) {
                                            this._tempData.store('authNonce', '');
                                            this._tempData.store('authStateControl', '');

                                            authResponseIsValid = true;
                                            console.log('AuthorizedCallback state, nonce, iss, aud, signature validated, returning token');
                                        } else {
                                            console.log('AuthorizedCallback incorrect aud');
                                        }
                                    } else {
                                        console.log('AuthorizedCallback incorrect aud');
                                    }
                                } else {
                                    console.log('AuthorizedCallback incorrect iss');
                                }
                            } else {
                                console.log('AuthorizedCallback incorrect nonce');
                            }
                        } else {
                            console.log('AuthorizedCallback incorrect Signature id_token');
                        }
                    } else {
                        console.log('AuthorizedCallback incorrect state');
                    }
                }

                if (authResponseIsValid) {
                    this.SetAuthorizationData(token, id_token);
                    console.log(token);

                    // router navigate to Main page
                    this._router.navigate(['/']);
                } else {
                    this.ResetAuthorizationData();
                    this._router.navigate(['/Unauthorized']);
                }
            });
    }

    public Logoff() {
        // /connect/endsession?id_token_hint=...&post_logout_redirect_uri=https://myapp.com
        console.log('BEGIN Authorize, no auth data');

        let authorizationEndsessionUrl = this._configuration.logoutEndSession_url;

        let id_token_hint = this._sessionService.getSessionInfo().authorizationDataIdToken;
        let post_logout_redirect_uri = this._configuration.post_logout_redirect_uri;

        let url =
            authorizationEndsessionUrl + '?' +
            'id_token_hint=' + encodeURI(id_token_hint) + '&' +
            'post_logout_redirect_uri=' + encodeURI(post_logout_redirect_uri);

        this.ResetAuthorizationData();

        window.location.href = url;
    }

    private runGetSigningKeys() {
        this.getSigningKeys()
            .subscribe(
            jwtKeys => this.jwtKeys = jwtKeys,
            error => this.errorMessage = <any>error);
    }

    private getSigningKeys(): Observable<JwtKeys> {
        return this._http.get(this._configuration.jwks_url)
            .map(this.extractData)
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body;
    }

    private handleError(error: Response | any) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }

    public HandleError(error: any) {
        console.log(error);
        if (error.status == 403) {
            this._router.navigate(['/Forbidden']);
        } else if (error.status == 401) {
            this.ResetAuthorizationData();
            this._router.navigate(['/Unauthorized']);
        }
    }

    private getUserData = (): Observable<string[]> => {
        return this._http.get(this._configuration.userinfo_url, {
            headers: this.headers,
            body: ''
        }).map(res => res.json());
    }
}