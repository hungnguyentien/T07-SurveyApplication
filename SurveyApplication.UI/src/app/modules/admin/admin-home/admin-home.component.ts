import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit{

  cities: any[] = [
    { name: 'New York' },
    { name: 'Los Angeles' },
    { name: 'Chicago' },
  ];
  selectedCity: any; // Biến để lưu trữ thành phố được chọn
  constructor(){}
  ngOnInit(): void {
    
  }
}
