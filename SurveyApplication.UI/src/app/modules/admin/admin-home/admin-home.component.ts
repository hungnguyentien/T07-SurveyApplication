import { Component } from '@angular/core';

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
  constructor() {}
  ngOnInit() {
    this.chart1();
    this.chart2();
    this.verticalBar();
  }

  ngAfterViewInit(): void {}

  chart1(){
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');

    this.dataNhomDoiTuong = {
        labels: ['Bộ', 'Sở', 'Doanh Nghiệp'],
        datasets: [
            {
                data: [300, 50, 100],
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
  chart2(){
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');

    this.dataDotKhaosat = {
        labels: ['Đợt 1', 'Đợt 2', 'Đợt 3'],
        datasets: [
            {
                data: [100, 100, 100],
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

  verticalBar(){
    const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
        
        this.barData = {
            labels: ['Hà Nội', 'Hưng Yên', 'Hải Phòng', 'Hải Dương', 'Nghệ An', 'Bắc Ninh'],
            datasets: [
                {
                    backgroundColor: documentStyle.getPropertyValue('--blue-400'),
                    borderColor: documentStyle.getPropertyValue('--blue-400'),
                    data: [100, 200, 400, 600, 800, 1000],
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
                            weight: 500
                        }
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                },
                y: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: false
                    }
                }

            }
        };
  }
}
