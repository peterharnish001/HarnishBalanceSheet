import { Routes } from '@angular/router';
import { HbsIndexComponent } from './hbs-index/hbs-index.component';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { MsalRedirectComponent } from '@azure/msal-angular';

export const routes: Routes = [
  {
    path: 'auth',
    component: MsalRedirectComponent
  },
  {
    path: '',
    redirectTo: '/index',
    pathMatch: 'full'
  },
  {
    path: "index",
    component: HbsIndexComponent
  },
  {
    path: 'create',
    component: CreateComponent
  },
  {
    path: 'details/:id',
    component: DetailsComponent
  },
  {
    path: 'edit/:id',
    component: EditComponent
  }
]
