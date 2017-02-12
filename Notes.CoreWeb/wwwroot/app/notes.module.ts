import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HeaderComponent } from './header/header.component';
import { NotesComponent } from './notes/notes.component';
@NgModule({
    imports: [BrowserModule],
    declarations: [NotesComponent, HeaderComponent],
    bootstrap: [NotesComponent, HeaderComponent]
})
export class NotesModule { }