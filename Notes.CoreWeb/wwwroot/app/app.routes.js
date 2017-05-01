"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
//import { ForbiddenComponent } from './forbidden/forbidden.component';
var notes_component_1 = require("./Notes/notes.component");
//import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
//import { SecureFilesComponent } from './securefile/securefiles.component';
//import { DATA_RECORDS_ROUTES } from './dataeventrecords/dataeventrecords.routes';
var appRoutes = [
    // { path: '', component: AppComponent },
    { path: '', component: notes_component_1.NotesComponent }
    //  { path: 'Forbidden', component: ForbiddenComponent },
    //  { path: 'Unauthorized', component: UnauthorizedComponent },
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routes.js.map