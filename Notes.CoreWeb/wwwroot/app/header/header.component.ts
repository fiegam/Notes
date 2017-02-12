import { Component } from '@angular/core';

@Component({
    selector: 'notes-header',
    templateUrl: 'app/header/templates/header.html'
})
export class HeaderComponent {
    public User = { Name: 'test' };

}