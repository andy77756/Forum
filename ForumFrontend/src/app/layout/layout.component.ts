import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfo } from '../interfaces/UserInfo';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  token = '';
  constructor(private router: Router) { }

  ngOnInit(): void {
    if(localStorage.getItem('userInfo') != null){
      const user : UserInfo = JSON.parse(localStorage.getItem('userInfo')?? '');
      this.token = user.token;
    }
  }

  logout(){
    /* TODO Call Web API */
    localStorage.clear();
    this.token = '';
    this.router.navigateByUrl('/');
  }

}
