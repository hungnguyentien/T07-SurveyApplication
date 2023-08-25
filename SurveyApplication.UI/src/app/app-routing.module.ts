import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: '',
    data: {
      title: 'Default',
    },
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./layout/teamplate/teamplate.module').then(
            (x) => x.TeamplateModule
          ),
      },
    ],
  },
  
  {
    path: '**',
    redirectTo: 'login',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
