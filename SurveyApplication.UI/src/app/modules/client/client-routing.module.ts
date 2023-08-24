import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientHomeComponent } from './client-home/client-home.component';

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
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClientRoutingModule {}
