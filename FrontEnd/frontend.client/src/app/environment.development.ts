import { BrowserAuthOptions } from "@azure/msal-browser";

export const environment = {
  apiUrl: 'http://localhost:5001/api/',
  clientId: '16d21f89-ffc5-4fc0-9e41-914845978cce',
  authority: 'https://login.microsoftonline.com/consumers',
  redirectUri: 'http://localhost:4200',
  protectedResourceValue:  ['api://2568cfc9-32a1-423a-a7fe-3706039f2a0d/access_as_user']
};
