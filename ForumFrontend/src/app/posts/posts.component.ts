import { Metadata } from './../interfaces/Metadata';
import { map, catchError } from 'rxjs/operators';
import { PostsService } from 'src/app/services/posts.service';
import { Post } from './../interfaces/Post';
import { Component, Input, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {

  posts: Post[] = [];
  metadata: Metadata = {
    pageSize: 10,
    currentIndex: 0,
    length: 0
  };

  key = '';
  searchField = '';
  searchKey = new Map<string,string> ();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private postService: PostsService) { }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params =>{
      if(params.keys.length != 0){
        this.initState();
        this.metadata.currentIndex = parseInt(params.get('page')??'0');
        this.metadata.pageSize = parseInt(params.get('size')??'0');
        this.key = params.get('key')??'';
        this.searchField = params.get('searchField')??'';
        this.searchKey.set(this.searchField, this.key);
      }
      else{
        this.initState();
      }

      this.postService
      .getPost(this.searchKey.get('keyTopic')??'', this.searchKey.get('keyNickname')??'', this.metadata.currentIndex, this.metadata.pageSize)
      .pipe(
        catchError(error => {
          return throwError(error);
        }),
        map((result) => result),
      )
      .subscribe({
        next: (data) => {
          this.posts = data.returnData.posts;
          this.metadata = data.returnData.metaData;
        },
        error: (error: HttpErrorResponse) => {
          alert(error.error.body[0]);
        }
      });
    });
  }

  initState(){
    this.searchKey.clear();
    this.metadata = {
      pageSize: 10,
      currentIndex: 0,
      length: 0
    }
    this.key='';
    this.searchField = '';
  }

  doSearch($event: any){
    this.searchKey.set("keyTopic","");
    this.searchKey.set("keyNickname","");
    this.searchKey.set($event.searchField, $event.key);

    this.key = $event.key;

    this.router.navigate(
      ['/posts'],
      { queryParams:
        {
          page: 0,
          size: this.metadata.pageSize,
          key: $event.key,
          searchField: $event.searchField
        }
      }
    );
  }

  changePage(event: PageEvent){
    this.router.navigate(
      ['/posts'],
      { queryParams:
        {
          page: event.pageIndex,
          size: event.pageSize,
          key: this.key,
          searchField: this.searchField
        }
      }
    );
  }

  toPost(postid: number){
    this.router.navigate(
      ['/post', postid],
      {queryParams:
        {
          page: this.metadata.currentIndex,
          size: this.metadata.pageSize,
          key: this.key,
          searchField: this.searchField
        }
      }
    )
  }
}
