import { UtilityService } from './../../services/utility.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {FormArray, FormBuilder, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PostsService } from 'src/app/services/posts.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  form = this.formBuilder.group({
    topic: this.formBuilder.control('', [
      Validators.required,
      Validators.minLength(3)
    ]),
    content: this.formBuilder.control('', [
      Validators.required,
      Validators.minLength(10),
    ]),
  });

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private postService: PostsService,
    private utilityService: UtilityService) { }

  ngOnInit(): void {
  }

  send(){
    console.log(this.form.value);
    this.postService
      .addPost(this.form.value)
      .pipe(
        catchError(error => {
          return throwError(error);
        })
      )
      .subscribe({
        next: (data) => {
          console.log(data);
          console.log(data.statusCode);

          if(data.statusCode == -6 || data.statusCode == -7){
            this.router.navigateByUrl('/login');
          }
          else if(data.statusCode == 1){
            this.router.navigateByUrl('/');
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

}
