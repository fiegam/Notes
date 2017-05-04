import {XHRBackend, Http, RequestOptions} from "@angular/http";
import {InterceptedHttp} from "./http.interceptor";
import { SessionService } from "../services/session.service";

export function httpFactory(xhrBackend: XHRBackend, requestOptions: RequestOptions, sessionService: SessionService): Http {
    return new InterceptedHttp(xhrBackend, requestOptions, sessionService);
}