import { TranslateService } from '@ngx-translate/core';
import { OverlayRef } from '@angular/cdk/overlay';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {

  errorCode = '';

  constructor(
    private overlayRef: OverlayRef,
    private translaeSerive: TranslateService,
    private errCode: String) {
      this.errorCode = 'error.' + errCode.toString();
    }

  ngOnInit(): void {
  }

  close(){
    this.overlayRef.detach();
  }

}
