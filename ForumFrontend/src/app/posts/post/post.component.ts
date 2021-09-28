import { Metadata } from './../../interfaces/Metadata';
import { ReturnData } from './../../interfaces/ReturnData';
import { UserInfo } from './../../interfaces/UserInfo';
import { UtilityService } from './../../services/utility.service';
import { catchError, map, switchMap } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { Reply } from './../../interfaces/Reply';
import { Post } from './../../interfaces/Post';
import { PostsService } from 'src/app/services/posts.service';
import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Content } from '@angular/compiler/src/render3/r3_ast';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons'
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  arrowLeft = faArrowLeft;

  post: Post = {
    postid: 0,
    topic: '',
    content: '',
    postUserName: '',
    postAt: '',
    replyUserName: '',
    replyAt: '',
    replyCount: 0
  }
  replies: Reply[] = [];

  metadata: Metadata = {
    length: 0,
    currentIndex: 1,
    pageSize: 10
  }

  reply = {
    userId: 0,
    postId: 0,
    content: ''
  };

  placeHolder = "請輸入回覆內容...";
  level = 0;
  disabledReply = false;

  queryParam = {
    key: '',
    searchField:'',
    page: 0,
    size: 10
  }

  constructor(
    private postService: PostsService,
    private route: ActivatedRoute,
    private router: Router,
    private utilityService :UtilityService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(paraMap => {
      const id = paraMap.get('id');
      this.postService.getReplies(parseInt(id??'0'), 1, 10).subscribe(returnData => {
        if (returnData.statusCode == 1) {
          this.post = returnData.returnData.post;
          this.replies = returnData.returnData.replies;
          this.metadata = returnData.returnData.metaData;
          this.metadata.currentIndex--;
          this.reply.postId = Number(returnData.returnData.post.postid);
        }
        else if(returnData.statusCode == -11){
          this.utilityService.openDialog(returnData.statusCode.toString());
          this.router.navigateByUrl('/');
        }
        else{
          this.utilityService.openDialog(returnData.statusCode.toString());
        }
      })
    })

    this.route.queryParamMap.subscribe(params => {
      this.queryParam = {
        key: params.get('key')??'',
        searchField: params.get('searchField')??'',
        page: parseInt(params.get('page')??'0'),
        size: parseInt(params.get('size')??'0')
      }
    })


    if(localStorage.getItem('userInfo') != null){
      const user : UserInfo = JSON.parse(localStorage.getItem('userInfo')?? '');
      this.reply.userId = Number(user.userId);
      this.level = Number(user.level);
    }
    if (this.level < 1){
        this.disabledReply = true;
    }
  }

  addReply(){

    this.postService
      .addReply(this.reply)
      .pipe(
        catchError(error => {
          return throwError(error);
        })
      )
      .subscribe({
        next: (data) => {
          if(data.statusCode == 1){
            this.replies.push(data.returnData);
            this.reply.content = '';
            this.placeHolder = "請輸入回覆內容...";
          }

          if(data.statusCode == -6 || data.statusCode == -7){
            this.utilityService.openDialog(data.statusCode.toString());
            this.router.navigateByUrl(`/login`);
          }
          else{
            this.utilityService.openDialog(data.statusCode.toString());
          }
        },
        error: (error: HttpErrorResponse) => {
          alert(error.error.body[0]);
        }
      });
  }

  back(){
    this.router.navigate(
      ['/posts'],
      {queryParams: this.queryParam}
    )
  }

  changePage(event: PageEvent){
    this.route.paramMap.subscribe(paraMap => {
      const id = paraMap.get('id');
      this.postService
      .getReplies(parseInt(id??'0'), event.pageIndex+1, event.pageSize)
      .subscribe(returnData => {
        if (returnData.statusCode == 1) {
          this.post = returnData.returnData.post;
          this.replies = returnData.returnData.replies;
          this.metadata = returnData.returnData.metaData;
          this.reply.postId = Number(returnData.returnData.post.postid);
        }
        else if(returnData.statusCode == -11){
          this.utilityService.openDialog(returnData.statusCode.toString());
          this.router.navigateByUrl('/');
        }
        else{
          this.utilityService.openDialog(returnData.statusCode.toString());
        }
      })
    })
  }

}
