import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CodeModule, KqTrangThai } from '@app/enums';
import { Module } from '@app/models';
import { LoginService, ModuleService } from '@app/services';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  lstModule: Module[] = [];
  module!: Module;
  constructor(
    private router: Router,
    private moduleService: ModuleService,
    private loginService: LoginService
  ) {}
  ngOnInit(): void {
    this.moduleService.getAll().subscribe({
      next: (res) => {
        this.lstModule = [];
        const currentUser = this.loginService.getCurrentUser();
        res.forEach((x) => {
          if (
            x.routerLink === '#' &&
            this.hasModuleChil(
              res.filter((p) => p.idParent == x.id),
              currentUser
            )
          )
            this.lstModule.push(x);
          else if (currentUser[(CodeModule as any)[x.codeModule]])
            this.lstModule.push(x);
        });
      },
    });
  }

  hasModuleChil(chil: Module[], currentUser: any): boolean {
    return !!chil.find((x) => currentUser[(CodeModule as any)[x.codeModule]]);
  }

  ngAfterViewChecked() {
    document.querySelectorAll(`a[href="${this.router.url}"]`).forEach((el) => {
      el.parentElement?.classList.add('active');
    });
  }

  handlerClick = (link: string) => {
    this.router.navigate([link]);
  };

  trackByFn(index: number) {
    return index;
  }

  getLstModuleNoChil(): Module[] {
    return this.lstModule.filter(
      (x) => x.routerLink !== '#' && x.idParent === 0
    );
  }

  getLstModuleParent(): Module[] {
    return this.lstModule.filter((x) => x.routerLink === '#');
  }

  getLstModuleChil(idParent: number): Module[] {
    return this.lstModule.filter((x) => x.idParent === idParent);
  }
}
