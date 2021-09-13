import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterInfo } from '../interfaces/RegisterInfo'
import { UserInfo } from '../interfaces/UserInfo';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ReturnData } from '../interfaces/ReturnData'

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private httpClient: HttpClient) { }

  register(registerInfo: RegisterInfo): Observable<ReturnData<UserInfo>>{

    return this.httpClient.post<ReturnData<UserInfo>>(`${environment.apiUrl}/api/Authorize/Register`, registerInfo);
  }
}
