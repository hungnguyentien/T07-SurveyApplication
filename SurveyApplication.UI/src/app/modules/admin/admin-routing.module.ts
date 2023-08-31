import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminUnitTypeComponent } from './admin-unit-type/admin-unit-type.component';
import { AdminObjectSurveyComponent } from './admin-object-survey/admin-object-survey.component';
import { QuestionComponent } from './admin-question/question/question.component';
import { AdminPeriodSurveyComponent } from './admin-period-survey/admin-period-survey.component';
import { Thongke } from '@app/models';
import { AdminSendEmailComponent } from './admin-send-email/admin-send-email.component';
import { AdminTableSurveyComponent } from './admin-table-survey/admin-table-survey.component';
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Admin',   
    },
    children: [
      {
        path: 'home',
        component: AdminHomeComponent,
        data: {
          title: 'Admin-home',
        },
      },
      {
        path: 'loai-hinh-don-vi',
        component: AdminUnitTypeComponent,
        data: {
          title: 'loai-hinh-don-vi',
        },
      },
      {
        path: 'quan-ly-don-vi',
        component: AdminObjectSurveyComponent,
        data: {
          title: 'quan-ly-don-vi',
        },
      },
      {
        path: 'quan-ly-cau-hoi',
        component: QuestionComponent,
        title: 'Quản lý câu hỏi',
        data: {
          title: 'Quản lý câu hỏi',
        },
      },
      {
        path: 'dot-khao-sat',
        component: AdminPeriodSurveyComponent,
        data: {
          title: 'dot-khao-sat',
        },
      },
      {
        path: 'bang-khao-sat',
        component:AdminTableSurveyComponent,
        data: {
          title: 'bang-khao-sat',
        },
      },
      {
        path: 'gui-email',
        component:AdminSendEmailComponent,
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
