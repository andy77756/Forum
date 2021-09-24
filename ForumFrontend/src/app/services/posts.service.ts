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

  addPost(post: Post): Observable<ReturnData<Post>> {
    return this.httpClient.post<ReturnData<Post>>(`${environment.apiUrl}/api/Forum/Posts`, post);
  }

  getPost(key: string, pageIndex: Number, pageSize: Number): Observable<ReturnData<Post[]>> {
    return this.httpClient.get<ReturnData<Post[]>>(
      `${environment.apiUrl}/api/Forum/Posts?key=${key}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

  getReplies(postId: Number, pageIndex: Number, pageSize: Number): Observable<ReturnData<Replies>>{
    return this.httpClient.get<ReturnData<Replies>>(
      `${environment.apiUrl}/api/Forum/Reply?postId=${postId}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

  addReply(reply: PostReply): Observable<ReturnData<Reply>>{
    return this.httpClient.post<ReturnData<Reply>>(`${environment.apiUrl}/api/Forum/Reply`, reply);
  }
}
