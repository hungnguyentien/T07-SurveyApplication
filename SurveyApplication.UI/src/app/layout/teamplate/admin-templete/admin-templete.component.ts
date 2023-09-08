import { Component } from '@angular/core';
import { LoginService } from '@app/services';

@Component({
  selector: 'app-admin-templete',
  templateUrl: './admin-templete.component.html',
  styleUrls: ['./admin-templete.component.css'],
})
export class AdminTempleteComponent {
  constructor(private loginService: LoginService) {}
  logout() {
    this.loginService.logout();
  }
}
