import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminComponent } from './components/admin/admin.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'admin', component: AdminComponent },
  {
    path: '',
    data: {
      title: 'Default',
    },
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./layout/teamplate/teamplate.module').then(
            (x) => x.TeamplateModule
          ),
      },
    ],
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
