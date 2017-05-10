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
var SessionService = (function () {
    function SessionService() {
        this.unauthorized = new core_1.EventEmitter();
        this.authorized = new core_1.EventEmitter();
        this.isLoggedIn = false;
    }
    SessionService.prototype.saveSessionData = function (sessionInfo) {
        localStorage.setItem('sessionData', JSON.stringify(sessionInfo));
        this.isLoggedIn = true;
        this.authorized.emit();
    };
    SessionService.prototype.deleteSessionData = function () {
        if (this.isLoggedIn) {
            localStorage.removeItem('sessionData');
            this.isLoggedIn = false;
            this.unauthorized.emit();
        }
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
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], SessionService.prototype, "unauthorized", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], SessionService.prototype, "authorized", void 0);
SessionService = __decorate([
    core_1.Injectable()
], SessionService);
exports.SessionService = SessionService;
//# sourceMappingURL=session.service.js.map