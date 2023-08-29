import { Component } from '@angular/core';
import { ServiceService } from '@app/services';
import { Customer, Representative, UnitType } from '@app/models';
import { UnitTypeService } from '@app/services/unit-type.service';

@Component({
  selector: 'app-admin-unit-type',
  templateUrl: './admin-unit-type.component.html',
  styleUrls: ['./admin-unit-type.component.css'],
})
export class AdminUnitTypeComponent {
  public value: any;
  customers!: Customer[];
  representatives!: Representative[];
  statuses!: any[];
  loading: boolean = true;
  activityValues: number[] = [0, 100];
  

  selectedUnitType!: UnitType[];
  datas : UnitType [] = [];
  MaLoaiHinh: any;
  
  
  

  constructor(private UnitTypeService:UnitTypeService,private customerService: ServiceService) {}
  ngOnInit() {
    this.customerService.getCustomersLarge().then((customers) => {
      this.customers = customers;
      this.loading = false;
    });
  }

  
  first: number = 0;
  rows: number = 5;
  page: number = 1;
  TotalCount = 0;
  onPageChange(event:any) {
      this.first = event.first;
      this.rows = event.rows;
      this.page = event.page+1
      let obj = {
        pageIndex: this.page,
        pageSize: this.rows,
        keyword: this.MaLoaiHinh
      }
      this.UnitTypeService.Search("api/CuDuong/cuduong_search",obj).subscribe(res=>{
        this.datas = res.Data
        this.TotalCount = res.TotalItems
      })
  }
  search(){
    let obj = {
      pageIndex: this.page,
      pageSize: this.rows,
      keyword: this.MaLoaiHinh
    }
    this.UnitTypeService.Search("api/CuDuong/cuduong_search",obj).subscribe(res=>{
      debugger
      this.datas = res.data
      this.TotalCount = res.totalItems
    })
  }
}
