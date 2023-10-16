import { Component, ViewChild } from '@angular/core';
import {
  BangKhaoSatCauHoi,
  CauHoi,
  CreateGuiEmail,
  CreateUpdateBangKhaoSat,
  DonVi,
  Paging,
  TableSurvey,
} from '@app/models';
import {
  Validators,
  FormGroup,
  FormBuilder,
  FormArray,
  AbstractControl,
} from '@angular/forms';
import {
  ConfirmationService,
  MessageService,
  PrimeNGConfig,
} from 'primeng/api';
import { Table } from 'primeng/table';
import {
  UnitTypeService,
  TableSurveyService,
  PeriodSurveyService,
  CauHoiService,
  ObjectSurveyService,
  GuiEmailService,
  BaoCaoCauHoiService,
} from '@app/services';
import Utils from '@app/helpers/utils';
import { DatePipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { BksTrangThai } from '@app/enums';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { coerceStringArray } from '@angular/cdk/coercion';

import { TranslateService } from '@ngx-translate/core';

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
  originalDatas: any[] = [];


  filteredDatas: any[] = []; // Dữ liệu sau khi lọc
  filterText: string = '';


  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  detaiDatas: any[] = [];
  id!: number;
  confirmationHeader: string = '';
  showadd: boolean = false;
  formTableSurvey!: FormGroup;
  MaLoaiHinh!: string;
  IdLoaiHinh!: string;
  MaBangKhaoSat!: string;

  DSLoaiHinh: any[] = [];
  DSDotKhaoSat: any[] = [];
  showHeader: boolean = true;
  Gettrangthai!: number;
  GetMaBangKhaoSat!: number;
  form: FormGroup = new FormGroup({});

  searchText = new FormControl('');
  serverError: string = '';

  visible: boolean = false;
  visibleDetail: boolean = false;
  checkBtnDetail: boolean = false;
  modalTitle: string = '';

  @ViewChild('dtq') tableQ!: Table;
  loadingCauHoi: boolean = true;
  selectedCauHoi!: CauHoi[];
  datasCauHoi: CauHoi[] = [];
  pagingCauHoi!: Paging;
  dataTotalRecordsCauHoi!: number;
  keyWordCauHoi!: string;
  visibleCauHoi: boolean = false;

  frmGuiEmail!: FormGroup;

  visibleGuiEmail: boolean = false;
  lstDonVi!: DonVi[];
  selectedDonVi!: number[];
  selectedLoaiHinh!: number;
  lstIdDonViError: boolean = false;

  iPanelTitle!: number;
  minDate!: Date;
  minDate2!: Date;
  reon!: boolean;
  currentDate!: Date;
  constructor(
    private router: Router,
    private FormBuilder: FormBuilder,
    private TableSurveyService: TableSurveyService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private periodSurveyService: PeriodSurveyService,
    private unitTypeService: UnitTypeService,
    private cauHoiService: CauHoiService,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private objectSurveyService: ObjectSurveyService,
    private guiEmailService: GuiEmailService,
    private baocaocauhoiservice: BaoCaoCauHoiService,
    private config: PrimeNGConfig,
    private translateService: TranslateService

  ) {
    this.currentDate = new Date();
  }

  ngOnInit() {

    // Utils.translate('vi', this.translateService, this.config);
    this.form = this.fb.group({
      searchText: [''], // Khởi tạo FormControl searchText
      idDotKhaoSat: [''], // Khởi tạo FormControl idDotKhaoSat
    });
    this.LoadUnitType();
    this.LoadPeriodSurvey();
    this.formTableSurvey = this.FormBuilder.group(
      {
        id: [''],
        maBangKhaoSat: [{ value: this.MaBangKhaoSat, disabled: true }],
        idLoaiHinh: ['', Validators.required],
        idDotKhaoSat: ['', Validators.required],
        tenBangKhaoSat: ['', Validators.required],
        moTa: [''],
        ngayBatDau: ['', Validators.required],
        ngayKetThuc: ['', Validators.required],
        bangKhaoSatCauHoi: this.FormBuilder.array([]),
        bangKhaoSatCauHoiGroup: this.FormBuilder.array([]),
      },
      { validator: this.dateRangeValidator }
    );
    this.minDate = new Date();
    this.minDate2 = new Date(1900, 0, 1);
  }
  checkDate(): Date {
    let currentDate = new Date();
    this.modalTitle == 'Thêm mới bảng khảo sát' ? currentDate = this.minDate : currentDate = this.minDate2
    return currentDate
  }

  dateRangeValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    const startDate = control.get('ngayBatDau')?.value;
    const endDate = control.get('ngayKetThuc')?.value;

    if (startDate && endDate && startDate > endDate) {
      return { dateRangeError: true };
    }

    return null;
  }


  LoadUnitType() {
    this.unitTypeService.getAll().subscribe((data) => {
      this.DSLoaiHinh = data;
    });
  }

  LoadPeriodSurvey() {
    const code = this.selectedLoaiHinh;
    this.periodSurveyService.getDotKhaoSatByLoaiHinh(code ?? 0).subscribe((data) => {
      this.DSDotKhaoSat = data;
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


  //#region Loadding table and search

  // loadListLazy = (event: any) => {
  //   this.loading = true;
  //   let pageSize = event.rows;
  //   let pageIndex = event.first / pageSize + 1;
  //   this.paging = {
  //     pageIndex: pageIndex,
  //     pageSize: pageSize,
  //     keyword: this.keyWord,
  //     orderBy: event.sortField
  //       ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
  //       : '',
  //   };
  //   this.TableSurveyService.getByCondition(this.paging).subscribe({
  //     next: (res) => {
  //       this.datas = res.data;
  //       this.dataTotalRecords = res.totalFilter;
  //     },
  //     error: (e) => {
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

    this.TableSurveyService.getByCondition(this.paging).subscribe({
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
    this.TableSurveyService.getByCondition(this.paging).subscribe({
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
    this.cauHoiService.getByCondition(this.pagingCauHoi).subscribe({
      next: (res) => {
        this.datasCauHoi = res.data;
        this.dataTotalRecordsCauHoi = res.totalFilter;
      },
      error: (e) => {
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
        this.loadingCauHoi = false;
      },
      complete: () => {
        this.loadingCauHoi = false;
      },
    });
  };
  //#endregion

  //#region CRUD

  detail(data: any) {
    this.checkBtnDetail = true
    this.visible = !this.visible;
    this.modalTitle = 'Chi tiết bảng khảo sát';
    this.formTableSurvey.disable();
    this.reon = true;

    this.lstBangKhaoSatCauHoiGroup.disable();


    this.lstBangKhaoSatCauHoi.clear();

    this.lstBangKhaoSatCauHoiGroup.clear();



    this.TableSurveyService.getById<CreateUpdateBangKhaoSat>(data.id).subscribe(
      {
        next: (res) => {
          let k = Object.keys(res);
          let v = Object.values(res);
          Utils.setValueForm(this.formTableSurvey, k, v);
          const ngayBatDauFormatted = this.datePipe.transform(
            data.ngayBatDau,
            'dd/MM/yyyy'
          );
          const ngayKetThuFormatted = this.datePipe.transform(
            data.ngayKetThuc,
            'dd/MM/yyyy'
          );
          this.formTableSurvey.controls['ngayBatDau'].setValue(
            ngayBatDauFormatted
          );
          this.formTableSurvey.controls['ngayKetThuc'].setValue(
            ngayKetThuFormatted
          );
          let bangKhaoSatCauHoiGroup: BangKhaoSatCauHoi[] = [];
          res.bangKhaoSatCauHoi?.forEach((el, i) => {
            if (el.panelTitle) {
              const newItem = {
                id: el.id,
                idCauHoi: el.idCauHoi,
                priority: i,
                isRequired: el.isRequired,
                panelTitle: el.panelTitle,
                maCauHoi: el.maCauHoi,
                tieuDe: el.tieuDe,
              };
              bangKhaoSatCauHoiGroup.push(newItem);
            } else {
              const idCauHoi = el.idCauHoi;
              // Kiểm tra xem idCauHoi đã tồn tại trong lstBangKhaoSatCauHoi chưa
              const idCauHoiExists = this.lstBangKhaoSatCauHoi.controls.some(
                (control) => {
                  const idCauHoiControl = control.get('idCauHoi');
                  return idCauHoiControl
                    ? idCauHoiControl.value === idCauHoi
                    : false;
                }
              );
              if (!idCauHoiExists) {
                // Nếu idCauHoi chưa tồn tại, thêm mới
                const newItem = this.FormBuilder.group({
                  id: 0,
                  idCauHoi: idCauHoi,
                  priority: i,
                  isRequired: el.isRequired,
                  maCauHoi: el.maCauHoi,
                  tieuDe: el.tieuDe,
                });
                this.lstBangKhaoSatCauHoi.push(newItem);
              }
            }
          });
          // Lọc bỏ các giá trị null sau khi thêm mới
          for (let i = this.lstBangKhaoSatCauHoi.length - 1; i >= 0; i--) {
            const control = this.lstBangKhaoSatCauHoi.at(i).get('idCauHoi');
            if (control !== null) {
              const idCauHoi = control.value;
              if (idCauHoi === null) {
                this.lstBangKhaoSatCauHoi.removeAt(i);
              }
            }
          }

          if (bangKhaoSatCauHoiGroup.length > 0) {
            const groupTitle = bangKhaoSatCauHoiGroup
              .map((x) => x.panelTitle)
              .filter(Utils.onlyUnique);
            groupTitle.forEach((x, i) => {
              const newItem = this.FormBuilder.group({
                panelTitle: [x, Validators.required],
                bangKhaoSatCauHoi: this.FormBuilder.array<BangKhaoSatCauHoi>(
                  []
                ),
              });
              this.lstBangKhaoSatCauHoiGroup.push(newItem);
              bangKhaoSatCauHoiGroup
                .filter((g) => g.panelTitle == x)
                .forEach((el) => {
                  const newItem = this.FormBuilder.group<BangKhaoSatCauHoi>({
                    id: 0,
                    idCauHoi: el.idCauHoi,
                    priority: i,
                    isRequired: el.isRequired,
                    panelTitle: el.panelTitle,
                    maCauHoi: el.maCauHoi,
                    tieuDe: el.tieuDe,
                  });
                  this.lstBangKhaoSatCauHoiGroupItem(i).push(newItem);
                });
            });
          }
        },
      }
    );

  }

  Add() {
    this.formTableSurvey.reset();
    this.modalTitle = 'Thêm mới bảng khảo sát';
    this.formTableSurvey.enable();
    this.formTableSurvey.get('maBangKhaoSat')?.disable();
    this.TableSurveyService.generateMaBangKhaoSat().subscribe({
      next: (res: any) => {
        this.formTableSurvey.controls['maBangKhaoSat'].setValue(res.maBangKhaoSat);
        this.MaBangKhaoSat = res.maBangKhaoSat;
      },
    });
    this.checkBtnDetail = false;
    this.showadd = true;
    this.reon = false;
    this.visible = !this.visible;
    this.lstBangKhaoSatCauHoi.clear();
    this.lstBangKhaoSatCauHoiGroup.clear();
  }
  Edit(data: any) {
    this.showadd = false;
    this.reon = false;
    this.checkBtnDetail = false;
    this.modalTitle = 'Cập nhật bảng khảo sát';
    this.visible = !this.visible;
    this.Gettrangthai = data.trangThai;
    this.GetMaBangKhaoSat = data.maBangKhaoSat;
    this.formTableSurvey.enable();
    this.formTableSurvey.get('maBangKhaoSat')?.disable();
    this.lstBangKhaoSatCauHoi.clear();
    this.lstBangKhaoSatCauHoiGroup.clear();
    this.TableSurveyService.getById<CreateUpdateBangKhaoSat>(data.id).subscribe(
      {
        next: (res) => {
          let k = Object.keys(res);
          let v = Object.values(res);
          Utils.setValueForm(this.formTableSurvey, k, v);
          const ngayBatDauFormatted = this.datePipe.transform(
            data.ngayBatDau,
            'dd/MM/yyyy'
          );
          const ngayKetThuFormatted = this.datePipe.transform(
            data.ngayKetThuc,
            'dd/MM/yyyy'
          );
          this.formTableSurvey.controls['ngayBatDau'].setValue(
            ngayBatDauFormatted
          );
          this.formTableSurvey.controls['ngayKetThuc'].setValue(
            ngayKetThuFormatted
          );
          let bangKhaoSatCauHoiGroup: BangKhaoSatCauHoi[] = [];
          res.bangKhaoSatCauHoi?.forEach((el, i) => {
            if (el.panelTitle) {
              const newItem = {
                id: el.id,
                idCauHoi: el.idCauHoi,
                priority: i,
                isRequired: el.isRequired,
                panelTitle: el.panelTitle,
                maCauHoi: el.maCauHoi,
                tieuDe: el.tieuDe,
              };
              bangKhaoSatCauHoiGroup.push(newItem);
            } else {
              const idCauHoi = el.idCauHoi;
              // Kiểm tra xem idCauHoi đã tồn tại trong lstBangKhaoSatCauHoi chưa
              const idCauHoiExists = this.lstBangKhaoSatCauHoi.controls.some(
                (control) => {
                  const idCauHoiControl = control.get('idCauHoi');
                  return idCauHoiControl
                    ? idCauHoiControl.value === idCauHoi
                    : false;
                }
              );
              if (!idCauHoiExists) {
                // Nếu idCauHoi chưa tồn tại, thêm mới
                const newItem = this.FormBuilder.group({
                  id: 0,
                  idCauHoi: idCauHoi,
                  priority: i,
                  isRequired: el.isRequired,
                  maCauHoi: el.maCauHoi,
                  tieuDe: el.tieuDe,
                });
                this.lstBangKhaoSatCauHoi.push(newItem);
              }
            }
          });
          // Lọc bỏ các giá trị null sau khi thêm mới
          for (let i = this.lstBangKhaoSatCauHoi.length - 1; i >= 0; i--) {
            const control = this.lstBangKhaoSatCauHoi.at(i).get('idCauHoi');
            if (control !== null) {
              const idCauHoi = control.value;
              if (idCauHoi === null) {
                this.lstBangKhaoSatCauHoi.removeAt(i);
              }
            }
          }

          if (bangKhaoSatCauHoiGroup.length > 0) {
            const groupTitle = bangKhaoSatCauHoiGroup
              .map((x) => x.panelTitle)
              .filter(Utils.onlyUnique);
            groupTitle.forEach((x, i) => {
              const newItem = this.FormBuilder.group({
                panelTitle: [x, Validators.required],
                bangKhaoSatCauHoi: this.FormBuilder.array<BangKhaoSatCauHoi>(
                  []
                ),
              });
              this.lstBangKhaoSatCauHoiGroup.push(newItem);
              bangKhaoSatCauHoiGroup
                .filter((g) => g.panelTitle == x)
                .forEach((el) => {
                  const newItem = this.FormBuilder.group<BangKhaoSatCauHoi>({
                    id: 0,
                    idCauHoi: el.idCauHoi,
                    priority: i,
                    isRequired: el.isRequired,
                    panelTitle: el.panelTitle,
                    maCauHoi: el.maCauHoi,
                    tieuDe: el.tieuDe,
                  });
                  this.lstBangKhaoSatCauHoiGroupItem(i).push(newItem);
                });
            });
          }

          console.log('Danh sách sau khi xử lý:', this.lstBangKhaoSatCauHoi);
        },
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
      ObjTableSurvey['maBangKhaoSat'] = this.MaBangKhaoSat;
      ObjTableSurvey.ngayBatDau = Utils.plusDate(ObjTableSurvey.ngayBatDau, 'DD/MM/YYYY');
      ObjTableSurvey.ngayKetThuc = Utils.plusDate(ObjTableSurvey.ngayKetThuc, 'DD/MM/YYYY');
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
          }
        },

        error: (e) => {
          const errorMessage = e.errorMessage;
          Utils.messageError(this.messageService, errorMessage);
        },
      });
    }
  }

  SaveEdit() {
    const objTableSurvey = this.formTableSurvey.value;
    objTableSurvey.ngayBatDau = Utils.plusDate(objTableSurvey.ngayBatDau, 'DD/MM/YYYY');
    objTableSurvey.ngayKetThuc = Utils.plusDate(objTableSurvey.ngayKetThuc, 'DD/MM/YYYY');
    objTableSurvey['maBangKhaoSat'] = this.GetMaBangKhaoSat;
    objTableSurvey['trangThai'] = this.Gettrangthai;
    this.TableSurveyService.update(objTableSurvey).subscribe({
      next: (res: any) => {
        if (res) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.table.reset();
          this.formTableSurvey.reset();
          this.visible = false;
        }
      },
      error: (e) => {
        const errorMessage = e.errorMessage;
        Utils.messageError(this.messageService, errorMessage);
      },
      // error: (e: HttpErrorResponse | any) => {
      //   if (e instanceof HttpErrorResponse) {
      //     if (e.error && Array.isArray(e.error) && e.error.length > 0) {
      //       const errorMessage = e.error[0];
      //       Utils.messageError(this.messageService, errorMessage);
      //     }
      //   } else if (typeof e === 'object' && 'errorMessage' in e) {
      //     // Trường hợp e là một đối tượng chứa errorMessage
      //     const errorMessage = e.errorMessage;
      //     Utils.messageError(this.messageService, errorMessage);
      //   } else {
      //     // Xử lý lỗi mặc định nếu kiểu dữ liệu không xác định
      //     Utils.messageError(this.messageService, 'Lỗi không xác định.');
      //   }
      // },
    });
  }

  Delete(data: any) {
    this.confirmationHeader = 'Xác nhận xoá bảng khảo sát';
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.TableSurveyService.delete(data.id).subscribe((res: any) => {
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

  ToggleStatus(rowData: TableSurvey) {
    this.confirmationHeader = 'Xác nhận thực hiện thay đổi trạng thái không';
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn thực hiện không ?',
      header: 'trangthai',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        if (rowData.trangThai === BksTrangThai.DangKhaoSat) {
          this.TableSurveyService.update({
            id: rowData.id,
            maBangKhaoSat: rowData.maBangKhaoSat,
            idDotKhaoSat: rowData.idDotKhaoSat,
            idLoaiHinh: rowData.idLoaiHinh,
            tenBangKhaoSat: rowData.tenBangKhaoSat,
            moTa: rowData.moTa,
            ngayBatDau: rowData.ngayBatDau,
            ngayKetThuc: rowData.ngayKetThuc,
            trangThai: BksTrangThai.TamDung,
          }).subscribe((res) => {
            if (res.success === true) {
              this.table.reset();
              this.messageService.add({
                severity: 'success',
                summary: 'Thành Công',
                detail: 'Thực Hiện Thành Công !',
              });
            } else {
              console.log('Cập nhật không thành công', res.message);
            }
          });
        } else if (rowData.trangThai === BksTrangThai.TamDung) {
          this.TableSurveyService.update({
            id: rowData.id,
            maBangKhaoSat: rowData.maBangKhaoSat,
            idDotKhaoSat: rowData.idDotKhaoSat,
            idLoaiHinh: rowData.idLoaiHinh,
            tenBangKhaoSat: rowData.tenBangKhaoSat,
            moTa: rowData.moTa,
            ngayBatDau: rowData.ngayBatDau,
            ngayKetThuc: rowData.ngayKetThuc,
            trangThai: BksTrangThai.DangKhaoSat,
          }).subscribe((res) => {
            if (res.success === true) {
              this.table.reset();
              this.messageService.add({
                severity: 'success',
                summary: 'Thành Công',
                detail: 'Thực Hiện Thành Công !',
              });
            } else {
              console.log('Cập nhật không thành công', res.message);
            }
          });
        }
      },
      reject: () => {
        console.log('Đã từ chối');
      },
    });
  }

  confirmDeleteMultiple() {
    let ids: number[] = [];
    this.selectedTableSurvey.forEach((el) => {
      ids.push(el.id);
    });

    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} bảng khảo sát này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.TableSurveyService.deleteMultiple(ids).subscribe({
          next: (res: any) => {
            if (res.success == false) {
              Utils.messageError(this.messageService, res.message);
            } else {
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} bảng khảo sát thành công!`
              );
              this.selectedTableSurvey = [];
            }
          },
          complete: () => {
            this.table.reset();
          },
        });
      },
      reject: () => { },
    });
  }

  CloseModal() {
    this.visible = false;
  }
  /** Đảo ngược giá trị của biến showHeader */
  toggleHeader() {
    this.showHeader = !this.showHeader;
  }

  //#endregion

  //#region Lst BangKhaoSatCauHoi không có group

  get lstBangKhaoSatCauHoi(): FormArray {
    return this.formTableSurvey.get('bangKhaoSatCauHoi') as FormArray;
  }

  showSialogCh = () => {
    this.selectedCauHoi = [];
    this.lstBangKhaoSatCauHoiGroup.clear();
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
      if (
        !this.lstBangKhaoSatCauHoi.value.find(
          (x: { maCauHoi: string }) => x.maCauHoi === el.maCauHoi
        )
      )
        this.lstBangKhaoSatCauHoi.push(newItem);
    });
    this.visibleCauHoi = false;
  }

  deleteItem(index: number) {
    this.lstBangKhaoSatCauHoi.removeAt(index);
    console.log(this.lstBangKhaoSatCauHoi);
  }

  //#endregion
  CheckButton() {
    const checklst = this.lstBangKhaoSatCauHoi.length;
    const checkGruop = this.lstBangKhaoSatCauHoiGroup.length;
    return (checkGruop === 0 && checklst === 0) ? true : (checklst > 0 || checkGruop > 0) ? false : true;
  }


  get lstBangKhaoSatCauHoiGroup(): FormArray {
    return this.formTableSurvey.get('bangKhaoSatCauHoiGroup') as FormArray;
  }

  addGroup = () => {
    this.lstBangKhaoSatCauHoi.clear();
    const newItem = this.FormBuilder.group({
      panelTitle: ['', Validators.required],
      bangKhaoSatCauHoi: this.FormBuilder.array<BangKhaoSatCauHoi>([]),
    });
    this.lstBangKhaoSatCauHoiGroup.push(newItem);
  };

  deleteGroup(index: number) {
    this.lstBangKhaoSatCauHoiGroup.removeAt(index);
  }

  lstBangKhaoSatCauHoiGroupItem(index: number): FormArray {
    let lstBangKhaoSatCauHoiGroupItem = this.lstBangKhaoSatCauHoiGroup
      .at(index)
      .get('bangKhaoSatCauHoi');
    return lstBangKhaoSatCauHoiGroupItem as FormArray;
  }

  showGroupDialogCh = (index: number) => {
    this.selectedCauHoi = [];
    this.visibleCauHoi = true;
    this.iPanelTitle = index;
  };

  addGroupItem() {
    this.selectedCauHoi.forEach((el, i) => {
      const newItem = this.FormBuilder.group<BangKhaoSatCauHoi>({
        id: 0,
        idCauHoi: el.id,
        priority: i,
        isRequired: false,
        panelTitle: '',
        maCauHoi: el.maCauHoi,
        tieuDe: el.tieuDe,
      });

      for (
        let index = 0;
        index < this.lstBangKhaoSatCauHoiGroup.length;
        index++
      ) {
        let itemCheck = this.lstBangKhaoSatCauHoiGroupItem(index).value.find(
          (x: { maCauHoi: string }) => x.maCauHoi === el.maCauHoi
        );
        if (itemCheck) {
          Utils.messageInfo(
            this.messageService,
            `Trường ${itemCheck.maCauHoi} trùng nhau`
          );
          return;
        }
      }

      this.lstBangKhaoSatCauHoiGroupItem(this.iPanelTitle).push(newItem);
    });
    this.visibleCauHoi = false;
  }

  deleteGroupItem(index: number, iPanelTitle: number) {
    this.lstBangKhaoSatCauHoiGroupItem(iPanelTitle).removeAt(index);
  }

  //#region Event gửi mail

  openGuiEmail = () => {
    this.visibleGuiEmail = true;
    this.selectedDonVi = [];
    if (this.frmGuiEmail) {
      this.frmGuiEmail.reset();
      this.frmGuiEmail.get('lstIdDonVi')?.reset();
      this.frmGuiEmail.get('lstBangKhaoSat')?.reset();
    }

    this.frmGuiEmail = this.FormBuilder.group({
      noidung: ['', Validators.required],
      tieuDe: ['', Validators.required],
      lstIdDonVi: this.FormBuilder.array([] as number[], Validators.required),
      lstBangKhaoSat: this.FormBuilder.array(
        this.selectedTableSurvey?.map((x) => x.id) ?? ([] as number[]),
        Validators.required
      ),
    });

    this.objectSurveyService.getAllByObj<DonVi>().subscribe((res) => {
      this.lstDonVi = res;
    });
  };

  onSubmitGuiEmail = () => {
    this.selectedDonVi.forEach((el) =>
      (this.frmGuiEmail.get('lstIdDonVi') as FormArray).push(
        this.FormBuilder.control(el)
      )
    );

    if (this.frmGuiEmail.invalid) return;
    let data = this.frmGuiEmail.value as CreateGuiEmail;
    this.lstIdDonViError = data.lstIdDonVi.length === 0;
    this.guiEmailService.createByDonVi(data).subscribe({
      next: (res) => {
        if (res.success) {
          Utils.messageSuccess(this.messageService, res.message);
          this.table.reset();
        } else {
          Utils.messageError(this.messageService, res.errors.at(0) ?? '');
        }
      },
      complete: () => {
        this.visibleGuiEmail = false;
      },
    });
  };

  //#endregion

  // xem chi tiết của bảng khảo khát
  detailTableSurvey(data: any) {
    this.baocaocauhoiservice.setSharedData(data);
    this.router.navigate(['/admin/thong-ke-khao-sat']);
  }
}
