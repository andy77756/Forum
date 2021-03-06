import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserInfo} from '../interfaces/UserInfo'
import { UtilityService } from '../services/utility.service';

@Injectable({
  providedIn: 'root'
})
export class LevelTwoAuthGuard implements CanActivate {

constructor(
  private router: Router,
  private utilityService: UtilityService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(localStorage.getItem('userInfo') != null){
        const user : UserInfo = JSON.parse(localStorage.getItem('userInfo') ?? '' );
        if(user.token != null && user.level >= 2 ){
          return true;
        }

        if(user.level < 2){
          this.utilityService.openDialog('-8');
          return this.router.navigateByUrl('/');
        }
      }

      return this.router.parseUrl(`/login?redirect=${state.url}`);
  }

}
