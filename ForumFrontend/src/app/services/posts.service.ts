import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserInfo } from '../interfaces/UserInfo';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../interfaces/Post';
import { ReturnData } from '../interfaces/ReturnData';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  token = '';

  constructor(private httpClient: HttpClient) { }

  addPost(post: Post): Observable<ReturnData<Post>> {

    if(localStorage.getItem('userInfo') != null){
      const user : UserInfo = JSON.parse(localStorage.getItem('userInfo')?? '');
      this.token = user.token;
    }
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': `${environment.apiUrl}`,
        'Access-Control-Allow-Methods': 'GET, PUT, POST, DELETE, OPTIONS',
        'Authorization': `Bearer ${this.token}`
      })
    };
    return this.httpClient.post<ReturnData<Post>>(`${environment.apiUrl}/api/posts`, post, httpOptions);
  }
}
