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
var Observable_1 = require("rxjs/Observable");
var http_1 = require("@angular/http");
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var session_service_1 = require("../../auth/services/session.service");
//import 'rxjs/add/operator/catch';
require("rxjs/add/operator/map");
var NotesService = (function () {
    function NotesService(session, http) {
        this.session = session;
        this.http = http;
        this.notesUrl = 'http://localhost:5001/api/values'; // URL to web API
    }
    NotesService.prototype.GetNotes = function () {
        var jwt = localStorage.getItem('authorizationDataIdToken');
        var authHeader = new http_1.Headers();
        if (jwt) {
            authHeader.append('Authorization', 'Bearer ' + jwt);
        }
        return this.http.get(this.notesUrl, { headers: authHeader })
            .map(this.extractData);
    };
    NotesService.prototype.extractData = function (res) {
        var body = res.json();
        return body || {};
    };
    NotesService.prototype.handleError = function (error) {
        // In a real world app, we might use a remote logging infrastructure
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
        return Observable_1.Observable.throw(errMsg);
    };
    return NotesService;
}());
NotesService = __decorate([
    core_1.Injectable(),
    __param(0, core_2.Inject(session_service_1.SessionService)), __param(1, core_2.Inject(http_1.Http)),
    __metadata("design:paramtypes", [session_service_1.SessionService, http_1.Http])
], NotesService);
exports.NotesService = NotesService;
//# sourceMappingURL=NotesService.js.map