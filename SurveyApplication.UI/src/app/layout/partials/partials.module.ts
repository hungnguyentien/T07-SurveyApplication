import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterClientComponent } from './footer-client/footer-client.component';
import { HeaderClientComponent } from './header-client/header-client.component';
import { RouterModule } from '@angular/router';
import { ShareModule } from '../share/share.module';

@NgModule({
  declarations: [
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    FooterClientComponent,
    HeaderClientComponent,
  ],
  imports: [CommonModule, RouterModule, ShareModule],
  exports: [
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    FooterClientComponent,
    HeaderClientComponent,
    HeaderComponent
  ],
})
export class PartialsModule {}
