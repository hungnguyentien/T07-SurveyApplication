import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientHomeComponent } from './client-home/client-home.component'; 
import { ClientComponent } from './client.component'; 

const routes: Routes = [
  { 
    path: '', component: ClientComponent,
    children:[
      { path: '', component: ClientHomeComponent}

    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
