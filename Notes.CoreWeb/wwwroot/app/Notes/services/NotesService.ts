import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers } from '@angular/http';
import {Injectable} from "@angular/core";
import {Inject} from "@angular/core";
import {SessionService} from "../../auth/services/session.service";

//import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()

export class NotesService {
    constructor( @Inject(SessionService) private session: SessionService, @Inject(Http) private http: Http) {
    }
    private notesUrl = 'http://localhost:5001/api/values';  // URL to web API

    public GetNotes(): Observable<Note[]> {

var jwt = localStorage['authorizationDataIdToken'];
  var authHeader = new Headers();
  if(jwt) {
    authHeader.append('Authorization', 'Bearer ' + JSON.parse(jwt));      
  }
        
        return this.http.get(this.notesUrl,{ headers: authHeader })
            .map(this.extractData);
    }

    private extractData(res: Response): Note[] {
        let body = res.json();
        return body || {};
    }
    private handleError(error: Response | any) {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}