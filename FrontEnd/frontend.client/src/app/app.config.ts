import { ApplicationConfig, importProvidersFrom } from "@angular/core";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, withComponentInputBinding, withRouterConfig } from '@angular/router';
import { routes } from './app.routes';
import { NgxSpinnerModule } from 'ngx-spinner';
import { provideToastr } from 'ngx-toastr';


export const appConfig: ApplicationConfig = {
  providers : [
    provideRouter(routes, withComponentInputBinding(), withRouterConfig({
      paramsInheritanceStrategy: 'always'
    })),
    importProvidersFrom(NgxSpinnerModule),
    provideToastr({
      positionClass: 'toast-bottom-right'
    }),
    provideAnimationsAsync(),
    importProvidersFrom(BrowserAnimationsModule),
  ]
}
