import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-general-info',
  templateUrl: './general-info.component.html',
  styleUrls: ['./general-info.component.css'],
})
export class GeneralInfoComponent {
  constructor(private router: Router, private titleService: Title) {
    this.titleService.setTitle('Thông tin chung');
  }

  handlerClick = (link: string) => {
    this.router.navigate([link]);
  };
}
