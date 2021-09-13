import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './interfaces/UserInfo';
import { environment } from 'src/environments/environment';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {


  constructor() {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let token = ''
    if(localStorage.getItem('userInfo') != null){
      const user : UserInfo = JSON.parse(localStorage.getItem('userInfo')?? '');
      token = user.token;
    }
    const newRequest = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
        'Access-Control-Allow-Origin': `${environment.apiUrl}`,
        'Access-Control-Allow-Methods': 'GET, PUT, POST, DELETE, OPTIONS',
      }
    })
    return next.handle(newRequest);
  }
}
