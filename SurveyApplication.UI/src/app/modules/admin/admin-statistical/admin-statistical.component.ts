import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import Utils from '@app/helpers/utils';
import {
  BaoCaoCauHoiService,
  PeriodSurveyService,
  TableSurveyService,
  UnitTypeService,
} from '@app/services';
import { TranslateService } from '@ngx-translate/core';
import { MessageService, PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-admin-statistical',
  templateUrl: './admin-statistical.component.html',
  styleUrls: ['./admin-statistical.component.css'],
})
export class AdminStatisticalComponent {
  frmStatiscal!: FormGroup;
  LstDotKhaoSat: any[] = [];
  LstBangKhaoSat: any[] = [];
  LstLoaiHinhDv: any[] = [];

  doughnutData: any;
  doughnutOptions: any;

  barData: any;
  barOptions: any;

  selectedDotKhaoSat: number = 1;
  selectedBangKhaoSat: number = 1;

  constructor(
    private formBuilder: FormBuilder,
    private periodSurveyService: PeriodSurveyService,
    private tableSurveyService: TableSurveyService,
    private unitTypeService: UnitTypeService,
    private config: PrimeNGConfig,
    private translateService: TranslateService,
    private messageService: MessageService,
    private baoCaoCauHoiService: BaoCaoCauHoiService
  ) {}

  ngOnInit() {
    Utils.translate('vi', this.translateService, this.config);
    this.frmStatiscal = this.formBuilder.group({
      idDotKhaoSat: [this.selectedDotKhaoSat],
      idBangKhaoSat: [this.selectedBangKhaoSat],
      idLoaiHinh: [],
      ngayBatDau: [],
      ngayKetThuc: [],
    });

    this.loadPeriodSurvey();
    this.loadTableSurvey();
    this.loadUnitType();
    this.getVauleChar(this.frmStatiscal.value);
  }

  getVauleChar = (params: any) => {
    this.baoCaoCauHoiService.getBaoCaoCauHoi(params).subscribe({
      next: (res) => {
        this.setChar(
          [res.countDonViMoi, res.countDonViTraLoi],
          [res.countDonViSo, res.countDonViBo, res.countDonViNganh]
        );
      },
    });
  };

  setChar = (doughnutData: number[], barData: number[]) => {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    this.doughnutData = {
      labels: ['Đơn vị được mời', 'Đơn vị tham gia khảo sát'],
      datasets: [
        {
          data: doughnutData,
          backgroundColor: [
            documentStyle.getPropertyValue('--blue-500'),
            documentStyle.getPropertyValue('--yellow-500'),
          ],
          hoverBackgroundColor: [
            documentStyle.getPropertyValue('--blue-400'),
            documentStyle.getPropertyValue('--yellow-400'),
          ],
        },
      ],
    };

    this.doughnutOptions = {
      cutout: '60%',
      plugins: {
        legend: {
          labels: {
            color: textColor,
          },
        },
      },
    };

    const textColorSecondary = documentStyle.getPropertyValue(
      '--text-color-secondary'
    );
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
    this.barData = {
      labels: ['Sở', 'Bộ', 'Ngành'],
      datasets: [
        {
          label: 'Đối tượng',
          backgroundColor: documentStyle.getPropertyValue('--blue-500'),
          borderColor: documentStyle.getPropertyValue('--blue-500'),
          data: barData,
          borderWidth: 1,
        },
      ],
    };

    this.barOptions = {
      responsive: true,
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
        y: {
          beginAtZero: true,
          ticks: {
            color: textColorSecondary,
          },
          grid: {
            color: surfaceBorder,
            drawBorder: false,
          },
        },
        x: {
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
  };

  search = () => {
    let params = this.frmStatiscal.value;
    if (!params.idDotKhaoSat)
      Utils.messageError(this.messageService, `Vui lòng chọn đợt khảo sát!`);
    else if (!params.idBangKhaoSat)
      Utils.messageError(this.messageService, `Vui lòng chọn bảng khảo sát!`);
    else this.getVauleChar(params);
  };

  reset = () => {this.frmStatiscal.reset()}

  loadPeriodSurvey() {
    this.periodSurveyService.getAll().subscribe((data) => {
      this.LstDotKhaoSat = data; // Lưu dữ liệu vào danh sách
    });
  }

  loadTableSurvey() {
    this.tableSurveyService.getAll().subscribe((data) => {
      this.LstBangKhaoSat = data; // Lưu dữ liệu vào danh sách
    });
  }

  loadUnitType() {
    this.unitTypeService.getAll().subscribe((data) => {
      this.LstLoaiHinhDv = data; // Lưu dữ liệu vào danh sách
    });
  }
}
