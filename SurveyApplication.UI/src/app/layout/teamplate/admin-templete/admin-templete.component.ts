import { Component } from '@angular/core';
import { LoginService } from '@app/services';

@Component({
  selector: 'app-admin-templete',
  templateUrl: './admin-templete.component.html',
  styleUrls: ['./admin-templete.component.css'],
})
export class AdminTempleteComponent {
  userName: string = '';
  constructor(private loginService: LoginService) {
    this.userName = loginService.getCurrentUser()?.name;
  }
  
  logout() {
    this.loginService.logout();
  }
}
