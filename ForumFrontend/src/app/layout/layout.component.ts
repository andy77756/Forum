import { UtilityService } from './../services/utility.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfo } from '../interfaces/UserInfo';
import { faCaretDown, faGlobe } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  caretDown = faCaretDown;
  global = faGlobe;

  userInfo : UserInfo = {
    token: '',
    userId: 0,
    userName: '',
    nickname: '',
    level: 0
  }
  constructor(private router: Router, private utilityService: UtilityService) { }

  ngOnInit(): void {
    if(localStorage.getItem('userInfo') != null){
      const user : UserInfo = JSON.parse(localStorage.getItem('userInfo')?? '');
      this.userInfo = user;
    }
  }

  logout(){
    /* TODO Call Web API */
    localStorage.clear();
    this.userInfo = {
      token: '',
      userId: 0,
      userName: '',
      nickname: '',
      level: 0
    };
    this.router.navigateByUrl('/');
  }

  changeLang(lang: string){
    this.utilityService.changeLang(lang);
  }

}
