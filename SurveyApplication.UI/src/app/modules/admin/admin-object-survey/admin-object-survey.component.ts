import { Component, ViewChild } from '@angular/core';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import {
  LinhVucHoatDongService,
  ObjectSurveyService,
  PhieuKhaoSatService,
  UnitTypeService,
  XaPhuongService,
  QuanHuyenService
} from '@app/services';
import { CreateUnitAndRep } from '@app/models/CreateUnitAndRep';
import { Table } from 'primeng/table';
import { DonVi, HanhChinhVn, LinhVucHoatDong, Paging, DonViNguoiDaiDienResponse } from '@app/models';
import Utils from '@app/helpers/utils';

@Component({
  selector: 'app-admin-object-survey',
  templateUrl: './admin-object-survey.component.html',
  styleUrls: ['./admin-object-survey.component.css'],
})
export class AdminObjectSurveyComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  selectedObjectSurvey!: DonVi[];
  datas: DonVi[] = [];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  checkBtnDetail:boolean = false
  actionDetail!:any;
  modalTitle = '';

  showadd!: boolean;
  FormObjectSurvey!: FormGroup;
  FormRepresentative!: FormGroup;
  IdDonVi!: string;
  Madonvi!: string;

  IdNguoiDaiDien!: string;
  MaNguoiDaiDien!: string;
  IdLoaiHinh!: string;
  listloaihinhdonvi: any[] = [];
  listlinhvuchoatdong: any[] = [];
  ContainerAdd: any[] = [];

  visible: boolean = false;
  lstLinhVuc: LinhVucHoatDong[] | undefined;

  cities: HanhChinhVn[] = [];
  districts: HanhChinhVn[] = [];
  wards: HanhChinhVn[] = [];
  selectedTinh: string | undefined;
  selectedQuanHuyen: string | undefined;
  selectedPhuongXa: string | undefined;

  constructor(
    private objectSurveyService: ObjectSurveyService,
    private unitTypeService: UnitTypeService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private linhVucHoatDongService: LinhVucHoatDongService,
    private phieuKhaoSatService: PhieuKhaoSatService,
    private xaPhuongService: XaPhuongService,
    private quanHuyenService: QuanHuyenService,
  ) {}
  ngOnInit() {
    this.GetAllFieldOfActivity();
    this.GetUnitType();
    this.FormObjectSurvey = new FormGroup({
      IdLoaiHinh: new FormControl('', Validators.required),
      IdLinhVuc: new FormControl(''),
      TenDonVi: new FormControl('', Validators.required),
      MaSoThue: new FormControl(''),
      MaDonVi: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      WebSite: new FormControl(''),
      SoDienThoai: new FormControl('',[Validators.required, Validators.pattern('^[0-9]{10,11}$')] ),
      DiaChi: new FormControl('', Validators.required),
      IdTinhTp: new FormControl('', Validators.required),
      IdQuanHuyen: new FormControl('', Validators.required),
      IdXaPhuong: new FormControl('', Validators.required),
    });

    this.FormRepresentative = new FormGroup({
      HoTen: new FormControl('', Validators.required),
      ChucVu: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      SoDienThoai: new FormControl('',[Validators.required, Validators.pattern('^[0-9]{10,11}$')] ),
      MoTa: new FormControl(''),
    });

    this.phieuKhaoSatService
      .getTinh()
      .subscribe((data) => (this.cities = data));

    this.phieuKhaoSatService
      .getQuanHuyen()
      .subscribe((data) => (this.districts = data));

    this.phieuKhaoSatService
      .getPhuongXa()
      .subscribe((data) => (this.wards = data));

    // this.phieuKhaoSatService
    //   .getTinh()
    //   .subscribe((data) => (this.cities = data));

    this.linhVucHoatDongService.getAll().subscribe({
      next: (res) => {
        this.loading = true;
        this.lstLinhVuc = res;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  }

  onCityChange(): void {
    const code = this.selectedTinh;
    this.wards = [];
    this.quanHuyenService.getQuanHuyenByTinhTp(code ?? '').subscribe((res) => {
          this.districts = res;
      });
  }

  onDistrictChange(): void {
    const code = this.selectedQuanHuyen;
    this.xaPhuongService
      .getPhuongXaByQuanHuyen(code ?? '').subscribe((res) => {
        this.wards = res;
    });
  }

  GetUnitType() {
    this.unitTypeService.getAll().subscribe((response: any) => {
      this.listloaihinhdonvi = response;
    });
  }

  GetAllFieldOfActivity() {
    this.objectSurveyService
      .GetAllFieldOfActivity()
      .subscribe((response: any) => {
        this.listlinhvuchoatdong = response;
      });
  }

  //#region

  setValueDiaChi = () => {
    this.FormObjectSurvey.get('IdTinhTp')?.setValue(
      this.cities.find((x) => x.code === this.selectedTinh)?.id
    );
    this.FormObjectSurvey.get('IdQuanHuyen')?.setValue(
      this.districts.find((x) => x.code === this.selectedQuanHuyen)?.id
    );
    this.FormObjectSurvey.get('IdXaPhuong')?.setValue(
      this.wards.find((x) => x.code === this.selectedPhuongXa)?.id
    );
  };

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: this.keyWord,
      orderBy: event.sortField
        ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
        : '',
    };
    this.objectSurveyService.getByConditionTepm<DonVi>(this.paging).subscribe({
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
    this.objectSurveyService.getByConditionTepm<DonVi>(this.paging).subscribe({
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

  detail(data:any){
    this.checkBtnDetail = true
    this.visible = !this.visible;
    this.modalTitle = 'Chi tiết đơn vị';
    this.FormObjectSurvey.disable();

    this.selectedTinh = this.cities.find(
      (x) => x.id === data.idTinhTp
    )?.code;
    this.selectedQuanHuyen = this.districts.find(
      (x) => x.id === data.idQuanHuyen
    )?.code;
    this.selectedPhuongXa = this.wards.find(
      (x) => x.id === data.idXaPhuong
    )?.code;

    this.FormObjectSurvey.controls['IdLoaiHinh'].setValue(data.idLoaiHinh);
    this.FormObjectSurvey.controls['IdLinhVuc'].setValue(data.idLinhVuc);
    this.FormObjectSurvey.controls['TenDonVi'].setValue(data.tenDonVi);
    this.FormObjectSurvey.controls['MaSoThue'].setValue(data.maSoThue);
    this.FormObjectSurvey.controls['MaDonVi'].setValue(data.maDonVi);
    this.FormObjectSurvey.controls['Email'].setValue(data.emailDonVi);
    this.FormObjectSurvey.controls['WebSite'].setValue(data.webSite);
    this.FormObjectSurvey.controls['SoDienThoai'].setValue(data.soDienThoaiDonVi);
    this.FormObjectSurvey.controls['DiaChi'].setValue(data.diaChi);
    this.FormObjectSurvey.controls['IdTinhTp'].setValue(this.selectedTinh);
    this.FormObjectSurvey.controls['IdQuanHuyen'].setValue(this.selectedQuanHuyen);
    this.FormObjectSurvey.controls['IdXaPhuong'].setValue(this.selectedPhuongXa);

    this.FormRepresentative.controls['HoTen'].setValue(data.hoTen);
    this.FormRepresentative.controls['ChucVu'].setValue(data.chucVu);
    this.FormRepresentative.controls['Email'].setValue(data.emailNguoiDaiDien);
    this.FormRepresentative.controls['SoDienThoai'].setValue(data.soDienThoaiNguoiDaiDien);
    this.FormRepresentative.controls['MoTa'].setValue(data.moTa);
  }

  Add() {
    this.checkBtnDetail = false;
    this.modalTitle  = 'Thêm mới đơn vị';
    this.FormObjectSurvey.enable();
    this.FormObjectSurvey.reset();
    this.FormRepresentative.reset();
    this.visible = !this.visible;
    this.showadd = true;
    this.FormObjectSurvey.get('MaDonVi')?.enable();
  }

  Edit(data: any) {
    this.FormObjectSurvey.enable();
    this.FormObjectSurvey.get("MaDonVi")?.disable();
    this.modalTitle = 'Cập nhật đơn vị';
    this.checkBtnDetail = false;
    this.showadd = false;
    this.visible = !this.visible;
    this.Madonvi = data.maDonVi;
    this.objectSurveyService.getById<CreateUnitAndRep>(data.idDonVi).subscribe({
      next: (res) => {
        Utils.setValueForm(
          this.FormObjectSurvey,
          Object.keys(res.donViDto),
          Object.values(res.donViDto),
          true
        );
        Utils.setValueForm(
          this.FormRepresentative,
          Object.keys(res.nguoiDaiDienDto),
          Object.values(res.nguoiDaiDienDto),
          true
        );
        this.IdDonVi = res.donViDto.id.toString();
        this.IdNguoiDaiDien = res.nguoiDaiDienDto.id.toString();
        this.selectedTinh = this.cities.find(
          (x) => x.id === res.donViDto.idTinhTp
        )?.code;
        this.selectedQuanHuyen = this.districts.find(
          (x) => x.id === res.donViDto.idQuanHuyen
        )?.code;
        this.selectedPhuongXa = this.wards.find(
          (x) => x.id === res.donViDto.idXaPhuong
        )?.code;

        this.FormObjectSurvey.controls['IdTinhTp'].setValue(this.selectedTinh);
        this.FormObjectSurvey.controls['IdQuanHuyen'].setValue(this.selectedQuanHuyen);
        this.FormObjectSurvey.controls['IdXaPhuong'].setValue(this.selectedPhuongXa);
      },
    });
  }

  Save() {
    if (this.showadd) {
      this.SaveAdd();
    } else {
      this.SaveEdit();
    }
  }

  SaveAdd() {
    this.setValueDiaChi();
    if (this.FormObjectSurvey.valid && this.FormRepresentative.valid) {
      const obj: CreateUnitAndRep = {
        donViDto: this.FormObjectSurvey.value,
        nguoiDaiDienDto: this.FormRepresentative.value,
      };
      debugger
      this.objectSurveyService.create(obj).subscribe({
        next: (res) => {
          if ('response_1' in res && 'response_2' in res) {
            const donViResponse = res as DonViNguoiDaiDienResponse;
            if (donViResponse.response_1.success && donViResponse.response_2.success) {
              Utils.messageSuccess(this.messageService, donViResponse.response_1.message);
              this.table.reset();
              this.FormObjectSurvey.reset();
              this.FormRepresentative.reset();
              this.visible = false;
            } else {
              const errorsFromResponse1 = donViResponse.response_1.errors || [];
              const errorsFromResponse2 = donViResponse.response_2.errors || [];
              const allErrors = [...errorsFromResponse1, ...errorsFromResponse2];
              
              if (allErrors.length > 0) {
                for (const error of allErrors) {
                  Utils.messageError(this.messageService, error);
                }
              }
            }
          }
        },
      });            
    }
  }

  SaveEdit() {
    this.setValueDiaChi();
    const updatedFormObjectSurveyValue = { ...this.FormObjectSurvey.value };
    updatedFormObjectSurveyValue['Id'] = this.IdDonVi;
    updatedFormObjectSurveyValue['MaDonVi'] = this.Madonvi;
    const updatedFormRepresentativeValue = { ...this.FormRepresentative.value };
    updatedFormRepresentativeValue['Id'] = this.IdNguoiDaiDien;
    updatedFormRepresentativeValue['IdDonVi'] = this.IdDonVi;
    const obj: CreateUnitAndRep = {
      donViDto: updatedFormObjectSurveyValue,
      nguoiDaiDienDto: updatedFormRepresentativeValue,
    };
    this.objectSurveyService.update(obj).subscribe({
      next: (res) => {
        if (res != null) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.table.reset();
          this.FormObjectSurvey.reset();
          this.FormRepresentative.reset();
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

  Delete(DonVi: any) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.objectSurveyService.delete(DonVi).subscribe((res: any) => {
          if (res.success == true) {
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
            this.table.reset();
            this.FormObjectSurvey.reset();
            this.FormRepresentative.reset();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Lỗi',
              detail: 'Lỗi vui Lòng kiểm tra lại !',
            });
          }
        });
      },
    });
  }

  confirmDeleteMultiple() {
    let ids: number[] = [];
    this.selectedObjectSurvey.forEach((el) => {
      ids.push(el.id);
    });

    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} đơn vị này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.objectSurveyService.deleteMultiple(ids).subscribe({
          next: (res: any) => {
            if (res.success == false) {
              Utils.messageError(this.messageService, res.message);
            } else {
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} đơn vị thành công!`
              );
              this.selectedObjectSurvey = [];
            }
          },
          error: (e) => Utils.messageError(this.messageService, e.message),
          complete: () => {
            this.table.reset();
          },
        });
      },
      reject: () => {},
    });
  }

  //#endregion
}
