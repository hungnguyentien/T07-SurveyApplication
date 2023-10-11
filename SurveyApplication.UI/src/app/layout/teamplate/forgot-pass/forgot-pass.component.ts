import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService} from 'primeng/api';
import { AccountService } from '@app/services/account.service';
import Utils from '@app/helpers/utils';
@Component({
  selector: 'app-forgot-pass',
  templateUrl: './forgot-pass.component.html',
  styleUrls: ['./forgot-pass.component.css']
})
export class ForgotPassComponent {
  submitted: boolean = false;
  frmForgotPass!:FormGroup;
  data:any;
  constructor(private formBuilder:FormBuilder,private router: Router,private accountService:AccountService, private messageService:MessageService){}
  ngOnInit() {
    this.actionRequest();
    this.frmForgotPass = this.formBuilder.group(
      {
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]],
      }
    ), { validator: this.checkPasswords };
  }

  
  actionRequest() {
    this.router.events.subscribe(event => {
      // Lấy chuỗi URL sau khi thay đổi route
      const currentURL = this.router.url;
      // Tách phần query từ dấu "?"
      const queryString = currentURL.split('?')[1];
      type ParamMap = Record<string, string>;
      // Tạo một đối tượng để lưu trữ các tham số
      const paramMap: ParamMap = {};
      // Tách các tham số từ dấu "&" và lưu vào đối tượng paramMap
      const params = queryString.split('&');
      for (let i = 0; i < params.length; i++) {
        const keyValue = params[i].split('=');
        const key = decodeURIComponent(keyValue[0]);
        const value = decodeURI(keyValue.slice(1).join('='));
        paramMap[key] = value;
      }
      this.data = paramMap;
    });
  }
  

  checkPasswords: ValidatorFn = (
    group: AbstractControl
  ): ValidationErrors | null => {
    let pass = group.get('password')?.value;
    let confirmPass = group.get('passwordConfirmed')?.value;
    return pass === confirmPass ? null : { notSame: true };
  };
  f = (name: string, subName: string = ''): FormControl => {
    return (
      subName
        ? this.frmForgotPass?.get(name)?.get(subName)
        : this.frmForgotPass?.get(name)
    ) as FormControl;
  };
  onSubmit(){
    debugger
    const frmData = this.frmForgotPass.value;
    frmData['email'] = this.data.email;
    frmData['token'] = this.data.token;
    this.accountService.resetPassword(frmData).subscribe({
      next: (res) => {
        console.log(res)
        debugger
       if(res.success == true){
        Utils.messageSuccess(this.messageService, res.message);
       }else{
        Utils.messageError(this.messageService, res.message);
       }
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
      }
    })
  }
}
