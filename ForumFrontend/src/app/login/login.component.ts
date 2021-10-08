import { UtilityService } from './../services/utility.service';
import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterStateSnapshot } from '@angular/router';
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
    pwd: '',
  };

  userRegister = {
    userName: '',
    nickname:'',
    pwd: '',
  };

  loginFlag = true;

  redirect = '/';

  constructor(private router: Router,
    private route: ActivatedRoute,
    private loginService: LoginService,
    private registerService: RegisterService,
    private translateService: TranslateService,
    private utilityService: UtilityService) { }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(queryParamMap => {
      this.redirect = queryParamMap.get('redirect')??'/';
    });
  }

  reverseFlag(){
    this.loginFlag = !this.loginFlag;
  }

  login(){
    this.loginService
      .login(this.userLogin)
      .pipe(
        catchError(error => {
          this.utilityService.openDialog("-9");
          return throwError(error);
        }),
        map((result) => result),
      )
      .subscribe({
        next: (data) => {
          console.log(data);
          if(data.statusCode != 1){
            this.utilityService.openDialog(data.statusCode.toString());
          }
          else{
            console.log(JSON.stringify(data.returnData));
            localStorage.setItem('userInfo',JSON.stringify(data.returnData));
            this.router.navigateByUrl(this.redirect);
          }

        },
        error: (error: HttpErrorResponse) => {
          alert(error.error.body[0]);
        }
    });
  }

  register(){
    this.registerService
      .register(this.userRegister)
      .pipe(
        catchError(error => {
          return throwError(error);
        })
      )
      .subscribe({
        next: (data) => {
          console.log(data);
          if(data.statusCode != 1){
            this.utilityService.openDialog(data.statusCode.toString());
          }
          else{
            localStorage.setItem('userInfo', JSON.stringify(data.returnData));
            this.router.navigateByUrl(this.redirect);
          }
        },
        error: (error: HttpErrorResponse) => {
          alert(error.error.body[0]);
        }
      });

  }

}
