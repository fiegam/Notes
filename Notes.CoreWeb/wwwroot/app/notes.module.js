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
var core_1 = require('@angular/core');
var platform_browser_1 = require('@angular/platform-browser');
var ng_bootstrap_1 = require('@ng-bootstrap/ng-bootstrap');
var header_component_1 = require('./header/header.component');
var notes_component_1 = require('./notes/notes.component');
var http_1 = require('@angular/http');
var NotesModule = (function () {
    function NotesModule() {
    }
    NotesModule = __decorate([
        core_1.NgModule({
            imports: [ng_bootstrap_1.NgbModule.forRoot(), platform_browser_1.BrowserModule, http_1.HttpModule],
            declarations: [notes_component_1.NotesComponent, header_component_1.HeaderComponent],
            bootstrap: [notes_component_1.NotesComponent, header_component_1.HeaderComponent],
        }), 
        __metadata('design:paramtypes', [])
    ], NotesModule);
    return NotesModule;
}());
exports.NotesModule = NotesModule;
//# sourceMappingURL=notes.module.js.map