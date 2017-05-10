import { Injectable, Output, EventEmitter } from "@angular/core";

@Injectable()
export class SessionService {
    @Output() unauthorized = new EventEmitter();
    @Output() authorized = new EventEmitter();

    public saveSessionData(sessionInfo: SessionInfo): void {
        localStorage.setItem('sessionData', JSON.stringify(sessionInfo));
        this.isLoggedIn = true;
        this.authorized.emit();
    }

    public deleteSessionData() {
        if (this.isLoggedIn) {
            localStorage.removeItem('sessionData');
            this.isLoggedIn = false;
            this.unauthorized.emit();
        }
    }

    public isLoggedIn: Boolean = false;

    public getSessionInfo(): SessionInfo {
        let data = localStorage.getItem('sessionData');
        if (data) {
            return JSON.parse(data);
        }
        else {
            return <SessionInfo>{};
        }
    }

    public getCurrentUser(): User {
        return this.getSessionInfo().user;
    }
    public setCurrentUser(user: User): void {
        let data = this.getSessionInfo();
        data.user = user;
        this.saveSessionData(data);
    }
}