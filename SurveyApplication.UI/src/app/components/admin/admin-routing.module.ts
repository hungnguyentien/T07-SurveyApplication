import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminObjectSurveyComponent } from './admin-object-survey/admin-object-survey.component';
import { AdminUnitTypeComponent } from './admin-unit-type/admin-unit-type.component';
import { AdminPeriodSurveyComponent } from './admin-period-survey/admin-period-survey.component';
import { QuestionComponent } from './admin-question/question/question.component';


const routes: Routes = [
  { 
    path: 'admin', component: AdminComponent,
    children:[
      { path: '', component: AdminHomeComponent},
      { path: 'doi-tuong-khao-sat', component: AdminObjectSurveyComponent},
      { path: 'loai-hinh-don-vi', component: AdminUnitTypeComponent},
      { path: 'dot-khao-sat', component: AdminPeriodSurveyComponent},
      { path: 'question', component: QuestionComponent},

    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
