import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '@app/models';
import { LoginService } from '@app/services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  model: Login = {
    UserName: '',
    Password: '',
    grant_type: '',
    isRememberMe: true
  };

  constructor(private router: Router, private loginService: LoginService) {
    if (this.loginService.currentUserValue()) {
      this.router.navigate(['admin/home']);
    }
  }

  ngOnInit(): void {}

  login() {
    this.loginService.login(this.model).subscribe((result: any) => {
      if (result) {
        this.router.navigate(['admin/home']);
      } else {
        alert('Lỗi không thành công !');
      }
    });
  }

  handlerClick = (link: string) => {
    this.router.navigate([link]);
  };
}
