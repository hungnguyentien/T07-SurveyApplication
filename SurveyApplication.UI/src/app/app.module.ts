import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminRoutingModule } from './modules/admin/admin-routing.module';
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
import { ReactiveFormsModule } from '@angular/forms';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { TreeSelectModule } from 'primeng/treeselect';

import { AdminObjectSurveyComponent } from './modules/admin/admin-object-survey/admin-object-survey.component';
import { AdminUnitTypeComponent } from './modules/admin/admin-unit-type/admin-unit-type.component';
import { ChooseAnAnswerComponent } from './modules/admin/admin-question/choose-an-answer/choose-an-answer.component';
import { LongTextComponent } from './modules/admin/admin-question/long-text/long-text.component';
import { QuestionComponent } from './modules/admin/admin-question/question/question.component';
import { AdminPeriodSurveyComponent } from './modules/admin/admin-period-survey/admin-period-survey.component';

import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { PaginatorModule } from 'primeng/paginator';
import { MessagesModule } from 'primeng/messages';
@NgModule({
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  declarations: [
    AppComponent,
    AdminObjectSurveyComponent,
    AdminUnitTypeComponent,
    AdminPeriodSurveyComponent,
    ChooseAnAnswerComponent,
    LongTextComponent,
    QuestionComponent,
    
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AdminRoutingModule,
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
    ReactiveFormsModule,
    ConfirmDialogModule,
    ToastModule,
    TreeSelectModule,
    CKEditorModule,
    PaginatorModule,
    MessagesModule,
    
  ],
  providers: [MessageService, ConfirmationService],
  bootstrap: [AppComponent],
})
export class AppModule {}
