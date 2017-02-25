import { Component } from '@angular/core';
import {Inject} from "@angular/core";
import {NotesService} from "./services/NotesService";
import {SessionService} from "../auth/service/session.service";

@Component({
    selector: 'notes-body',
    templateUrl: 'app/notes/templates/notes.html',
    providers: [NotesService, SessionService]
})
export class NotesComponent {

    public notes: Note[] = [];

    test = 'test string';
    testNote = [{
        title: 'Note1',
        body: 'Body1'
    }, {
            title: 'Note2',
            body: 'Body2'
        }
    ];

     

    constructor( @Inject(NotesService) private notesService: NotesService) {
        notesService.GetNotes().subscribe(
            notes => {
                this.notes = notes;
            });

    }
}