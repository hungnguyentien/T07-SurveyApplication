import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import Utils from '@app/helpers/utils';
import { Login } from '@app/models';
import { LoginService } from '@app/services';
import { MessageService } from 'primeng/api';
import { NgxSpinnerService } from "ngx-spinner";
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  visible: boolean = false;
  forgotPass:string ="";
  model: Login = {
    UserName: '',
    Password: '',
    grant_type: '',
    isRememberMe: true,
  };

  constructor(
    private router: Router,
    private loginService: LoginService,
    private titleService: Title,
    private messageService: MessageService,
    private spinner: NgxSpinnerService
  ) {
    this.titleService.setTitle('Đăng nhập');
    if (this.loginService.currentUserValue()) {
      this.router.navigate(['admin/dashboard']);
    }
  }

  ngOnInit() {
    console.clear();
  }

  login() {
    this.spinner.show();
    setTimeout(() => {
    this.loginService.login(this.model).subscribe((result: any) => {
      if (result) {
        this.router.navigate(['admin/dashboard']);
      }
    });
    this.spinner.hide();
  }, 2000);
  }

  handlerClick = (link: string) => {
    this.router.navigate([link]);
  };

  forgotPassword(){
    this.visible = true;
  }
  SubmitforgotPass(){
    const data = this.forgotPass 
    this.loginService.forgotPass(data).subscribe({
      next: (res: any) => {
        Utils.messageSuccess(this.messageService, res.message);
      },
      error: (e) => Utils.messageError(this.messageService, e.message)
    })
  }
}
