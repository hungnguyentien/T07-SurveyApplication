import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { HighlightDirective } from './highlight.directive';
@NgModule({
  declarations: [LoginComponent, HighlightDirective],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    ToastModule,
  ],
  exports: [HighlightDirective],
})
export class ShareModule {}
