import { ApplicationConfig, importProvidersFrom } from "@angular/core";
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
    })
  ]
}
