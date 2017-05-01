import { Routes, RouterModule } from '@angular/router';

//import { ForbiddenComponent } from './forbidden/forbidden.component';
import { NotesComponent } from './Notes/notes.component';
//import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
//import { SecureFilesComponent } from './securefile/securefiles.component';

//import { DATA_RECORDS_ROUTES } from './dataeventrecords/dataeventrecords.routes';


const appRoutes: Routes = [
   // { path: '', component: AppComponent },
    { path: '', component: NotesComponent }
  //  { path: 'Forbidden', component: ForbiddenComponent },
  //  { path: 'Unauthorized', component: UnauthorizedComponent },
];

export const routing = RouterModule.forRoot(appRoutes);