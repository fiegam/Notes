import { Component } from '@angular/core';

@Component({
    selector: 'notes-body',
    templateUrl: 'app/notes/templates/notes.html'
})
export class NotesComponent {
    notes = [{
        Title: 'Note1',
        Body: 'Body1'
    }, {
            Title: 'Note2',
            Body: 'Body2'
        }
    ];

    test = 'test string';
    testNote = [{
        Title: 'Note1',
        Body: 'Body1'
    }, {
            Title: 'Note2',
            Body: 'Body2'
        }
    ];

    constructor() {
        //this.notes = [{
        //    Title: 'Note1',
        //    Body: 'Body1'
        //}, {
        //        Title: 'Note2',
        //        Body: 'Body2'
        //    }
        //];

    }
}