import { Component, ViewChild } from '@angular/core';
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
import * as moment from 'moment';
import 'moment-timezone'; // Import 'moment-timezone'
import * as XLSX from 'xlsx';
import { Table } from 'primeng/table';
import {
  BaoCaoCauHoiChiTiet,
  BaoCaoCauHoiChiTietRequest,
  FileQuestion,
} from '@app/models';
import { KqSurveyCheckBox } from '@app/enums';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
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

  datas: any;

  @ViewChild('dtCt') tableCt!: Table;
  loading: boolean = true;
  selectedBaoCaoChiTiet!: BaoCaoCauHoiChiTiet[];
  dataChiTiet: BaoCaoCauHoiChiTiet[] = [];
  dataTotalRecords!: number;
  keyWord!: string;
  paging!: BaoCaoCauHoiChiTietRequest;
  lstTh: string[] = [];

  dataTableSurvey!:any;

  @ViewChild('dt') table!: Table; // Khi sử dụng p-table, sử dụng ViewChild để truy cập nó
  constructor(
    private formBuilder: FormBuilder,
    private periodSurveyService: PeriodSurveyService,
    private tableSurveyService: TableSurveyService,
    private unitTypeService: UnitTypeService,
    private config: PrimeNGConfig,
    private translateService: TranslateService,
    private messageService: MessageService,
    private baoCaoCauHoiService: BaoCaoCauHoiService,
    private route: ActivatedRoute,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    Utils.translate('vi', this.translateService, this.config);
    this.frmStatiscal = this.formBuilder.group({
      idDotKhaoSat: [],
      idBangKhaoSat: [],
      idLoaiHinhDonVi: [],
      ngayBatDau: [],
      ngayKetThuc: [],
    });
    this.loadPeriodSurvey();
    this.loadTableSurvey();
    this.loadUnitType();
    this.getVauleChar(this.frmStatiscal.value);
    //Nhận data từ bên bảng khảo sát
    debugger
    this.dataTableSurvey = this.baoCaoCauHoiService.getSharedData();
    this.frmStatiscal.controls['idDotKhaoSat'].setValue(parseInt(this.dataTableSurvey.idDotKhaoSat));
    this.frmStatiscal.controls['idBangKhaoSat'].setValue(parseInt(this.dataTableSurvey.id));
    this.frmStatiscal.controls['idLoaiHinhDonVi'].setValue(parseInt(this.dataTableSurvey.idLoaiHinh));
    // const ngayBatDauFormatted = this.datePipe.transform(this.dataTableSurvey.ngayBatDau, 'dd/MM/yyyy');
    // const ngayKetThucFormatted = this.datePipe.transform(this.dataTableSurvey.ngayKetThuc, 'dd/MM/yyyy');
    // this.frmStatiscal.controls['ngayBatDau'].setValue(ngayBatDauFormatted);
    // this.frmStatiscal.controls['ngayKetThuc'].setValue(ngayKetThucFormatted);
    this.getVauleChar(this.frmStatiscal.value);
    // this.getBaoCaoCauHoiChiTiet(this.paging);

  
    // this.getVauleChar(this.frmStatiscal.value);
    // this.route.params.subscribe((params:any) => {
    //   debugger
    //   this.frmStatiscal.patchValue({
    //     idDotKhaoSat: parseInt(params.idDotKhaoSat) || '',
    //     idBangKhaoSat: parseInt(params.id) || '', 
    //     idLoaiHinh: parseInt(params.idLoaiHinh) || '',
    //     ngayBatDau: moment(params.ngayBatDau).format('MM/DD/YYYY') || '',
    //   ngayKetThuc: moment(params.ngayKetThuc).format('MM/DD/YYYY') || '',
    //   });
    //   this.getVauleChar(this.frmStatiscal.value);
    //   this.getBaoCaoCauHoiChiTiet(this.paging);
    // });
  }

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      ...this.frmStatiscal.value,
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: '',
    };
    this.baoCaoCauHoiService.getBaoCaoCauHoiChiTiet(this.paging).subscribe({
      next: (res) => {
        this.lstTh = [];
        this.dataChiTiet = res.data;
        res.data[0]?.lstCauHoiCauTraLoi.map((x) => this.lstTh.push(x.cauHoi));
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
    this.getBaoCaoCauHoiChiTiet(this.paging);
  };

  getBaoCaoCauHoiChiTiet = (paging: BaoCaoCauHoiChiTietRequest) => {
    this.baoCaoCauHoiService.getBaoCaoCauHoiChiTiet(paging).subscribe({
      next: (res) => {
        this.lstTh = [];
        this.dataChiTiet = res.data;
        res.data[0].lstCauHoiCauTraLoi.map((x) => this.lstTh.push(x.cauHoi));
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

  countSpan() {
    return this.lstTh.length + 2;
  }

  trackByFn(index: number) {
    return index;
  }

  exportToExcel() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(
      this.table.el.nativeElement
    );
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, 'ThongKe.xlsx');
  }

  getVauleChar = (params: any) => {
    debugger
    this.baoCaoCauHoiService.getBaoCaoCauHoi(params).subscribe({
      next: (res) => {
        this.datas = res.listCauHoiTraLoi ?? [];
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
    if (this.frmStatiscal.value.ngayBatDau) {
      params.ngayBatDau = moment(this.frmStatiscal.value.ngayBatDau).format(
        'MM/DD/YYYY'
      );
    }
    if (this.frmStatiscal.value.ngayKetThuc) {
      params.ngayKetThuc = moment(this.frmStatiscal.value.ngayKetThuc).format(
        'MM/DD/YYYY'
      );
    }
    if (!params.idDotKhaoSat)
      Utils.messageError(this.messageService, `Vui lòng chọn đợt khảo sát!`);
    else if (!params.idBangKhaoSat)
      Utils.messageError(this.messageService, `Vui lòng chọn bảng khảo sát!`);
    else {
      this.getVauleChar(params);
      this.paging.keyword = this.keyWord;
      this.paging.idBangKhaoSat = params.idBangKhaoSat;
      this.paging.idDotKhaoSat = params.idDotKhaoSat;
      this.paging.idLoaiHinhDonVi = params.idLoaiHinh;
      this.paging.ngayBatDau = params.ngayBatDau;
      this.paging.ngayKetThuc = params.ngayKetThuc;
      this.getBaoCaoCauHoiChiTiet(this.paging);
    }
  };

  reset = () => {
    this.frmStatiscal.reset();
  };

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

  getDataKqBangMotLuaChon = (data: string) => {
    return data && JSON.parse(data) == KqSurveyCheckBox.value
      ? KqSurveyCheckBox.text
      : '';
  };

  getDataKqFile = (data: string): any[] => {
    return data && JSON.parse(data) ? JSON.parse(data) : [];
  };

  getDataKqFile2 = (data: string): any[] => {
    return data && JSON.parse(data) ? JSON.parse(data) : [];
  };

  getDataKq = (data: string) => {
    try {
      return data && JSON.parse(data) ? JSON.parse(data) : '';
    } catch {
      return data;
    }
  };

  downloadFileBase64 = (file: FileQuestion) => {
    Utils.downloadFileBase64(file);
  };
}
