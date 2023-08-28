import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(private router: Router, private titleService: Title) {
    this.titleService.setTitle('Quản lý khảo sát');
  }
  handlerClick = (link: string) => {
    this.router.navigate([link]);
  };
}
