import { ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from "@angular/core";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, withComponentInputBinding, withRouterConfig } from '@angular/router';
import { routes } from './app.routes';
import { NgxSpinnerModule } from 'ngx-spinner';
import { provideToastr } from 'ngx-toastr';
import { provideHttpClient, withInterceptors, withInterceptorsFromDi, HTTP_INTERCEPTORS } from '@angular/common/http';
import { loadingInterceptor } from './loading.interceptor';
import { MsalService, MsalGuard, MsalInterceptor, MSAL_INSTANCE, MsalBroadcastService, MSAL_INTERCEPTOR_CONFIG } from '@azure/msal-angular';
import { MSALInstanceFactory, msalInterceptorConfigFactory } from './msal.config';

export const appConfig: ApplicationConfig = {
  providers : [
    provideRouter(routes, withComponentInputBinding(), withRouterConfig({
      paramsInheritanceStrategy: 'always'
    })),
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    importProvidersFrom(NgxSpinnerModule),
    provideToastr({
      positionClass: 'toast-bottom-right'
    }),
    provideAnimationsAsync(),
    importProvidersFrom(BrowserAnimationsModule),
    provideHttpClient(
      withInterceptors([loadingInterceptor]),
      withInterceptorsFromDi()
    ),
    { provide: MSAL_INSTANCE, useFactory: MSALInstanceFactory },
    MsalService,
    MsalGuard,
    MsalBroadcastService,
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: msalInterceptorConfigFactory
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    }
  ]
}
