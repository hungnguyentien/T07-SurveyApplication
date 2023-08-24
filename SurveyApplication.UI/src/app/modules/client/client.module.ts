import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SurveyCreatorModule } from 'survey-creator-angular';
import { SurveyModule } from 'survey-angular-ui';

import { ClientHomeComponent } from './client-home/client-home.component';
import { ClientRoutingModule } from './client-routing.module';

@NgModule({
  declarations: [ClientHomeComponent],
  imports: [
    CommonModule,
    ClientRoutingModule,
    FormsModule,
    SurveyCreatorModule,
    SurveyModule,
  ]
})
export class ClientModule { }