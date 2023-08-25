import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

@NgModule({
  declarations: [AdminHomeComponent],
  imports: [
    CommonModule,
    FormsModule,
    DropdownModule,
    AdminRoutingModule,
    CKEditorModule
  ]
})
export class AdminModule { }