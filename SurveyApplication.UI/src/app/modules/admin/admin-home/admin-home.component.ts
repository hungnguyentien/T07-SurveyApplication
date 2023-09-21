import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormGroup, Validators,FormBuilder } from '@angular/forms';
import { AdminHomeService } from '@app/services/admin-home.service';

import * as moment from 'moment';
import 'moment-timezone'; // Import 'moment-timezone'
@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent {
  dataNhomDoiTuong:any;
  optionNhomDoiTuong:any;
  dataDotKhaosat:any;
  optionDotKhaosat:any;
  barData:any;
  barOptions:any
  FormAminHome!:FormGroup
  listDashBoard!:any

    labelsDataChart:any
    realDataChart:any 
   sumBangKhoaSat!:number;
   sumDoiKhoaSat!:number;
   sumSoLuongThamGia!:number; 


  constructor(private adminHomeService:AdminHomeService , private FormBuilder: FormBuilder,private datePipe: DatePipe) {}
  ngOnInit():void {
    this.FormAminHome = this.FormBuilder.group(
        {
          NgayBatDau: ['', Validators.required],
          NgayKetThuc: ['', Validators.required],
        }
      );
    this.adminHomeService.GetAllDashBoard().subscribe(res=>{
        this.listDashBoard = res
        this.sumBangKhoaSat = this.listDashBoard.countBangKhaoSat;
        this.sumDoiKhoaSat = this.listDashBoard.countDotKhaoSat;
        this.sumSoLuongThamGia = this.listDashBoard.countThamGia;
        this.realDataChart = this.listDashBoard
        this.verticalBar(this.realDataChart);
        this.chart1(this.realDataChart)
        this.chart2(this.realDataChart)
    })
  }

  ngAfterViewInit(): void {}
  onSubmit(){
    const valuesNgay = this.FormAminHome.value;
    const ngayBatDauMoment = moment(valuesNgay.NgayBatDau);
    const ngayKetThucMoment = moment(valuesNgay.NgayKetThuc);
    const ngayBatDauStr = ngayBatDauMoment.format('MM/DD/YYYY');
    const ngayKetThucStr = ngayKetThucMoment.format('MM/DD/YYYY');
    this.adminHomeService.GetDashBoard(ngayBatDauStr,ngayKetThucStr).subscribe(res=>{
        this.listDashBoard = res
        this.sumBangKhoaSat = this.listDashBoard.countBangKhaoSat;
        this.sumDoiKhoaSat = this.listDashBoard.countDotKhaoSat;
        this.sumSoLuongThamGia = this.listDashBoard.countThamGia;
        this.realDataChart = this.listDashBoard
        this.verticalBar(this.realDataChart)
        this.chart1(this.realDataChart)
        this.chart2(this.realDataChart)
    })
  }
  chart1(datas:any){
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    this.dataNhomDoiTuong = {
        labels: ['Bộ', 'Sở', 'Doanh Nghiệp'],
        datasets: [
            {
                data: [datas.countDonViBo, datas.countDonViSo, datas.countDonViNganh],
                backgroundColor: [documentStyle.getPropertyValue('--blue-500'), documentStyle.getPropertyValue('--yellow-500'), documentStyle.getPropertyValue('--green-500')],
                hoverBackgroundColor: [documentStyle.getPropertyValue('--blue-400'), documentStyle.getPropertyValue('--yellow-400'), documentStyle.getPropertyValue('--green-400')]
            }
        ]
    };
    this.optionNhomDoiTuong = {
        cutout: '60%',
        plugins: {
            legend: {
                labels: {
                    color: textColor
                }
            }
        }
    };
  }
  chart2(datas:any){
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    this.dataDotKhaosat = {
        labels: ['Đợt 1', 'Đợt 2', 'Đợt 3'],
        datasets: [
            {
                data: [datas.countDot1, datas.countDot2, datas.countDot3],
                backgroundColor: [documentStyle.getPropertyValue('--orange-400'), documentStyle.getPropertyValue('--green-600'), documentStyle.getPropertyValue('--blue-400')],
                hoverBackgroundColor: [documentStyle.getPropertyValue('--orange-300'), documentStyle.getPropertyValue('--green-500'), documentStyle.getPropertyValue('--blue-300')]
            }
        ]
    };


    this.optionDotKhaosat = {
        cutout: '60%',
        plugins: {
            legend: {
                labels: {
                    color: textColor
                }
            }
        }
    };
  }

  verticalBar(datas:any){
    const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
        const labelsTinh = datas.listTinhTp.map((item:any) => item.tenTinhTp);
        const datasTinh = datas.listTinhTp.map((item:any) => item.countTinhTp);
        this.barData = {
            labels: labelsTinh,
            datasets: [
                {
                    label: 'Dữ Liệu',
                    data: datasTinh,
                    backgroundColor: documentStyle.getPropertyValue('--blue-400'),
                    borderColor: documentStyle.getPropertyValue('--blue-400'),
                    borderWidth: 1,
                }
            ]
        };

        this.barOptions = {
            maintainAspectRatio: false,
            aspectRatio: 0.8,
            plugins: {
                legend: {
                    labels: {
                        color: textColor
                    }
                }
            },
            scales: {
                x: {
                    ticks: {
                        color: textColorSecondary,
                        font: {
                            weight: 500,
                            width:'30'
                        }
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    },
                    
                },
                y: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    },
                    barPercentage: 0.3
                }

            }
        };
        
  }

  reset = () => {
    this.FormAminHome.reset();
    this.adminHomeService.GetAllDashBoard().subscribe(res=>{
        this.listDashBoard = res
        this.sumBangKhoaSat = this.listDashBoard.countBangKhaoSat;
        this.sumDoiKhoaSat = this.listDashBoard.countDotKhaoSat;
        this.sumSoLuongThamGia = this.listDashBoard.countThamGia;
        this.realDataChart = this.listDashBoard
        this.verticalBar(this.realDataChart);
        this.chart1(this.realDataChart)
        this.chart2(this.realDataChart)
    })
  }
}
