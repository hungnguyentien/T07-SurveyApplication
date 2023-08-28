import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SurveyCreatorModule } from 'survey-creator-angular';
import { SurveyModule } from 'survey-angular-ui';
import { NgxLoadingModule } from 'ngx-loading';
import { ToastModule } from 'primeng/toast';

import { ClientHomeComponent } from './client-home/client-home.component';
import { ClientRoutingModule } from './client-routing.module';
import { GeneralInfoComponent } from './general-info/general-info.component';
import { SurveyInfoComponent } from './survey-info/survey-info.component';

@NgModule({
  declarations: [
    ClientHomeComponent,
    GeneralInfoComponent,
    SurveyInfoComponent,
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
    FormsModule,
    SurveyCreatorModule,
    SurveyModule,
    ReactiveFormsModule,
    NgxLoadingModule.forRoot({}),
    ToastModule
  ],
})
export class ClientModule {}
