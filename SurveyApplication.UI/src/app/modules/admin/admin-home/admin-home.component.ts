import { Component } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import Utils from '@app/helpers/utils';
import { DashBoard } from '@app/models';
import { AdminHomeService } from '@app/services/admin-home.service';
import { TranslateService } from '@ngx-translate/core';

import * as moment from 'moment';
import 'moment-timezone'; // Import 'moment-timezone'
import { PrimeNGConfig } from 'primeng/api';
@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent {
  dataNhomDoiTuong: any;
  optionNhomDoiTuong: any;
  dataDotKhaosat: any;
  optionDotKhaosat: any;
  barData: any;
  barOptions: any;
  FormAminHome!: FormGroup;
  listDashBoard!: DashBoard;

  realDataChart!: DashBoard;
  sumBangKhoaSat!: number;
  sumDoiKhoaSat!: number;
  sumSoLuongThamGia!: number;

  constructor(
    private router: Router,
    private FormBuilder: FormBuilder,
    private adminHomeService: AdminHomeService,
    private config: PrimeNGConfig,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    Utils.translate('vi', this.translateService, this.config);
    this.FormAminHome = this.FormBuilder.group({
      NgayBatDau: ['', Validators.required],
      NgayKetThuc: ['', Validators.required],
    });
    this.adminHomeService.GetAllDashBoard().subscribe((res) => {
      this.listDashBoard = res;
      this.sumBangKhoaSat = this.listDashBoard.countBangKhaoSat;
      this.sumDoiKhoaSat = this.listDashBoard.countDotKhaoSat;
      this.sumSoLuongThamGia = this.listDashBoard.countThamGia;
      this.realDataChart = this.listDashBoard;
      this.verticalBar(this.realDataChart);
      this.chart1(this.realDataChart);
      this.chart2(this.realDataChart);
    });
  }

  handlerClick = (link: string) => {
    this.router.navigate([link]);
    const navItems = document.querySelectorAll('.nav-item');
    navItems.forEach((navItem) => {
      navItem.classList.remove('active');
      navItem.children[0].classList.add('collapsed');
      let div = navItem.children[1];
      div && div.classList.remove('show');
    });
  };

  ngAfterViewInit(): void {}

  onSubmit() {
    const valuesNgay = this.FormAminHome.value;
    const ngayBatDauStr = valuesNgay.NgayBatDau
      ? moment(valuesNgay.NgayBatDau).format('MM/DD/YYYY')
      : '';
    const ngayKetThucStr = valuesNgay.NgayKetThuc
      ? moment(valuesNgay.NgayKetThuc).format('MM/DD/YYYY')
      : '';
    this.adminHomeService
      .GetDashBoard(ngayBatDauStr, ngayKetThucStr)
      .subscribe((res) => {
        this.listDashBoard = res;
        this.sumBangKhoaSat = this.listDashBoard.countBangKhaoSat;
        this.sumDoiKhoaSat = this.listDashBoard.countDotKhaoSat;
        this.sumSoLuongThamGia = this.listDashBoard.countThamGia;
        this.realDataChart = this.listDashBoard;
        this.verticalBar(this.realDataChart);
        this.chart1(this.realDataChart);
        this.chart2(this.realDataChart);
      });
  }

  chart1(datas: DashBoard) {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    this.dataNhomDoiTuong = {
      labels: datas.lstCountDonViByLoaiHinh.map((x) => x.ten),
      datasets: [
        {
          data: datas.lstCountDonViByLoaiHinh.map((x) => x.count),
          backgroundColor: [
            documentStyle.getPropertyValue('--blue-500'),
            documentStyle.getPropertyValue('--yellow-500'),
            documentStyle.getPropertyValue('--green-500'),
          ],
          hoverBackgroundColor: [
            documentStyle.getPropertyValue('--blue-400'),
            documentStyle.getPropertyValue('--yellow-400'),
            documentStyle.getPropertyValue('--green-400'),
          ],
        },
      ],
    };
    this.optionNhomDoiTuong = {
      cutout: '60%',

      plugins: {
        legend: {
          labels: {
            color: textColor,
          },
        },
      },
    };
  }

  chart2(datas: DashBoard) {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    this.dataDotKhaosat = {
      labels: datas.lstCountDot.map((x) => x.ten),
      datasets: [
        {
          borderWidth: 2,
          data: datas.lstCountDot.map((x) => x.count),
          backgroundColor: [
            documentStyle.getPropertyValue('--orange-400'),
            documentStyle.getPropertyValue('--green-600'),
            documentStyle.getPropertyValue('--blue-400'),
          ],
          hoverBackgroundColor: [
            documentStyle.getPropertyValue('--orange-300'),
            documentStyle.getPropertyValue('--green-500'),
            documentStyle.getPropertyValue('--blue-300'),
          ],
        },
      ],
    };

    this.optionDotKhaosat = {
      cutout: '60%',
      plugins: {
        legend: {
          labels: {
            color: textColor,
          },
        },
      },
    };
  }

  verticalBar(datas: any) {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue(
      '--text-color-secondary'
    );
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
    const labelsTinh = datas.listTinhTp.map((item: any) => item.tenTinhTp);
    const datasTinh = datas.listTinhTp.map((item: any) => item.countTinhTp);
    this.barData = {
      labels: labelsTinh,
      datasets: [
        {
          label: 'Dữ Liệu',
          data: datasTinh,
          backgroundColor: documentStyle.getPropertyValue('--blue-400'),
          borderColor: documentStyle.getPropertyValue('--blue-400'),
          borderWidth: 1,
          categoryPercentage: 0.2, // sử dụng set độ rộng của cột
        },
      ],
    };

    this.barOptions = {
      maintainAspectRatio: false,
      aspectRatio: 0.8,
      plugins: {
        legend: {
          labels: {
            color: textColor,
          },
        },
      },
      scales: {
        x: {
          ticks: {
            color: textColorSecondary,
            font: {
              weight: 500,
              width: '30',
            },
          },
          grid: {
            color: surfaceBorder,
            drawBorder: false,
          },
        },
        y: {
          ticks: {
            color: textColorSecondary,
          },
          grid: {
            color: surfaceBorder,
            drawBorder: false,
          },
        },
      },
    };
  }

  reset = () => {
    this.FormAminHome.reset();
    this.adminHomeService.GetAllDashBoard().subscribe((res) => {
      this.listDashBoard = res;
      this.sumBangKhoaSat = this.listDashBoard.countBangKhaoSat;
      this.sumDoiKhoaSat = this.listDashBoard.countDotKhaoSat;
      this.sumSoLuongThamGia = this.listDashBoard.countThamGia;
      this.realDataChart = this.listDashBoard;
      this.verticalBar(this.realDataChart);
      this.chart1(this.realDataChart);
      this.chart2(this.realDataChart);
    });
  };
}
