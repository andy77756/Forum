import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { LoginService } from '../services/login.service';
import { RegisterService } from '../services/regiser.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userLogin = {
    userName: '',
    nickname:'',
    pwd: '',
  };

  loginFlag = true;

  redirect = '/';

  constructor(private router: Router,
    private route: ActivatedRoute,
    private loginService: LoginService,
    private registerService: RegisterService) { }

  ngOnInit(): void {
  }

  reverseFlag(){
    this.loginFlag = !this.loginFlag;
  }

  login(){
    console.log("login");
    this.loginService
      .login(this.userLogin)
      .pipe(
        catchError(error => {
          return throwError(error);
        }),
        map((result) => result),
      )
      .subscribe({
        next: (user) => {
          console.log(user);
          localStorage.setItem('token', user.token);
          this.router.navigateByUrl(this.redirect);
      },
      error: (error: HttpErrorResponse) => {
        alert(error.error.body[0]);
      }
    });
  }

  register(){
    this.registerService
      .register(this.userLogin)
      .pipe(
        catchError(error => {
          return throwError(error);
        })
      )
      .subscribe({
        next: (user) => {
          console.log(user);
          localStorage.setItem('token', user.token);
          this.router.navigateByUrl(this.redirect);
        },
        error: (error: HttpErrorResponse) => {
          alert(error.error.body[0]);
        }
      });

  }

}
