import { Routes } from '@angular/router';
import { HomePage } from './features/home/home.page';
import { authGuard } from './shared/guards/auth.guard';
import { WelcomePage } from './features/welcome/welcome.page';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    loadComponent: () =>
      import('./features/home/home.page').then((m) => m.HomePage),
    canActivate: [authGuard],
  },
  {
    path: 'welcome',
    loadComponent: () =>
      import('./features/welcome/welcome.page').then((m) => m.WelcomePage),
  },
  {
    path: 'currency-rates',
    loadComponent: () =>
      import('./features/currency-rates/currency-rates.page').then(
        (m) => m.CurrencyRatesPage
      ),
    canActivate: [authGuard],
  },
  {
    path: 'notes',
    loadComponent: () =>
      import('./features/notes/notes.page').then((m) => m.NotesPage),
    canActivate: [authGuard],
  },
  {
    path: 'create-note',
    loadComponent: () =>
      import('./shared/components/create-note/create-note.component').then(
        (m) => m.CreateNoteComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'edit-note/:id',
    loadComponent: () =>
      import('./shared/components/create-note/create-note.component').then(
        (m) => m.CreateNoteComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'planner',
    loadComponent: () =>
      import('./features/planner/planner.page').then((m) => m.PlannerPage),
    canActivate: [authGuard],
  },
  {
    path: '**',
    component: HomePage,
  },
];
