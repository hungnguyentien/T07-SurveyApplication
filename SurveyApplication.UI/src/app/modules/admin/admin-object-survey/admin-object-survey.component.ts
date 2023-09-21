import { Component, ViewChild } from '@angular/core';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import {
  LinhVucHoatDongService,
  ObjectSurveyService,
  UnitTypeService,
} from '@app/services';
import { CreateUnitAndRep } from '@app/models/CreateUnitAndRep';
import { Table } from 'primeng/table';
import { DonVi, LinhVucHoatDong, Paging } from '@app/models';
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

  combinedArray: any[] = [];
  cities: any[] = [];
  districts: any[] = [];
  wards: any[] = [];
  visible: boolean = false;
  lstLinhVuc: LinhVucHoatDong[] | undefined;

  

  constructor(
    private objectSurveyService: ObjectSurveyService,
    private unitTypeService: UnitTypeService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private linhVucHoatDongService: LinhVucHoatDongService
  ) {}
  ngOnInit() {
    // this.GetObjectSurvey();
    this.GetAllFieldOfActivity();
    this.GetUnitType();
    this.FormObjectSurvey = new FormGroup({
      IdLoaiHinh: new FormControl('', Validators.required),
      IdLinhVuc: new FormControl('', Validators.required),
      TenDonVi: new FormControl('', Validators.required),
      MaSoThue: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      WebSite: new FormControl('', Validators.required),
      SoDienThoai: new FormControl('', Validators.required),
      DiaChi: new FormControl('', Validators.required),
      
      // DiaChi: new FormGroup({
      //   TinhThanh: new FormControl(''),
      //   QuanHuyen: new FormControl(''),
      //   PhuongXa: new FormControl(''),
      //   DiaChiChiTiet: new FormControl('')
      // }),
    });

    this.FormRepresentative = new FormGroup({
      HoTen: new FormControl('', Validators.required),
      ChucVu: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      SoDienThoai: new FormControl('', Validators.required),
      MoTa: new FormControl(''),
    });

    this.objectSurveyService.getCities().subscribe((data: any) => {
      this.cities = data;
    });

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

  onCityChange(event: any): void {
    const selectedCityId = event.target.value;
    const selectedCity = this.cities.find((city) => city.Id === selectedCityId);
    this.districts = selectedCity?.Districts || [];
    this.wards = [];
    this.combineArrays();
  }

  onDistrictChange(event: any): void {
    const selectedDistrictId: string = event.target.value;
    const selectedCityId: string | undefined = this.cities.find((city) =>
      city.Districts.some(
        (district: { Id: string }) => district.Id === selectedDistrictId
      )
    )?.Id;

    const selectedCity = this.cities.find((city) => city.Id === selectedCityId);
    const selectedDistrict = selectedCity?.Districts.find(
      (district: { Id: string }) => district.Id === selectedDistrictId
    );

    this.wards = selectedDistrict?.Wards || [];
    this.combineArrays();
  }

  combineArrays(): void {
    this.combinedArray = [...this.cities, ...this.districts, ...this.wards];
  }

  getCombinedArrayAsString(): string {
    return this.combinedArray.map((item) => item.Id).join(',');
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

  Add() {
    this.FormObjectSurvey.reset();
    this.FormRepresentative.reset();
    this.showadd = true;
    this.visible = !this.visible;
  }

  Edit(data: any) {
    this.showadd = false;
    this.visible = !this.visible;
    this.Madonvi=data.maDonVi;
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
      },
      error: (e) => Utils.messageError(this.messageService, e.message),
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
    debugger
    if (this.FormObjectSurvey.valid && this.FormRepresentative.valid) {
      const obj: CreateUnitAndRep = {
        
        donViDto: this.FormObjectSurvey.value,
        nguoiDaiDienDto: this.FormRepresentative.value,
      };
      this.objectSurveyService.create(obj).subscribe({
        next: (res) => {
          debugger
          if (res != null) {
            console.log("res",res)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Thêm thành Công !',
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
  }

  SaveEdit() {
    debugger
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
        debugger
        if (res != null) {
           debugger
           
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
          debugger
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
          next: (res:any) => {
            
            if(res.success == false){
              Utils.messageError(this.messageService, res.message)
            }
            else{
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} đơn vị thành công!`
              );
            }
          },
          error: (e) => Utils.messageError(this.messageService, e.message),
          complete: () => {
            this.table.reset();
          },
        });
      },
      reject: () => { },
    });
  }
}
