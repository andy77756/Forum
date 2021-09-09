import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterInfo } from '../interfaces/RegisterInfo'
import { UserInfo } from '../interfaces/UserInfo';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private httpClient: HttpClient) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': `${environment.apiUrl}`,
      'Access-Control-Allow-Methods': 'GET, PUT, POST, DELETE, OPTIONS'
    })
  };
  register(registerInfo: RegisterInfo): Observable<UserInfo>{

    return this.httpClient.post<UserInfo>(`${environment.apiUrl}/api/Authorize/Register`, registerInfo, this.httpOptions);
  }
}
