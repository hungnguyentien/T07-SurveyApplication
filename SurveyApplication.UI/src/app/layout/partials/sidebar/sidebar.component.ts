import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  constructor(private router: Router) {}
  ngOnInit(): void {
    document.querySelectorAll(`a[href="${this.router.url}"]`).forEach((el) => {
      el.parentElement?.classList.add('active');
    });
    const navItems = document.querySelectorAll('.nav-item');
    const removeActive = () => {
      navItems.forEach((navItem) => {
        navItem.classList.remove('active');
      });
    };
    navItems.forEach((item) => {
      item.addEventListener('click', () => {
        removeActive();
        item.classList.add('active');
      });
    });
    // Lấy tất cả các phần tử mục con
    let collapseItems = document.querySelectorAll('.collapse-item');
    // Lặp qua tất cả các mục con và thêm/xóa lớp active khi mục được nhấp
    collapseItems.forEach(function (item) {
      item.addEventListener('click', function () {
        // Xóa lớp active từ tất cả các mục con
        collapseItems.forEach(function (item) {
          item.classList.remove('active');
        });
        // Thêm lớp active vào mục con được nhấp
        item.classList.add('active');
      });
    });
  }

  handlerClick = (link: string) => {
    this.router.navigate([link]);
  };
}
