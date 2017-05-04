import { Injectable, Inject } from "@angular/core";
import { ConnectionBackend, RequestOptions, Request, RequestOptionsArgs, Response, Http, Headers } from "@angular/http";
import { Observable } from "rxjs/Rx";
import { SessionService } from "../services/session.service";
//import {environment} from "../environments/environment";

@Injectable()
export class InterceptedHttp extends Http {
    private environmentApiUrl: string = 'http://localhost:5001';

    constructor(backend: ConnectionBackend, defaultOptions: RequestOptions, @Inject(SessionService) private _sessionService: SessionService) {
        super(backend, defaultOptions);
    }

    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        return super.request(url, options);
    }

    get(url: string, options?: RequestOptionsArgs): Observable<Response> {
        url = this.updateUrl(url);
        return super.get(url, this.getRequestOptionArgs(options));
    }

    post(url: string, body: string, options?: RequestOptionsArgs): Observable<Response> {
        url = this.updateUrl(url);
        return super.post(url, body, this.getRequestOptionArgs(options));
    }

    put(url: string, body: string, options?: RequestOptionsArgs): Observable<Response> {
        url = this.updateUrl(url);
        return super.put(url, body, this.getRequestOptionArgs(options));
    }

    delete(url: string, options?: RequestOptionsArgs): Observable<Response> {
        url = this.updateUrl(url);
        return super.delete(url, this.getRequestOptionArgs(options));
    }

    private updateUrl(req: string) {
        if (req.startsWith("http")) {
            return req;
        }
        return this.environmentApiUrl + req;
    }

    private getRequestOptionArgs(options?: RequestOptionsArgs): RequestOptionsArgs {
        if (options == null) {
            options = new RequestOptions();
        }
        if (options.headers == null) {
            options.headers = new Headers();
        }
        options.headers.append('Content-Type', 'application/json');
        options.headers.append('Accept', 'application/json');

        if (this._sessionService.isLoggedIn) {
            var sessionInfo = this._sessionService.getSessionInfo();
            if (sessionInfo) {
                options.headers.append('Authorization', 'Bearer ' + sessionInfo.authorizationDataIdToken);
            }
        }
        return options;
    }
}