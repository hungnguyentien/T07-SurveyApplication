import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { AdminSendEmailComponent } from './admin-send-email/admin-send-email.component';
import { AdminTableSurveyComponent } from './admin-table-survey/admin-table-survey.component';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { MessageModule } from 'primeng/message';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog'; // Import ConfirmDialogModule
import { InputSwitchModule } from 'primeng/inputswitch';
import { DatePipe } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FieldsetModule } from 'primeng/fieldset';
import { DialogModule } from 'primeng/dialog';

@NgModule({
  declarations: [
    AdminHomeComponent,
    AdminSendEmailComponent,
    AdminTableSurveyComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    DropdownModule,
    AdminRoutingModule,
    CKEditorModule,
    PaginatorModule,
    TableModule,
    MessageModule,
    ReactiveFormsModule,
    ToastModule,
    ConfirmDialogModule,
    InputSwitchModule,
    ButtonModule,
    CalendarModule,
    InputTextModule,
    InputTextareaModule,
    FieldsetModule,
    DialogModule

  ],
  providers:[DatePipe]
})
export class AdminModule {}
