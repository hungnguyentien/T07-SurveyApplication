import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  constructor(private router: Router) {}
  handlerClick = (link: string) => {
    this.router.navigate([link]);
  };
  ngOnInit(): void {
    // const navItems = document.querySelectorAll('.nav-item');
    // navItems.forEach((item) => {
    //   item.addEventListener('click', () => {
    //     navItems.forEach((navItem) => {
    //       navItem.classList.remove('active');
    //     });
    //     item.classList.add('active');
    //   });
    // });
    // // Lấy tất cả các phần tử mục con
    // let collapseItems = document.querySelectorAll('.collapse-item');
    // // Lặp qua tất cả các mục con và thêm/xóa lớp active khi mục được nhấp
    // collapseItems.forEach(function (item) {
    //   item.addEventListener('click', function () {
    //     // Xóa lớp active từ tất cả các mục con
    //     collapseItems.forEach(function (item) {
    //       item.classList.remove('active');
    //     });
    //     // Thêm lớp active vào mục con được nhấp
    //     item.classList.add('active');
    //   });
    // });
  }
}
