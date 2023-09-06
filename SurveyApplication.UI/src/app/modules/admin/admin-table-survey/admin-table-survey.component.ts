import { Component, ViewChild } from '@angular/core';
import {
  CauHoi,
  CreateUpdateBangKhaoSat,
  Paging,
  TableSurvey,
} from '@app/models';
import { Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import {
  UnitTypeService,
  TableSurveyService,
  PeriodSurveyService,
  CauHoiService,
} from '@app/services';
import Utils from '@app/helpers/utils';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-admin-table-survey',
  templateUrl: './admin-table-survey.component.html',
  styleUrls: ['./admin-table-survey.component.css'],
})
export class AdminTableSurveyComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  selectedTableSurvey!: TableSurvey[];
  datas: TableSurvey[] = [];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  // first: number = 0;
  // TotalCount: number = 0;
  // pageIndex: number = 1;
  // pageSize: number = 5;
  // keyword: string = '';

  showadd: boolean = false;
  formTableSurvey!: FormGroup;
  MaLoaiHinh!: string;
  IdLoaiHinh!: string;

  DSLoaiHinh: any[] = [];
  DSDotKhaoSat: any[] = [];
  showHeader: boolean = true;

  visible: boolean = false;

  @ViewChild('dtq') tableQ!: Table;
  loadingCauHoi: boolean = true;
  selectedCauHoi!: CauHoi[];
  datasCauHoi: CauHoi[] = [];
  pagingCauHoi!: Paging;
  dataTotalRecordsCauHoi!: number;
  keyWordCauHoi!: string;
  visibleCauHoi: boolean = false;

  constructor(
    private FormBuilder: FormBuilder,
    private TableSurveyService: TableSurveyService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private periodSurveyService: PeriodSurveyService,
    private unitTypeService: UnitTypeService,
    private cauHoiService: CauHoiService,
    private datePipe: DatePipe
  ) {}
  ngOnInit() {
    this.LoadUnitType();
    this.LoadPeriodSurvey();
    this.formTableSurvey = this.FormBuilder.group({
      id: [''],
      maBangKhaoSat: [''],
      idLoaiHinh: ['', Validators.required],
      idDotKhaoSat: ['', Validators.required],
      tenBangKhaoSat: ['', Validators.required],
      moTa: ['', Validators.required],
      ngayBatDau: ['', Validators.required],
      ngayKetThuc: ['', Validators.required],
      bangKhaoSatCauHoi: this.FormBuilder.array([]),
    });
  }

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: '',
      orderBy: event.sortField
        ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
        : '',
    };
    this.TableSurveyService.getByCondition(this.paging).subscribe({
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

  onSubmitSearch = () => {
    this.paging.keyword = this.keyWord;
    this.TableSurveyService.getByCondition(this.paging).subscribe({
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

  loadListLazyCauHoi = (event: any) => {
    this.loadingCauHoi = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.pagingCauHoi = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: '',
      orderBy: event.sortField
        ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
        : '',
    };
    this.cauHoiService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.datasCauHoi = res.data;
        this.dataTotalRecordsCauHoi = res.totalFilter;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loadingCauHoi = false;
      },
      complete: () => {
        this.loadingCauHoi = false;
      },
    });
  };

  onSubmitSearchCauHoi = () => {
    this.pagingCauHoi.keyword = this.keyWordCauHoi;
    this.cauHoiService.getByCondition(this.pagingCauHoi).subscribe({
      next: (res) => {
        this.datasCauHoi = res.data;
        this.dataTotalRecordsCauHoi = res.totalFilter;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loadingCauHoi = false;
      },
      complete: () => {
        this.loadingCauHoi = false;
      },
    });
  };

  CloseModal() {
    this.visible = false;
  }

  LoadUnitType() {
    this.unitTypeService.getAll().subscribe((data) => {
      this.DSLoaiHinh = data; // Lưu dữ liệu vào danh sách
    });
  }

  LoadPeriodSurvey() {
    this.periodSurveyService.getAll().subscribe((data) => {
      this.DSDotKhaoSat = data; // Lưu dữ liệu vào danh sách
    });
  }

  toggleHeader() {
    this.showHeader = !this.showHeader; // Đảo ngược giá trị của biến showHeader
  }

  Add() {
    this.showadd = true;
    this.visible = !this.visible;
  }

  Edit(data: any) {
    this.showadd = false;
    this.visible = !this.visible;
    this.TableSurveyService.getById<CreateUpdateBangKhaoSat>(data.id).subscribe(
      {
        next: (res) => {
          let k = Object.keys(res);
          let v = Object.values(res);
          Utils.setValueForm(this.formTableSurvey, k, v);
          const ngayBatDauFormatted = this.datePipe.transform(
            data.ngayBatDau,
            'yyyy-MM-dd'
          );
          const ngayKetThuFormatted = this.datePipe.transform(
            data.ngayKetThuc,
            'yyyy-MM-dd'
          );
          this.formTableSurvey.controls['ngayBatDau'].setValue(
            ngayBatDauFormatted
          );
          this.formTableSurvey.controls['ngayKetThuc'].setValue(
            ngayKetThuFormatted
          );

          res.bangKhaoSatCauHoi?.forEach((el, i) => {
            const newItem = this.FormBuilder.group({
              id: 0,
              idCauHoi: el.id,
              priority: i,
              isRequired: false,
              maCauHoi: el.maCauHoi,
              tieuDe: el.tieuDe,
            });
            this.lstBangKhaoSatCauHoi.push(newItem);
          });
        },
        error: (e) => Utils.messageError(this.messageService, e.message),
      }
    );
  }

  Save() {
    if (this.showadd) {
      this.SaveAdd();
    } else {
      this.SaveEdit();
    }
  }

  SaveAdd() {
    if (this.formTableSurvey.valid) {
      const ObjTableSurvey = this.formTableSurvey.value;
      this.TableSurveyService.create(ObjTableSurvey).subscribe({
        next: (res) => {
          if (res != null) {
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Thêm thành Công !',
            });
            this.table.reset();
            this.formTableSurvey.reset();
            this.visible = false;
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Lỗi',
              detail: 'Lỗi vui Lòng kiểm tra lại !',
            });
          }
        },
      });
    }
  }

  SaveEdit() {
    this.visible = !this.visible;
    const objTableSurvey = this.formTableSurvey.value;
    this.TableSurveyService.update(objTableSurvey).subscribe({
      next: (res: any) => {
        if (res.success) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.table.reset();
          this.formTableSurvey.reset();
          this.visible = false;
          console.log(res);
        }
      },
    });
  }

  Delete(data: any) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        debugger;
        this.TableSurveyService.delete(data.id).subscribe((res: any) => {
          debugger;
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.table.reset();
          this.formTableSurvey.reset();
        });
      },
    });
  }

  get lstBangKhaoSatCauHoi(): FormArray {
    return this.formTableSurvey.get('bangKhaoSatCauHoi') as FormArray;
  }

  showSialogCh = () => {
    this.selectedCauHoi = [];
    this.visibleCauHoi = true;
  };

  addItem() {
    this.selectedCauHoi.forEach((el, i) => {
      const newItem = this.FormBuilder.group({
        id: 0,
        idCauHoi: el.id,
        priority: i,
        isRequired: false,
        maCauHoi: el.maCauHoi,
        tieuDe: el.tieuDe,
      });
      this.lstBangKhaoSatCauHoi.push(newItem);
    });
    this.visibleCauHoi = false;
  }

  deleteItem(index: number) {
    this.lstBangKhaoSatCauHoi.removeAt(index);
  }
}
