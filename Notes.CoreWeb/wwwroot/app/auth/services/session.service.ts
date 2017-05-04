import { Injectable } from "@angular/core";

@Injectable()
export class SessionService {
    public saveSessionData(sessionInfo: SessionInfo): void {
        localStorage.setItem('sessionData', JSON.stringify(sessionInfo));
        this.isLoggedIn = true;
    }

    public deleteSessionData() {
        localStorage.removeItem('sessionData');
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