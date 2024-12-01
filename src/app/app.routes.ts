import { Routes } from '@angular/router';
import { HomeCardComponent } from './shared/components/home-card/home-card.component';
import { HomePage } from './features/home/home.page';

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
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/login/login.page').then((m) => m.LoginPage),
  },
  {
    path: 'currency-rates',
    loadComponent: () =>
      import('./features/currency-rates/currency-rates.page').then(
        (m) => m.CurrencyRatesPage
      ),
  },
  {
    path: 'notes',
    loadComponent: () =>
      import('./features/notes/notes.page').then((m) => m.NotesPage),
  },
  {
    path: 'create-note',
    loadComponent: () =>
      import('./shared/components/create-note/create-note.component').then(
        (m) => m.CreateNoteComponent
      ),
  },
  {
    path: 'edit-note/:id',
    loadComponent: () =>
      import('./shared/components/create-note/create-note.component').then(
        (m) => m.CreateNoteComponent
      ),
  },
  {
    path: 'planner',
    loadComponent: () =>
      import('./features/planner/planner.page').then((m) => m.PlannerPage),
  },
  {
    path: '**',
    component: HomePage,
  },
];
