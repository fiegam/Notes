import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { NotesModule } from './notes.module';

const platform = platformBrowserDynamic();

platform.bootstrapModule(NotesModule);