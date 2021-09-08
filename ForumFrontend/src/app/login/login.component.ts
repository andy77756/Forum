import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user = {
    userName: '',
    pwd: '',
  };

  redirect = '/';

  constructor(private router: Router,
    private route: ActivatedRoute,
    private loginService: LoginService) { }

  ngOnInit(): void {
  }

  login(){
    console.log("login");
    this.loginService
      .login(this.user)
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

}
