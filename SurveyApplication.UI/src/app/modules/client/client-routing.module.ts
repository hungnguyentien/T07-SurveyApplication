import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Error500Component } from '@app/layout/partials/error500/error500.component';
import {
  GeneralInfoComponent,
  SurveyInfoComponent,
} from '@app/modules/client';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Khảo sát',
    },
    children: [
      {
        path: 'thong-tin-chung/:data',
        component: GeneralInfoComponent,
        title: 'Thông tin chung',
        data: {
          title: 'Thông tin chung',
        },
      },
      {
        path: 'thong-tin-khao-sat',
        component: SurveyInfoComponent,
        title: 'Thông tin phiếu khảo sát',
        data: {
          title: 'Thông tin phiếu khảo sát',
        },
      },
      {
        path: 'error-500',
        component:Error500Component,
        title: 'Error 500',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientRoutingModule {}
