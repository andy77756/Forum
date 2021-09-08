import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginInfo } from '../interfaces/LoginInfo'
import { UserInfo } from '../interfaces/UserInfo';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient) { }

  login(loginInfo: LoginInfo): Observable<UserInfo>{
    return this.httpClient.post<UserInfo>(`${environment.apiUrl}/api/Authorize/Login`, {user: loginInfo});
  }
}
