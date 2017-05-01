"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
// http://openid.net/specs/openid-connect-implicit-1_0.html
// id_token
//// id_token C1: The Issuer Identifier for the OpenID Provider (which is typically obtained during Discovery) MUST exactly match the value of the iss (issuer) Claim.
//// id_token C2: The Client MUST validate that the aud (audience) Claim contains its client_id value registered at the Issuer identified by the iss (issuer) Claim as an audience.The ID Token MUST be rejected if the ID Token does not list the Client as a valid audience, or if it contains additional audiences not trusted by the Client.
// id_token C3: If the ID Token contains multiple audiences, the Client SHOULD verify that an azp Claim is present.
// id_token C4: If an azp (authorized party) Claim is present, the Client SHOULD verify that its client_id is the Claim Value.
//// id_token C5: The Client MUST validate the signature of the ID Token according to JWS [JWS] using the algorithm specified in the alg Header Parameter of the JOSE Header. The Client MUST use the keys provided by the Issuer.
//// id_token C6: The alg value SHOULD be RS256. Validation of tokens using other signing algorithms is described in the OpenID Connect Core 1.0 [OpenID.Core] specification.
//// id_token C7: The current time MUST be before the time represented by the exp Claim (possibly allowing for some small leeway to account for clock skew).
// id_token C8: The iat Claim can be used to reject tokens that were issued too far away from the current time, limiting the amount of time that nonces need to be stored to prevent attacks.The acceptable range is Client specific.
//// id_token C9: The value of the nonce Claim MUST be checked to verify that it is the same value as the one that was sent in the Authentication Request.The Client SHOULD check the nonce value for replay attacks.The precise method for detecting replay attacks is Client specific.
// id_token C10: If the acr Claim was requested, the Client SHOULD check that the asserted Claim Value is appropriate.The meaning and processing of acr Claim Values is out of scope for this document.
// id_token C11: When a max_age request is made, the Client SHOULD check the auth_time Claim value and request re- authentication if it determines too much time has elapsed since the last End- User authentication.
//// Access Token Validation
//// access_token C1: Hash the octets of the ASCII representation of the access_token with the hash algorithm specified in JWA[JWA] for the alg Header Parameter of the ID Token's JOSE Header. For instance, if the alg is RS256, the hash algorithm used is SHA-256.
//// access_token C2: Take the left- most half of the hash and base64url- encode it.
//// access_token C3: The value of at_hash in the ID Token MUST match the value produced in the previous step if at_hash is present in the ID Token.
var OidcSecurityValidation = (function () {
    function OidcSecurityValidation() {
    }
    // id_token C7: The current time MUST be before the time represented by the exp Claim (possibly allowing for some small leeway to account for clock skew).
    OidcSecurityValidation.prototype.IsTokenExpired = function (token, offsetSeconds) {
        var decoded;
        decoded = this.GetPayloadFromToken(token, false);
        var tokenExpirationDate = this.getTokenExpirationDate(decoded);
        offsetSeconds = offsetSeconds || 0;
        if (tokenExpirationDate == null) {
            return false;
        }
        // Token expired?
        return !(tokenExpirationDate.valueOf() > (new Date().valueOf() + (offsetSeconds * 1000)));
    };
    // id_token C9: The value of the nonce Claim MUST be checked to verify that it is the same value as the one that was sent in the Authentication Request.The Client SHOULD check the nonce value for replay attacks.The precise method for detecting replay attacks is Client specific.
    OidcSecurityValidation.prototype.Validate_id_token_nonce = function (dataIdToken, local_nonce) {
        if (dataIdToken.nonce !== local_nonce) {
            console.log('Validate_id_token_nonce failed');
            return false;
        }
        return true;
    };
    // id_token C1: The Issuer Identifier for the OpenID Provider (which is typically obtained during Discovery) MUST exactly match the value of the iss (issuer) Claim.
    OidcSecurityValidation.prototype.Validate_id_token_iss = function (dataIdToken, client_id) {
        if (dataIdToken.iss !== client_id) {
            console.log('Validate_id_token_iss failed');
            return false;
        }
        return true;
    };
    // id_token C2: The Client MUST validate that the aud (audience) Claim contains its client_id value registered at the Issuer identified by the iss (issuer) Claim as an audience.
    // The ID Token MUST be rejected if the ID Token does not list the Client as a valid audience, or if it contains additional audiences not trusted by the Client.
    OidcSecurityValidation.prototype.Validate_id_token_aud = function (dataIdToken, aud) {
        if (dataIdToken.aud !== aud) {
            console.log('Validate_id_token_aud failed');
            return false;
        }
        return true;
    };
    OidcSecurityValidation.prototype.ValidateStateFromHashCallback = function (state, local_state) {
        if (state !== local_state) {
            console.log('ValidateStateFromHashCallback failed');
            return false;
        }
        return true;
    };
    OidcSecurityValidation.prototype.GetPayloadFromToken = function (token, encode) {
        var data = {};
        if (typeof token !== 'undefined') {
            var encoded = token.split('.')[1];
            if (encode) {
                return encoded;
            }
            data = JSON.parse(this.urlBase64Decode(encoded));
        }
        return data;
    };
    OidcSecurityValidation.prototype.GetHeaderFromToken = function (token, encode) {
        var data = {};
        if (typeof token !== 'undefined') {
            var encoded = token.split('.')[0];
            if (encode) {
                return encoded;
            }
            data = JSON.parse(this.urlBase64Decode(encoded));
        }
        return data;
    };
    OidcSecurityValidation.prototype.GetSignatureFromToken = function (token, encode) {
        var data = {};
        if (typeof token !== 'undefined') {
            var encoded = token.split('.')[2];
            if (encode) {
                return encoded;
            }
            data = JSON.parse(this.urlBase64Decode(encoded));
        }
        return data;
    };
    // id_token C5: The Client MUST validate the signature of the ID Token according to JWS [JWS] using the algorithm specified in the alg Header Parameter of the JOSE Header. The Client MUST use the keys provided by the Issuer.
    // id_token C6: The alg value SHOULD be RS256. Validation of tokens using other signing algorithms is described in the OpenID Connect Core 1.0 [OpenID.Core] specification.
    OidcSecurityValidation.prototype.Validate_signature_id_token = function (id_token, jwtkeys) {
        if (!jwtkeys || !jwtkeys.keys) {
            return false;
        }
        var header_data = this.GetHeaderFromToken(id_token, false);
        var kid = header_data.kid;
        var alg = header_data.alg;
        if ('RS256' != alg) {
            console.log('Only RS256 supported');
            return false;
        }
        var isValid = false;
        for (var _i = 0, _a = jwtkeys.keys; _i < _a.length; _i++) {
            var key = _a[_i];
            if (key.kid === kid) {
                var publickey = KEYUTIL.getKey(key);
                isValid = KJUR.jws.JWS.verify(id_token, publickey, ['RS256']);
                return isValid;
            }
        }
        return isValid;
    };
    // Access Token Validation
    // access_token C1: Hash the octets of the ASCII representation of the access_token with the hash algorithm specified in JWA[JWA] for the alg Header Parameter of the ID Token's JOSE Header. For instance, if the alg is RS256, the hash algorithm used is SHA-256.
    // access_token C2: Take the left- most half of the hash and base64url- encode it.
    // access_token C3: The value of at_hash in the ID Token MUST match the value produced in the previous step if at_hash is present in the ID Token.
    OidcSecurityValidation.prototype.Validate_id_token_at_hash = function (access_token, at_hash) {
        var hash = KJUR.crypto.Util.hashString(access_token, 'sha256');
        var first128bits = hash.substr(0, hash.length / 2);
        var testdata = hextob64u(first128bits);
        if (testdata === at_hash) {
            return true; // isValid;
        }
        return false;
    };
    OidcSecurityValidation.prototype.getTokenExpirationDate = function (dataIdToken) {
        if (!dataIdToken.hasOwnProperty('exp')) {
            return null;
        }
        var date = new Date(0); // The 0 here is the key, which sets the date to the epoch
        date.setUTCSeconds(dataIdToken.exp);
        return date;
    };
    OidcSecurityValidation.prototype.urlBase64Decode = function (str) {
        var output = str.replace('-', '+').replace('_', '/');
        switch (output.length % 4) {
            case 0:
                break;
            case 2:
                output += '==';
                break;
            case 3:
                output += '=';
                break;
            default:
                throw 'Illegal base64url string!';
        }
        return window.atob(output);
    };
    return OidcSecurityValidation;
}());
OidcSecurityValidation = __decorate([
    core_1.Injectable()
], OidcSecurityValidation);
exports.OidcSecurityValidation = OidcSecurityValidation;
//# sourceMappingURL=oidc.security.validation.js.map