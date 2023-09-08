import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '@app/services';

@Component({
  selector: 'app-admin-templete',
  templateUrl: './admin-templete.component.html',
  styleUrls: ['./admin-templete.component.css'],
})
export class AdminTempleteComponent {
  constructor(private loginService: LoginService, private router: Router) {}
  logout() {
    this.loginService.logout();
  }
}
