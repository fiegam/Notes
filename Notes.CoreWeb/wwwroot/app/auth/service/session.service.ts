import {Injectable} from "@angular/core";

//@Injectable() specifies class is available to an injector for instantiation and an injector will display an error when trying to instantiate a class that is not marked as @Injectable()

@Injectable()

export class SessionService {
    

    public userLoggedin(user: User) {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.isLoggedIn = true;
    }

    public userLoggedout() {
        localStorage.removeItem('currentUser');
        this.isLoggedIn = false;
    }

    public isLoggedIn: Boolean = false;

    public getCurrentUser(): User {
        return JSON.parse(localStorage.getItem("currentUser"));
    }

}