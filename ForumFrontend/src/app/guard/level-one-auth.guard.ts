import { UtilityService } from './../services/utility.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserInfo} from '../interfaces/UserInfo'

@Injectable({
  providedIn: 'root'
})
export class LevelOneAuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private utilityService: UtilityService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(localStorage.getItem('userInfo') != null){
        const user : UserInfo = JSON.parse(localStorage.getItem('userInfo') ?? '' );
        if(user.token != null && user.level >= 1 ){
          return true;
        }

        if(user.level < 1){
          this.utilityService.openDialog('-8');
          return false;
        }
      }

      return this.router.parseUrl(`/login?redirect=${state.url}`);
  }

}
