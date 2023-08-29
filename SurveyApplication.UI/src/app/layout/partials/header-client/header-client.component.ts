import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-client',
  templateUrl: './header-client.component.html',
  styleUrls: ['./header-client.component.css']
})
export class HeaderClientComponent {
  constructor(private router: Router) {}
  handlerClick = (link: string) => {
    this.router.navigate([link]);
  };
}
