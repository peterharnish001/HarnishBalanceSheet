import { Routes } from '@angular/router';
import { HbsIndexComponent } from './hbs-index/hbs-index.component';
import { CreateComponent } from './create/create.component'

export const routes: Routes = [
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
  }
]
