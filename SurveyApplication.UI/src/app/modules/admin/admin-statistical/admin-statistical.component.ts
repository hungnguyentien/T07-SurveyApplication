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

import { Workbook, Worksheet } from 'exceljs';
import * as fs from 'file-saver';
import { Table } from 'primeng/table';
import {
  BaoCaoCauHoiChiTiet,
  BaoCaoCauHoiChiTietRequest,
  BaoCaoCauHoiRequest,
  DoiTuongThamGiaKs,
  FileQuestion,
  StgFile,
} from '@app/models';
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
  datas: any;

  @ViewChild('dtCt') tableCt!: Table;
  loading: boolean = true;
  selectedBaoCaoChiTiet!: BaoCaoCauHoiChiTiet[];
  dataChiTiet: BaoCaoCauHoiChiTiet[] = [];
  dataTotalRecords!: number;
  keyWord!: string;
  paging!: BaoCaoCauHoiChiTietRequest;
  lstTh: string[] = [];

  dataTableSurvey!: any;

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
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    // Utils.translate('vi', this.translateService, this.config);
    this.frmStatiscal = this.formBuilder.group({
      idDotKhaoSat: [],
      idBangKhaoSat: [],
      idLoaiHinhDonVi: [],
      ngayBatDau: [],
      ngayKetThuc: [],
    });
    this.loadPeriodSurvey();
    this.loadUnitType();
    this.loadTableSurvey();
    this.getVauleChar(this.frmStatiscal.value);
    //Nhận data từ bên bảng khảo sát
    this.dataTableSurvey = this.baoCaoCauHoiService.getSharedData();
    if (this.dataTableSurvey) {
      this.frmStatiscal.controls['idDotKhaoSat'].setValue(parseInt(this.dataTableSurvey.idDotKhaoSat)) 
      this.frmStatiscal.controls['idBangKhaoSat'].setValue(parseInt(this.dataTableSurvey.id));
      this.frmStatiscal.controls['idLoaiHinhDonVi'].setValue(parseInt(this.dataTableSurvey.idLoaiHinh));

      const ngayBatDauFormatted = this.datePipe.transform(this.dataTableSurvey.ngayBatDau,'dd/MM/yyyy');
      const ngayKetThucFormatted = this.datePipe.transform(this.dataTableSurvey.ngayKetThuc,'dd/MM/yyyy');
      this.frmStatiscal.controls['ngayBatDau'].setValue(ngayBatDauFormatted);
      this.frmStatiscal.controls['ngayKetThuc'].setValue(ngayKetThucFormatted);
      let params: BaoCaoCauHoiRequest = {
        ...this.frmStatiscal.value,
      };
      this.loadTableSurvey();
      this.getVauleChar(params);
    }
  }

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    let frmValue = this.frmStatiscal.value;
    let ngayBatDau = this.dataTableSurvey?.ngayBatDau
      ? moment(this.dataTableSurvey?.ngayBatDau).format('YYYY-MM-DD')
      : '';
    let ngayKetThuc = this.dataTableSurvey?.ngayKetThuc
      ? moment(this.dataTableSurvey?.ngayKetThuc).format('YYYY-MM-DD')
      : '';
    this.paging = {
      ...frmValue,
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.keyWord,
      ngayBatDau: ngayBatDau,
      ngayKetThuc: ngayKetThuc,
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
        res.data &&
          res.data.length > 0 &&
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

  getVauleChar = (params: BaoCaoCauHoiRequest) => {
    this.baoCaoCauHoiService.getBaoCaoCauHoi(params).subscribe({
      next: (res) => {
        this.datas = res.listCauHoiTraLoi ?? [];
        this.setChar(
          [res.countDonViMoi, res.countDonViTraLoi],
          res.lstDoiTuongThamGiaKs
        );
      },
    });
  };

  setChar = (doughnutData: number[], barData: DoiTuongThamGiaKs[]) => {
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
      labels: barData.map((x) => x.ten),
      datasets: [
        {
          label: 'Đối tượng',
          backgroundColor: documentStyle.getPropertyValue('--blue-500'),
          borderColor: documentStyle.getPropertyValue('--blue-500'),
          data: barData.map((x) => x.soLuong),
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
    let frmValue = this.frmStatiscal.value;
    let ngayBatDau = frmValue.ngayBatDau
      ? moment(frmValue.ngayBatDau, 'DD/MM/YYYY').format('YYYY-MM-DD')
      : '';
    let ngayKetThuc = frmValue.ngayKetThuc
      ? moment(frmValue.ngayKetThuc, 'DD/MM/YYYY').format('YYYY-MM-DD')
      : '';
    if (!frmValue.idDotKhaoSat)
      Utils.messageError(this.messageService, `Vui lòng chọn đợt khảo sát!`);
    else if (!frmValue.idBangKhaoSat)
      Utils.messageError(this.messageService, `Vui lòng chọn bảng khảo sát!`);
    else {
      let params: BaoCaoCauHoiRequest = {
        ...this.frmStatiscal.value,
      };
      params.ngayBatDau = params.ngayBatDau
        ? moment(params.ngayBatDau, 'DD/MM/YYYY').format('DD/MM/YYYY')
        : '';
      params.ngayKetThuc = params.ngayKetThuc
        ? moment(params.ngayKetThuc, 'DD/MM/YYYY').format('DD/MM/YYYY')
        : '';
      this.getVauleChar(params);
      this.paging.keyword = this.keyWord;
      this.paging.idBangKhaoSat = frmValue.idBangKhaoSat;
      this.paging.idDotKhaoSat = frmValue.idDotKhaoSat;
      this.paging.idLoaiHinhDonVi = frmValue.idLoaiHinh;
      this.paging.ngayBatDau = ngayBatDau;
      this.paging.ngayKetThuc = ngayKetThuc;
      this.getBaoCaoCauHoiChiTiet(this.paging);
    }
  };

  reset = () => {
    this.frmStatiscal.reset();
  };

  loadPeriodSurvey() {
    this.periodSurveyService.getAll().subscribe((data:any) => {
      this.LstDotKhaoSat = data;
    });
  }

  loadTableSurvey() {
    const selectedValue = this.frmStatiscal.get('idDotKhaoSat')?.value;
    this.periodSurveyService.getDotKhaoSatByDotKhaoSat(selectedValue).subscribe((data:any) => {
      this.LstBangKhaoSat = data.data;
      this.frmStatiscal.controls['idBangKhaoSat'].setValue(parseInt(this.dataTableSurvey.id));
    });
  }

  loadUnitType() {
    this.unitTypeService.getAll().subscribe((data) => {
      this.LstLoaiHinhDv = data;
    });
  }

  getDataKqFile = (data: string): any[] => {
    return data && JSON.parse(data) ? JSON.parse(data) : [];
  };

  getDataKq = (data: string) => {
    try {
      return data && JSON.parse(data) ? JSON.parse(data) : '';
    } catch {
      return data;
    }
  };

  // downloadFileBase64 = (file: FileQuestion) => {
  //   Utils.downloadFileBase64(file);
  // };

  downloadFile(file: FileQuestion) {
    this.baoCaoCauHoiService.downloadFile(file.idFile).subscribe(
      (data: StgFile) => {
        const byteCharacters = atob(data.fileContents);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
          byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: data.contentType });

        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = data.fileName;
        document.body.appendChild(a);

        a.click();
        window.URL.revokeObjectURL(url);
      },
      (error) => {
        console.error('Error downloading file:', error);
      }
    );
  }

  // downloadFileBase64 = (file: FileQuestion) => {
  //   debugger
  //   // let idFile = file.idFile;
  //   const id = 1;
  //   this.baoCaoCauHoiService.downloadFile(id).subscribe({
  //     next: (res: any) => {
  //       // let blob = new Blob([bytes], { type: type });
  //       // let downloadUrl = URL.createObjectURL(blob);
  //       let downloadUrl = res.physicalPath;

  //       let a = document.createElement('a');
  //       a.href = downloadUrl;
        
  //       a.download = res.fileName;
  //       document.body.appendChild(a);
  //       a.click();
  //       setTimeout(function () {
  //         URL.revokeObjectURL(downloadUrl);
  //       }, 100);
  //     },
  //   });
  // };

  exportExcel() {
    let workbook = new Workbook();
    let worksheet = workbook.addWorksheet('ProductSheet');
    
    // Định dạng cho header và độ rộng của các cột
    worksheet.columns = [
      { header: 'STT', key: 'stt', width: 10 },
      { header: 'Câu hỏi - Câu trả lời', key: 'cauhoicautraloi', width: 70 },
      { header: 'Số lượt chọn', key: 'soluotchon', width: 15 },
      { header: 'Tỷ lệ', key: 'tyle', width: 15 },
    ];

    // Thêm dữ liệu
    let x = 2;
    let y = 2;
    this.datas.forEach((e: any, i: number) => {
      if (e && e.cauHoiTraLoi && e.cauHoiTraLoi.length > 0) {
        worksheet.addRow({ stt: i + 1, cauhoicautraloi: e.tenCauHoi, soluotchon: '', tyle: '' });
        worksheet.mergeCells(`B${x}:D${x}`);
        x += e.cauHoiTraLoi.length + 1;

        e.cauHoiTraLoi.forEach((element: any) => {
          if (element.length > 1) {
            worksheet.addRow({ stt: '', cauhoicautraloi: element.cauTraLoi, soluotchon: element.soLuotChon, tyle: parseFloat(element.tyLe.toFixed(4)) + ' %' });
          } else {
            worksheet.addRow({ stt: '', cauhoicautraloi: element.cauTraLoi, soluotchon: element.soLuotChon, tyle: parseFloat(element.tyLe.toFixed(4)) + ' %' });
          }
        });
        worksheet.mergeCells(`A${y}:A${y + e.cauHoiTraLoi.length}`);
        y += e.cauHoiTraLoi.length + 1;
      }
    });

    // Định dạng cho các ô
    worksheet.eachRow((row, rowNumber) => {
      row.alignment = { wrapText: true };
      row.eachCell((cell, colNumber) => {
        cell.alignment = { vertical: 'middle', horizontal: 'center', wrapText: true };
        cell.border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } };
        row.height = 30;
        
        if (rowNumber === 1) {
          cell.font = { bold: true };
          cell.fill = { type: 'pattern', pattern: 'solid', fgColor: { argb: 'FF808080' } };
          row.height = 40;
        }
      });
    });

    worksheet.getColumn('cauhoicautraloi').alignment = { vertical: 'middle', horizontal: 'left', wrapText: true };

    workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, 'ProductData.xlsx');
    });
  }
}