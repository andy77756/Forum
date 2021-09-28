import { UtilityService } from './services/utility.service';
import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'ForumFrontend';

  constructor(private translateService: TranslateService) { }

  ngOnInit(): void {
    this.translateService.use('zh-tw');
  }
}
