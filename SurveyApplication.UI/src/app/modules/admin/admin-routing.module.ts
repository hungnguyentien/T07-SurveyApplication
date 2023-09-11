import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminUnitTypeComponent } from './admin-unit-type/admin-unit-type.component';
import { AdminObjectSurveyComponent } from './admin-object-survey/admin-object-survey.component';
import { QuestionComponent } from './admin-question/question/question.component';
import { AdminPeriodSurveyComponent } from './admin-period-survey/admin-period-survey.component';
import { AdminSendEmailComponent } from './admin-send-email/admin-send-email.component';
import { AdminTableSurveyComponent } from './admin-table-survey/admin-table-survey.component';
import { AuthGuardService } from '@app/helpers/auth-guard.service';

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
        title: 'Quản lý dashboard',
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
      },
      
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
