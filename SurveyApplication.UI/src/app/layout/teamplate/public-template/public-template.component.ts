import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-public-template',
  templateUrl: './public-template.component.html',
  styleUrls: ['./public-template.component.css'],
})
export class TemplatePublicComponent implements OnInit {
  constructor() {}

  ngOnInit() {}

  pageTop = () => {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
  };
}
