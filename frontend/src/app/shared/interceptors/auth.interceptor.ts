import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

export function authInterceptor(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> {
  // Recupera il token JWT (ad esempio, da localStorage)
  const token = localStorage.getItem('token');

  // Se esiste un token, clonare la richiesta e aggiungere l'intestazione di autorizzazione
  if (token) {
    const cloned = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
    return next(cloned);
  }

  // Se non c'è un token, invia la richiesta originale
  return next(req);
}
