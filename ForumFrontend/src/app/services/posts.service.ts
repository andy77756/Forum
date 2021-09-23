import { Replies } from './../interfaces/Replies';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserInfo } from '../interfaces/UserInfo';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../interfaces/Post';
import { ReturnData } from '../interfaces/ReturnData';
import { Reply } from '../interfaces/Reply';
import { PostReply } from '../interfaces/PostReply';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  constructor(private httpClient: HttpClient) { }

  getTest(url: string): Observable<string>{
    return this.httpClient.get<string>(url);
  }

  addPost(post: Post): Observable<ReturnData<Post>> {
    return this.httpClient.post<ReturnData<Post>>(`${environment.apiUrl}/api/Forum/Posts`, post);
  }

  getPost(key: string, pageIndex: Number): Observable<ReturnData<Post[]>> {
    let params = new HttpParams();
    params.set('key', key);
    params.set('pageIndex', pageIndex.toString());
    return this.httpClient.get<ReturnData<Post[]>>(
      `${environment.apiUrl}/api/Forum/Posts`, { params: params});
  }

  getReplies(postId: Number, pageIndex: Number): Observable<ReturnData<Replies>>{
    console.log(`${postId} and ${pageIndex}`);

    let params = new HttpParams();
    params.set('postId', postId.toString());
    params.set('pageIndex', pageIndex.toString());
    return this.httpClient.get<ReturnData<Replies>>(
      `${environment.apiUrl}/api/Forum/Reply?postId=${postId.toString()}&pageIndex=${pageIndex.toString()}`);
  }

  addReply(reply: PostReply): Observable<ReturnData<Reply>>{
    return this.httpClient.post<ReturnData<Reply>>(`${environment.apiUrl}/api/Forum/Reply`, reply);
  }
}
