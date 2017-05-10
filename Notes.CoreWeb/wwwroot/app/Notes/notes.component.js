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
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var NotesService_1 = require("./services/NotesService");
var NotesComponent = (function () {
    function NotesComponent(_notesService) {
        this._notesService = _notesService;
        this.test = 'test string';
        this.testNote = [{
                title: 'Note1',
                body: 'Body1'
            }, {
                title: 'Note2',
                body: 'Body2'
            }
        ];
        this.notes = this.testNote;
    }
    NotesComponent.prototype.reload = function () {
        var _this = this;
        this._notesService.GetNotes().subscribe(function (notes) {
            _this.notes = notes;
        });
    };
    NotesComponent.prototype.ngOnInit = function () {
    };
    return NotesComponent;
}());
NotesComponent = __decorate([
    core_1.Component({
        //selector: 'notes-body',
        templateUrl: 'app/notes/templates/notes.html',
        providers: [NotesService_1.NotesService]
    }),
    __param(0, core_2.Inject(NotesService_1.NotesService)),
    __metadata("design:paramtypes", [NotesService_1.NotesService])
], NotesComponent);
exports.NotesComponent = NotesComponent;
//# sourceMappingURL=notes.component.js.map