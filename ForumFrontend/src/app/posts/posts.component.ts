import { map, catchError } from 'rxjs/operators';
import { PostsService } from 'src/app/services/posts.service';
import { Post } from './../interfaces/Post';
import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {

  posts: Post[] = [];

  test = '';

  constructor(
    private postService: PostsService) { }

  ngOnInit(): void {
    this.postService
      .getPost('', 1)
      .pipe(
        catchError(error => {
          return throwError(error);
        }),
        map((result) => result),
      )
      .subscribe({
        next: (data) => {
          this.posts = data.returnData
        },
        error: (error: HttpErrorResponse) => {
          alert(error.error.body[0]);
        }
      });
  }

  doSearch($event: string){
    this.postService
      .getPost($event, 1)
      .pipe(
        catchError(error => {
          return throwError(error);
        }),
        map((result) => result),
      )
      .subscribe({
        next: (data) => {
          this.posts = data.returnData
        },
        error: (error: HttpErrorResponse) => {
          alert(error.error.body[0]);
        }
      });
  }
}
