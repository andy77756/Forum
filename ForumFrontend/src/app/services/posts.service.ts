import { HttpClient, HttpParams } from '@angular/common/http';
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

  constructor(private httpClient: HttpClient) { }

  addPost(post: Post): Observable<ReturnData<Post>> {
    return this.httpClient.post<ReturnData<Post>>(`${environment.apiUrl}/api/Forum/Posts`, post);
  }

  getPost(key: string, pageIndex: Number): Observable<ReturnData<Post[]>> {
    let params = new HttpParams();
    params.set('key', key);
    params.set('pageIndex', pageIndex.toString());
    return this.httpClient.get<ReturnData<Post[]>>(
      `${environment.apiUrl}/api/Forum`, { params: params});
  }
}
