"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
//@Injectable() specifies class is available to an injector for instantiation and an injector will display an error when trying to instantiate a class that is not marked as @Injectable()
var SessionService = (function () {
    function SessionService() {
        this.isLoggedIn = false;
    }
    SessionService.prototype.userLoggedin = function (user) {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.isLoggedIn = true;
    };
    SessionService.prototype.userLoggedout = function () {
        localStorage.removeItem('currentUser');
        this.isLoggedIn = false;
    };
    SessionService.prototype.getCurrentUser = function () {
        return JSON.parse(localStorage.getItem("currentUser"));
    };
    return SessionService;
}());
SessionService = __decorate([
    core_1.Injectable()
], SessionService);
exports.SessionService = SessionService;
//# sourceMappingURL=session.service.js.map