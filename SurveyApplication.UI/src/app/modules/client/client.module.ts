import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SurveyCreatorModule } from 'survey-creator-angular';
import { SurveyModule } from 'survey-angular-ui';
import { NgxSpinnerModule } from "ngx-spinner";
import { DropdownModule } from 'primeng/dropdown';

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
    NgxSpinnerModule,
    ///TODO add primeng
    DropdownModule,
  ],
})
export class ClientModule {}
