import { Routes } from '@angular/router';
import { HbsIndexComponent } from './hbs-index/hbs-index.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/index',
    pathMatch: 'full'
  },
  {
    path: "index",
    component: HbsIndexComponent
  }
]
