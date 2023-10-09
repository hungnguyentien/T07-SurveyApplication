import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { HighlightDirective } from './highlight.directive';
import { HighlightGuiEmailDirective } from './highlight-gui-email.directive';
import { PasswordModule } from 'primeng/password';
import { DynamicTableComponent } from './dynamic-table/dynamic-table.component';
import { TableModule } from 'primeng/table';
@NgModule({
  declarations: [LoginComponent, HighlightDirective, HighlightGuiEmailDirective, DynamicTableComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    ToastModule,
    PasswordModule,
    TableModule
  ],
  exports: [HighlightDirective, HighlightGuiEmailDirective],
})
export class ShareModule {}
