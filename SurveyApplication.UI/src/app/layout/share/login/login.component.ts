import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from '@app/models';
import { LoginService } from '@app/services';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(private router: Router, private loginService: LoginService) {
    if (this.loginService.currentUserValue()) {
      this.router.navigate(['admin/home']);
    }
  }

  model: Login = {
    UserName: '',
    Password: '',
    grant_type: '',
  };
  ngOnInit(): void {
    // const tokenExists = this.cookieService.check('currentUser');
    // if (tokenExists) {
    //   console.log('Token đã tồn tại trong cookie.');
    // } else {
    //   console.log('Token không tồn tại trong cookie.');
    // }
  }

  login() {
    debugger
    this.loginService.login(this.model).subscribe((result: any) => {
      if (result) {
        // alert("Đăng nhập thành công !")
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
