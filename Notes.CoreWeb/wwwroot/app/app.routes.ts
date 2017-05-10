import { Routes, RouterModule } from '@angular/router';

//import { ForbiddenComponent } from './forbidden/forbidden.component';
//import { NotesComponent } from './notes/notes.component';
//import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
//import { SecureFilesComponent } from './securefile/securefiles.component';

//import { DATA_RECORDS_ROUTES } from './dataeventrecords/dataeventrecords.routes';
import { PageNotFoundComponent } from './not-found.component';


const appRoutes: Routes = [
    { path: '', redirectTo: '/notes', pathMatch: 'full' },
   // { path: '', component: NotesComponent }
  //  { path: 'Forbidden', component: ForbiddenComponent },
  //  { path: 'Unauthorized', component: UnauthorizedComponent },
    { path: '**', component: PageNotFoundComponent }
];

export const routing = RouterModule.forRoot(appRoutes);