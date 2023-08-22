import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminObjectSurveyComponent } from './admin-object-survey/admin-object-survey.component';
import { AdminUnitTypeComponent } from './admin-unit-type/admin-unit-type.component';


const routes: Routes = [
  { 
    path: 'admin', component: AdminComponent,
    children:[
      { path: '', component: AdminHomeComponent},
      { path: 'doi-tuong-khao-sat', component: AdminObjectSurveyComponent},
      { path: 'loai-hinh-don-vi', component: AdminUnitTypeComponent}
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
