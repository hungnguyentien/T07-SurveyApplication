import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TemplatePublicComponent } from './public-template/public-template.component';
import { AdminTempleteComponent } from './admin-templete/admin-templete.component';
import { LoginComponent } from '../share/login/login.component';
import { AuthGuard } from '@app/helpers';
import { CodeModule } from '@app/enums';
import { ForgotPassComponent } from './forgot-pass/forgot-pass.component';

const routes: Routes = [
  {
    path: 'forgot-password',
    component: ForgotPassComponent,
    title: 'Tạo mật khẩu mới',
    data: {
      title: 'Tạo mật khẩu mới',
    },
  },
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
        path: '',
        component: AdminTempleteComponent,
        data: {
          title: 'Admin',
        },
        children: [
          {
            path: 'admin',
            loadChildren: () =>
              import('../../modules/admin/admin.module').then(
                (x) => x.AdminModule
              ),
            canActivate: [AuthGuard],
            data: {
              role: CodeModule.Admin,
            },
          },
        ],
      },
      {
        path: 'Login',
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
