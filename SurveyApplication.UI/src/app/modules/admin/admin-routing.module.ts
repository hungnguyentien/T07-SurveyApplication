import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminUnitTypeComponent } from './admin-unit-type/admin-unit-type.component';
import { AdminObjectSurveyComponent } from './admin-object-survey/admin-object-survey.component';
import { QuestionComponent } from './admin-question/question.component';
import { AdminPeriodSurveyComponent } from './admin-period-survey/admin-period-survey.component';
import { AdminSendEmailComponent } from './admin-send-email/admin-send-email.component';
import { AdminTableSurveyComponent } from './admin-table-survey/admin-table-survey.component';
import { AdminStatisticalComponent } from './admin-statistical/admin-statistical.component';
import { FieldOfActivityComponent } from './admin-categories/field-of-activity/field-of-activity.component';
import { ProvinceComponent } from './admin-categories/province/province.component';
import { DistrictComponent } from './admin-categories/district/district.component';
import { WardsComponent } from './admin-categories/wards/wards.component';
import { Error403Component } from '@app/layout/partials/error403/error403.component';
import { AdminRoleComponent } from './admin-role/admin-role.component';
import { AdminAccountComponent } from './admin-account/admin-account.component';
import { AuthGuard } from '@app/helpers';
import { CodeModule } from '@app/enums';
import { AdminModuleManagementComponent } from './admin-module-management/admin-module-management.component';
import { AdminIpaddressComponent } from './admin-ipaddress/admin-ipaddress.component';
import { AdminBackupComponent } from './admin-backup/admin-backup.component';
import { ForgotPassComponent } from '../../layout/teamplate/forgot-pass/forgot-pass.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Admin',
    },
    children: [
      {
        path: 'dashboard',
        component: AdminHomeComponent,
        title: 'Dashboard',
        canActivate: [AuthGuard],
        data: {
          role: CodeModule.Dashboard,
        },
      },
      {
        path: 'loai-hinh-don-vi',
        component: AdminUnitTypeComponent,
        title: 'Quản lý loại hình đơn vị',
        canActivate: [AuthGuard],
        data: {
          role: CodeModule.QlLhDv,
        },
      },
      {
        path: 'quan-ly-don-vi',
        component: AdminObjectSurveyComponent,
        title: 'Quản lý đơn vị',
        canActivate: [AuthGuard],
        data: {
          role: CodeModule.QlDv,
        },
      },
      {
        path: 'quan-ly-cau-hoi',
        component: QuestionComponent,
        title: 'Quản lý câu hỏi',
        canActivate: [AuthGuard],
        data: {
          role: CodeModule.QlCh,
        },
      },
      {
        path: 'dot-khao-sat',
        component: AdminPeriodSurveyComponent,
        title: 'Quản lý đợt khảo sát',
        canActivate: [AuthGuard],
        data: {
          role: CodeModule.QlDks,
        },
      },
      {
        path: 'bang-khao-sat',
        component: AdminTableSurveyComponent,
        title: 'Quản lý bảng khảo sát',
        canActivate: [AuthGuard],
        data: {
          role: CodeModule.QlKs,
        },
      },
      {
        path: 'gui-email',
        component: AdminSendEmailComponent,
        title: 'Quản lý gửi email',
        canActivate: [AuthGuard],
        data: {
          role: CodeModule.QlGm,
        },
      },
      {
        path: 'thong-ke-khao-sat',
        component: AdminStatisticalComponent,
        title: 'Danh sách thống kê khảo sát',
        canActivate: [AuthGuard],
        data: {
          role: CodeModule.TkKs,
        },
      },

      {
        path: 'linh-vuc-hoat-dong',
        component: FieldOfActivityComponent,
        title: 'Quản lý lĩnh vực hoạt động',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý lĩnh vực hoạt động',
        },
      },
      {
        path: 'tinh-thanh',
        component: ProvinceComponent,
        title: 'Quản lý tỉnh thành',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý tỉnh thành',
        },
      },
      {
        path: 'quan-huyen',
        component: DistrictComponent,
        title: 'Quản lý quận/huyện',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý quận/huyện',
        },
      },
      {
        path: 'xa-phuong',
        component: WardsComponent,
        title: 'Quản lý xã/phường',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý xã/phường',
        },
      },
      {
        path: 'nhom-quyen',
        component: AdminRoleComponent,
        title: 'Quản lý nhóm quyền',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý nhóm quyền',
        },
      },
      {
        path: 'tai-khoan',
        component: AdminAccountComponent,
        title: 'Quản lý tài khoản',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý tài khoản',
        },
      },
      {
        path: 'quan-ly-module',
        component: AdminModuleManagementComponent,
        title: 'Quản lý module',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý module',
        },
      },
      {
        path: 'quan-ly-ipaddress',
        component: AdminIpaddressComponent,
        title: 'Quản lý Ipaddress',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý Ipaddress',
        },
      },
      {
        path: 'quan-ly-sao-luu-khoi-phuc',
        component: AdminBackupComponent,
        title: 'Quản lý sao lưu phục hồi',
        canActivate: [AuthGuard],
        data: {
          title: 'Quản lý sao lưu phục hồi',
        },
      },
      {
        path: 'error-403',
        component: Error403Component,
        title: 'error-403',
        data: {
          title: 'error-403',
        },
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
