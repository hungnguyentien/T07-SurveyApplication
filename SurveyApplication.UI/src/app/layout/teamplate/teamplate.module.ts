import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { TeamplateRoutingModule } from './teamplate-routing.module';
import { TemplatePublicComponent } from './public-template/public-template.component';
// import { PartialsModule } from '../partials/partials.module';
import { ShareModule } from '../share/share.module';
import { AdminTempleteComponent } from './admin-templete/admin-templete.component';
import { PartialsModule } from "../partials/partials.module";
// import { SidebarTemplateComponent } from './sidebar-template/sidebar-template.component';
@NgModule({
    declarations: [
        TemplatePublicComponent,
        AdminTempleteComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TeamplateRoutingModule,
        ShareModule,
        PartialsModule
    ]
})
export class TeamplateModule {}
