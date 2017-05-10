"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
//import { ForbiddenComponent } from './forbidden/forbidden.component';
//import { NotesComponent } from './notes/notes.component';
//import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
//import { SecureFilesComponent } from './securefile/securefiles.component';
//import { DATA_RECORDS_ROUTES } from './dataeventrecords/dataeventrecords.routes';
var not_found_component_1 = require("./not-found.component");
var appRoutes = [
    { path: '', redirectTo: '/notes', pathMatch: 'full' },
    // { path: '', component: NotesComponent }
    //  { path: 'Forbidden', component: ForbiddenComponent },
    //  { path: 'Unauthorized', component: UnauthorizedComponent },
    { path: '**', component: not_found_component_1.PageNotFoundComponent }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routes.js.map