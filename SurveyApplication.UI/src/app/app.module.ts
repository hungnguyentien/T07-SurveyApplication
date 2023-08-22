import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminHomeComponent } from './components/admin/admin-home/admin-home.component';
import { ClientComponent } from './components/client/client.component';
import { AdminComponent } from './components/admin/admin.component';
import { AdminRoutingModule } from './components/admin/admin-routing.module';
import { ClientRoutingModule } from './components/client/client-routing.module';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown'; 
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { MultiSelectModule } from 'primeng/multiselect';
import { TagModule } from 'primeng/tag';
import { SliderModule } from 'primeng/slider';
import { ProgressBarModule } from 'primeng/progressbar';
import { HttpClientModule } from '@angular/common/http';
import { SurveyCreatorModule } from 'survey-creator-angular';
import { SurveyModule } from "survey-angular-ui";

import { AdminObjectSurveyComponent } from './components/admin/admin-object-survey/admin-object-survey.component';
import { AdminUnitTypeComponent } from './components/admin/admin-unit-type/admin-unit-type.component';
import { ClientHomeComponent } from './components/client/client-home/client-home.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminHomeComponent,
    ClientHomeComponent,
    ClientComponent,
    AdminComponent,
    AdminObjectSurveyComponent,
    AdminUnitTypeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AdminRoutingModule,
    ClientRoutingModule,
    ButtonModule,
    DropdownModule,
    FormsModule,
    BrowserAnimationsModule,
    InputTextModule,
    TableModule,
    MultiSelectModule,
    TagModule,
    SliderModule,
    ProgressBarModule,
    HttpClientModule,
    SurveyCreatorModule,
    SurveyModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
