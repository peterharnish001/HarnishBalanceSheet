import { ApplicationConfig, importProvidersFrom } from "@angular/core";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, withComponentInputBinding, withRouterConfig } from '@angular/router';
import { routes } from './app.routes';
import { NgxSpinnerModule } from 'ngx-spinner';
import { provideToastr } from 'ngx-toastr';
import { provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { MsalModule, MsalService, MsalGuard, MsalInterceptor, MsalRedirectComponent, MSAL_INSTANCE, MSAL_GUARD_CONFIG, MSAL_INTERCEPTOR_CONFIG } from '@azure/msal-angular';
import { loadingInterceptor } from './loading.interceptor';
import { PublicClientApplication, InteractionType } from '@azure/msal-browser';
import { environment } from "./environment";


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
    provideHttpClient(
      withInterceptors([loadingInterceptor]),
      withInterceptorsFromDi()
    ),
    MsalService,
    MsalGuard,
    { provide: MSAL_INSTANCE, useFactory: MSALInstanceFactory },
    { provide: MSAL_GUARD_CONFIG, useFactory: MSALGuardConfigFactory },
    { provide: MSAL_INTERCEPTOR_CONFIG, useFactory: MSALInterceptorConfigFactory },
    { provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true }
  ]
}

export function MSALInstanceFactory() {
  return new PublicClientApplication({
    auth: {
      clientId: 'FRONTEND_APP_ID',
      authority: 'https://login.microsoftonline.com/' + environment.tenantId,
      redirectUri: '/'
    }
  });
}

export function MSALGuardConfigFactory() {
  return { interactionType: InteractionType.Redirect };
}

export function MSALInterceptorConfigFactory() {
  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap: new Map([
      [environment.apiUrl, ['api://' + environment.apiAppId + '/.default']]
    ])
  };
}
