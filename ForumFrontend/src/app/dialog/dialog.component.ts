import { TranslateService } from '@ngx-translate/core';
import { OverlayRef } from '@angular/cdk/overlay';
import { Component, OnInit } from '@angular/core';
import { faWindowClose } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {

  errorCode = '';
  windowClose = faWindowClose;

  constructor(
    private overlayRef: OverlayRef,
    private errCode: String) {
      this.errorCode = 'error.' + errCode.toString();
    }

  ngOnInit(): void {
  }

  close(){
    this.overlayRef.dispose();
  }
}
