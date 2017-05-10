import {XHRBackend, Http, RequestOptions} from "@angular/http";
import {InterceptedHttp} from "./http.interceptor";
import { SessionService } from "../services/session.service";
import { Router} from '@angular/router';

export function httpFactory(xhrBackend: XHRBackend, requestOptions: RequestOptions, sessionService: SessionService, router: Router): Http {
    return new InterceptedHttp(xhrBackend, requestOptions, sessionService, router);
}