import { Posts } from './../interfaces/Posts';
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

  getPost(keyTopic: string, keyNickname: string, pageIndex: number, pageSize: number): Observable<ReturnData<Posts>> {
    return this.httpClient.get<ReturnData<Posts>>(
      `${environment.apiUrl}/api/Forum/Posts?keyTopic=${keyTopic}&keyNickname=${keyNickname}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

  getReplies(postId: number, pageIndex: number, pageSize: number): Observable<ReturnData<Replies>>{
    return this.httpClient.get<ReturnData<Replies>>(
      `${environment.apiUrl}/api/Forum/Reply?postId=${postId}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }

  addReply(reply: PostReply): Observable<ReturnData<Reply>>{
    return this.httpClient.post<ReturnData<Reply>>(`${environment.apiUrl}/api/Forum/Reply`, reply);
  }
}
