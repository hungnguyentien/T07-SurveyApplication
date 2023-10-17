import { Component, ViewChild } from '@angular/core';
import Utils from '@app/helpers/utils';
import { IpAddress, Paging } from '@app/models';
import { IpaddressService } from '@app/services';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-admin-ipaddress',
  templateUrl: './admin-ipaddress.component.html',
  styleUrls: ['./admin-ipaddress.component.css'],
})
export class AdminIpaddressComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas: IpAddress[] = [];
  selectedIpaddress!: IpAddress[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  constructor(
    private ipaddressService: IpaddressService,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.loading = true;
  }

  // loadListLazy = (event: any) => {
  //   this.loading = true;
  //   let pageSize = event.rows;
  //   let pageIndex = event.first / pageSize + 1;
  //   this.paging = {
  //     pageIndex: pageIndex,
  //     pageSize: pageSize,
  //     keyword: '',
  //     orderBy: event.sortField
  //       ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
  //       : '',
  //   };
  //   this.ipaddressService.getByCondition(this.paging).subscribe({
  //     next: (res) => {
  //       this.datas = res.data;
  //       this.dataTotalRecords = res.totalFilter;
  //     },
  //     error: (e) => {
  //       Utils.messageError(this.messageService, e.message);
  //       this.loading = false;
  //     },
  //     complete: () => {
  //       this.loading = false;
  //     },
  //   });
  // };

  loadListLazy = (event: any) => {

    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;

    // Khởi tạo keyword là một chuỗi trống
    let keyword = this.keyWord || '';

    // Lặp qua các filter để xây dựng keyword
    for (const field in event.filters) {
      if (event.filters.hasOwnProperty(field)) {
        const filterValue = event.filters[field][0].value;
        if (filterValue != null) {
          // Nếu filterValue không null, thì thêm vào keyword với điều kiện phù hợp
          keyword += `${filterValue}`;
        }
      }
    }

    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: keyword, // Sử dụng keyword mới xây dựng từ các filter
      orderBy: event.sortField
        ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
        : '',
    };

    this.ipaddressService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.dataTotalRecords = res.totalFilter;
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };

  onSubmitSearch = () => {
    this.paging.keyword = this.keyWord;
    this.ipaddressService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.dataTotalRecords = res.totalFilter;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };
}
