import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminUnitTypeComponent } from './admin-unit-type/admin-unit-type.component';
import { AdminObjectSurveyComponent } from './admin-object-survey/admin-object-survey.component';
import { QuestionComponent } from './admin-question/question/question.component';
import { AdminPeriodSurveyComponent } from './admin-period-survey/admin-period-survey.component';
import { AdminSendEmailComponent } from './admin-send-email/admin-send-email.component';
import { AdminTableSurveyComponent } from './admin-table-survey/admin-table-survey.component';
import { AdminStatisticalComponent } from './admin-statistical/admin-statistical.component';
import { AuthGuardService } from '@app/helpers/auth-guard.service';
import { FieldOfActivityComponent } from './admin-categories/field-of-activity/field-of-activity.component';
import { ProvinceComponent } from './admin-categories/province/province.component';
import { DistrictComponent } from './admin-categories/district/district.component';
import { WardsComponent } from './admin-categories/wards/wards.component';
import { Error403Component } from '@app/layout/partials/error403/error403.component';
import { AdminRoleComponent } from './admin-role/admin-role.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Admin',   
    },
    children: [
      {
        path: 'dashboard',
        component: AdminHomeComponent, canActivate: [AuthGuardService],
        title: 'Dashboard',
        data: {
          title: 'Dashboard',
        },
      },
      {
        path: 'loai-hinh-don-vi',
        component: AdminUnitTypeComponent, canActivate: [AuthGuardService],
        title: 'Quản lý loại hình đơn vị',
        data: {
          title: 'loai-hinh-don-vi',
        },
      },
      {
        path: 'quan-ly-don-vi',
        component: AdminObjectSurveyComponent, canActivate: [AuthGuardService],
        title: 'Quản lý đơn vị',
        data: {
          title: 'quan-ly-don-vi',
        },
      },
      {
        path: 'quan-ly-cau-hoi',
        component: QuestionComponent, canActivate: [AuthGuardService],
        title: 'Quản lý câu hỏi',
        data: {
          title: 'Quản lý câu hỏi',
        },
      },
      {
        path: 'dot-khao-sat',
        component: AdminPeriodSurveyComponent, canActivate: [AuthGuardService],
        title: 'Quản lý đợt khảo sát',
        data: {
          title: 'dot-khao-sat',
        },
      },
      {
        path: 'bang-khao-sat',
        component:AdminTableSurveyComponent, canActivate: [AuthGuardService],
        title: 'Quản lý bảng khảo sát',
        data: {
          title: 'bang-khao-sat',
        },
      },
      {
        path: 'gui-email',
        component:AdminSendEmailComponent, canActivate: [AuthGuardService],
        title: 'Quản lý gửi email',
        data: {
          title: 'gui-email',
        },
      }, {
        path: 'thong-ke-khao-sat',
        component:AdminStatisticalComponent,
        title: 'Danh sách thống kê khảo sát',
        data: {
          title: 'thong-ke-khao-sat',
        },
      },

      {
        path: 'linh-vuc-hoat-dong',
        component: FieldOfActivityComponent, canActivate: [AuthGuardService],
        title: 'Quản lý lĩnh vực hoạt động',
        data: {
          title: 'Quản lý lĩnh vực hoạt động',
        },
      },
      {
        path: 'tinh-thanh',
        component: ProvinceComponent, canActivate: [AuthGuardService],
        title: 'Quản lý tỉnh thành',
        data: {
          title: 'Quản lý tỉnh thành',
        },
      },
      {
        path: 'quan-huyen',
        component:DistrictComponent, canActivate: [AuthGuardService],
        title: 'Quản lý quận/huyện',
        data: {
          title: 'Quản lý quận/huyện',
        },
      },
      {
        path: 'xa-phuong',
        component:WardsComponent, canActivate: [AuthGuardService],
        title: 'Quản lý xã/phường',
        data: {
          title: 'Quản lý xã/phường',
        },
      },
      {
        path: 'nhom-quyen',
        component:AdminRoleComponent, canActivate: [AuthGuardService],
        title: 'Quản lý nhóm quyền',
        data: {
          title: 'Quản lý nhóm quyền',
        },
      },
      {
        path: 'error-403',
        component:Error403Component,
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
