"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_interceptor_1 = require("./http.interceptor");
function httpFactory(xhrBackend, requestOptions, sessionService) {
    return new http_interceptor_1.InterceptedHttp(xhrBackend, requestOptions, sessionService);
}
exports.httpFactory = httpFactory;
//# sourceMappingURL=http.factory.js.map