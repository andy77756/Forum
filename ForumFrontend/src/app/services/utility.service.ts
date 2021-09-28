import { TranslateService } from '@ngx-translate/core';
import { Overlay, OverlayConfig, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal, PortalInjector } from '@angular/cdk/portal';
import { Injectable, Injector } from '@angular/core';
import { DialogComponent } from '../dialog/dialog.component';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor(
    private translateService: TranslateService,
    private overlay : Overlay,
    private injector: Injector) { }

  openDialog(errcode: String){
    const strategy = this.overlay
      .position()
      .global()
      .centerHorizontally()
      .centerVertically();

    const config = new OverlayConfig({
      hasBackdrop: true,
      //backdropClass: 'cdk-overlay-dark-backdrop',
      positionStrategy: strategy
    });

    const overlayRef = this.overlay.create(config);

    const injector = new PortalInjector(
      this.injector,
      new WeakMap<any,any>([[OverlayRef, overlayRef],[String, errcode]])
    );

    overlayRef.attach(new ComponentPortal(DialogComponent, null, injector));
  }

  changeLang(lang: string){
    this.translateService.use(lang);
  }
}
