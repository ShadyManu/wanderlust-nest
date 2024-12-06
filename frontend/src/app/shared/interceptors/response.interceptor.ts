import {
  HttpEvent,
  HttpEventType,
  HttpHandlerFn,
  HttpRequest,
} from '@angular/common/http';
import { Observable, map, catchError } from 'rxjs';
import { Result } from '../types/backend.interfaces';
import { ToastService } from '../components/toast/toast.component';
import { inject } from '@angular/core';

export function responseInterceptor(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> {
  const toastService = inject(ToastService);

  return next(req).pipe(
    map((event) => {
      if (event.type !== HttpEventType.Response) return event;

      if (!event.status.toString().startsWith('2')) {
        toastService.toastMessage.set('Error: ' + event.statusText);
      }

      const response = event.body as Result<any>;

      if (event.status === 200 && !response) {
        return event.clone({ body: true });
      }

      if (!!response.data) {
        return event.clone({ body: response.data });
      }

      if (!!response.error) {
        console.error(
          'Error:',
          response.error?.message,
          response.error?.innerException
        );
        throw new Error(response.error?.message);
      }

      return event.clone({ body: response });
    }),
    catchError((err) => {
      toastService.toastMessage.set(err.message);
      toastService.isToastOpen.set(true);
      throw err;
    })
  );
}
