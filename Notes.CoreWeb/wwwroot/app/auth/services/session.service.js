"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SessionService = (function () {
    function SessionService() {
        this.isLoggedIn = false;
    }
    SessionService.prototype.saveSessionData = function (sessionInfo) {
        localStorage.setItem('sessionData', JSON.stringify(sessionInfo));
        this.isLoggedIn = true;
    };
    SessionService.prototype.deleteSessionData = function () {
        localStorage.removeItem('sessionData');
    };
    SessionService.prototype.getSessionInfo = function () {
        var data = localStorage.getItem('sessionData');
        if (data) {
            return JSON.parse(data);
        }
        else {
            return {};
        }
    };
    SessionService.prototype.getCurrentUser = function () {
        return this.getSessionInfo().user;
    };
    SessionService.prototype.setCurrentUser = function (user) {
        var data = this.getSessionInfo();
        data.user = user;
        this.saveSessionData(data);
    };
    return SessionService;
}());
SessionService = __decorate([
    core_1.Injectable()
], SessionService);
exports.SessionService = SessionService;
//# sourceMappingURL=session.service.js.map