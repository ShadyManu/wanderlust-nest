import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { LoginResponse } from '../types/backend.interfaces';
// import { Preferences } from '@capacitor/preferences';

export const authGuard: CanActivateFn = (route, state) => {
  const token = localStorage.getItem('token');
  const refreshToken = localStorage.getItem('refreshToken');
  const router = inject(Router);

  // const setName = async () => {
  //   await Preferences.set({
  //     key: 'name',
  //     value: 'Max',
  //   });
  // };

  // const checkStorageKey = async (key: string) => {
  //   const { value } = await Preferences.get({ key: key });
  // };

  // const token = checkStorageKey('token');

  if (!token || !refreshToken) {
    router.navigate(['/welcome']);
    return of(false);
  }

  const authService = inject(AuthService);

  return authService.refreshToken(refreshToken).pipe(
    map((result) => {
      const value = result as LoginResponse;
      if (!!value && !!value.accessToken && !!value.refreshToken) {
        localStorage.setItem('token', value.accessToken);
        localStorage.setItem('refreshToken', value.refreshToken);
        return true;
      }
      return false;
    }),
    catchError(() => {
      router.navigate(['/welcome']);
      return of(false);
    })
  );
};
