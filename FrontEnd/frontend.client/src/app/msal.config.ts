import { Configuration, BrowserCacheLocation, LogLevel, PublicClientApplication, InteractionType } from '@azure/msal-browser';
import { MsalGuardConfiguration, MsalInterceptorConfiguration } from '@azure/msal-angular';
import { environment } from './environment';

// MSAL configuration object
export const msalConfig: Configuration = {
  auth: {
    clientId: environment.clientId,
    authority: environment.authority,
    redirectUri: environment.redirectUri,
    postLogoutRedirectUri: environment.redirectUri
  },
  cache: {
    cacheLocation: BrowserCacheLocation.SessionStorage
  },
  system: {
    loggerOptions: {
      loggerCallback: (level, message, containsPii) => {
        if (containsPii) return;
        console.log(`MSAL [${level}]: ${message}`);
      },
      piiLoggingEnabled: false,
      logLevel: LogLevel.Verbose
    }
  }
};


export const msalGuardConfigFactory: () => MsalGuardConfiguration = () => {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: ['user.read']
    }
  };
};


export const msalInterceptorConfigFactory: () => MsalInterceptorConfiguration = () => {
  const protectedResourceMap = new Map<string, Array<string>>();
  protectedResourceMap.set('https://graph.microsoft.com/v1.0/me', ['user.read']);
  protectedResourceMap.set(environment.apiUrl + '*', environment.protectedResourceValue);


  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap: protectedResourceMap
  };
};


export function MSALInstanceFactory(): PublicClientApplication {
  return new PublicClientApplication(msalConfig);
}

