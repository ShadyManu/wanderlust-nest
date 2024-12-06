import { Routes } from '@angular/router';
import { HomePage } from './features/home/home.page';
import { authGuard } from './shared/guards/auth.guard';
import { CreateNoteComponent } from './features/notes/create/create-note.component';
import { NOTES_ROUTES } from './features/notes/notes.routes';

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
    loadChildren: () => NOTES_ROUTES,
    canActivate: [authGuard],
    // children: [
    //   {
    //     path: 'create-note',
    //     loadComponent: () =>
    //       import('./features/notes/create/create-note.component').then(
    //         (m) => m.CreateNoteComponent
    //       ),
    //   },
    // ],
  },
  {
    path: 'create-note',
    loadComponent: () =>
      import('./features/notes/create/create-note.component').then(
        (m) => m.CreateNoteComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'edit-note/:id',
    loadComponent: () =>
      import('./features/notes/create/create-note.component').then(
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
