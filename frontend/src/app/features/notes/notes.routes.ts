import { Route } from '@angular/router';
import { authGuard } from 'src/app/shared/guards/auth.guard';

export const NOTES_ROUTES: Route[] = [
  {
    path: '',
    loadComponent: () => import('./notes.page').then((m) => m.NotesPage),
    canActivate: [authGuard],
  },
  {
    path: 'create-note',
    loadComponent: () =>
      import('./create/create-note.component').then(
        (m) => m.CreateNoteComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'edit-note/:id',
    loadComponent: () =>
      import('./edit/edit-note.component').then((m) => m.EditNoteComponent),
    canActivate: [authGuard],
  },
];
