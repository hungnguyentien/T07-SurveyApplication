import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'Login', pathMatch: 'full', title: 'Đăng nhập' },
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
    redirectTo: 'Login',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
