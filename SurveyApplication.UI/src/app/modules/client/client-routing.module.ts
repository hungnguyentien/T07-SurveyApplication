import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  ClientHomeComponent,
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
        path: 'khao-sat',
        component: ClientHomeComponent,
        data: {
          title: 'Phiếu khảo sát',
        },
      },
      {
        path: 'thong-tin-chung',
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
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientRoutingModule {}
