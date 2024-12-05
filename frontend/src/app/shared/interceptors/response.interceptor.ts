import {
  HttpEvent,
  HttpEventType,
  HttpHandlerFn,
  HttpRequest,
} from '@angular/common/http';
import { Observable, map, catchError } from 'rxjs';
import { Result } from '../types/backend.interfaces';

export function responseInterceptor(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> {
  return next(req).pipe(
    map((event) => {
      if (event.type === HttpEventType.Response) {
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
      }
      return event;
    }),
    catchError((err) => {
      console.error('Error:', err);
      throw err;
    })
  );
}
