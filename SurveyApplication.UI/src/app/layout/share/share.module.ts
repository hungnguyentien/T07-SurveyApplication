import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login/login.component';
import { TableComponent } from './table/table.component';

@NgModule({
  declarations: [LoginComponent, TableComponent],
  imports: [CommonModule],
})
export class ShareModule {}
