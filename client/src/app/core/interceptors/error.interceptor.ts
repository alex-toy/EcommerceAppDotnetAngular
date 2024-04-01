import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router : Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 404) this.router.navigateByUrl('/not-found');
        if (error.status === 500) this.router.navigateByUrl('/server-error');
        if (error.status === 400) this.router.navigateByUrl('/not-found');
        if (error.status === 400) this.router.navigateByUrl('/not-found');
      })
    );
  }
}
