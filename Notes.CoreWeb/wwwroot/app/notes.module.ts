import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from './header/header.component';
import { NotesComponent } from './notes/notes.component';

@NgModule({
    imports: [NgbModule.forRoot(),BrowserModule],
    declarations: [NotesComponent, HeaderComponent],
    bootstrap: [NotesComponent, HeaderComponent],
})
export class NotesModule { }