"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
//import {environment} from "../environments/environment";
var InterceptedHttp = (function (_super) {
    __extends(InterceptedHttp, _super);
    function InterceptedHttp(backend, defaultOptions) {
        var _this = _super.call(this, backend, defaultOptions) || this;
        _this.environmentApiUrl = 'http://localhost:5001';
        return _this;
    }
    InterceptedHttp.prototype.request = function (url, options) {
        return _super.prototype.request.call(this, url, options);
    };
    InterceptedHttp.prototype.get = function (url, options) {
        url = this.updateUrl(url);
        return _super.prototype.get.call(this, url, this.getRequestOptionArgs(options));
    };
    InterceptedHttp.prototype.post = function (url, body, options) {
        url = this.updateUrl(url);
        return _super.prototype.post.call(this, url, body, this.getRequestOptionArgs(options));
    };
    InterceptedHttp.prototype.put = function (url, body, options) {
        url = this.updateUrl(url);
        return _super.prototype.put.call(this, url, body, this.getRequestOptionArgs(options));
    };
    InterceptedHttp.prototype.delete = function (url, options) {
        url = this.updateUrl(url);
        return _super.prototype.delete.call(this, url, this.getRequestOptionArgs(options));
    };
    InterceptedHttp.prototype.updateUrl = function (req) {
        return this.environmentApiUrl + req;
    };
    InterceptedHttp.prototype.getRequestOptionArgs = function (options) {
        if (options == null) {
            options = new http_1.RequestOptions();
        }
        if (options.headers == null) {
            options.headers = new http_1.Headers();
        }
        options.headers.append('Content-Type', 'application/json');
        var jwt = localStorage['authorizationDataIdToken'];
        if (jwt) {
            options.headers.append('Authorization', 'Bearer ' + JSON.parse(jwt));
        }
        return options;
    };
    return InterceptedHttp;
}(http_1.Http));
InterceptedHttp = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.ConnectionBackend, http_1.RequestOptions])
], InterceptedHttp);
exports.InterceptedHttp = InterceptedHttp;
//# sourceMappingURL=httpInterceptor.js.map