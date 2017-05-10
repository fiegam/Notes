import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { FormsModule } from '@angular/forms';

import { NotesComponent } from './notes.component';

import { NotesService } from './services/NotesService';

import { NotesRoutingModule } from './notes.routing.module';

@NgModule({
    imports: [
        CommonModule,
       // FormsModule,
        NotesRoutingModule
    ],
    declarations: [
        NotesComponent
    ],
    providers: [NotesService]
})
export class NotesModule { }