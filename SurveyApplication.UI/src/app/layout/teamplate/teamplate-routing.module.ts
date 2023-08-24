import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TemplatePublicComponent } from './public-template/public-template.component';
import { LoginComponent } from '../share/login/login.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Trang',
    },
    children: [
      {
        path: '',
        component: TemplatePublicComponent,
        data: {
          title: 'Phiếu',
        },
        children: [
          {
            path: 'phieu',
            loadChildren: () =>
              import('../../modules/client/client.module').then(
                (x) => x.ClientModule
              ),
          },
        ],
      },
      {
        path: 'login',
        component: LoginComponent,
        data: {
          title: 'Đăng nhập',
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TeamplateRoutingModule {}
