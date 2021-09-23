import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './auth.interceptor';
import { LayoutModule } from './layout/layout.module';
import { DialogComponent } from './dialog/dialog.component';
import { OverlayModule } from '@angular/cdk/overlay';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import { ShareModule } from './share/share.module';


export function createTanslateLoder(http: HttpClient){
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    DialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LayoutModule,
    OverlayModule,
    FontAwesomeModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (createTanslateLoder),
        deps: [HttpClient]
      }
    }),
    ShareModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  entryComponents:[
    DialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
