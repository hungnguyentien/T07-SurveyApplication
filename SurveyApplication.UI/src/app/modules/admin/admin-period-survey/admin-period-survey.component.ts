import { Component, ViewChild } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/api';
import { Paging, PeriodSurvey, TableSurvey } from '@app/models';

import {
  FormGroup,
  Validators,
  FormBuilder,
  AbstractControl,
} from '@angular/forms';
import { DatePipe } from '@angular/common';
import { UnitTypeService, PeriodSurveyService } from '@app/services';
import { Table } from 'primeng/table';
import Utils from '@app/helpers/utils';
import * as moment from 'moment';
import 'moment-timezone'; // Import 'moment-timezone'
import { FilterMetadata } from "primeng/api";
@Component({
  selector: 'app-admin-period-survey',
  templateUrl: './admin-period-survey.component.html',
  styleUrls: ['./admin-period-survey.component.css'],
})
export class AdminPeriodSurveyComponent {
  readonly filters: { [key in keyof PeriodSurvey]: FilterMetadata[] } = {
    MaDotKhaoSat: [{ value: '', matchMode: 'contains', operator: 'and' }],
    id: []
  };

  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  selectedPeriodSurvey!: PeriodSurvey[];
  datas: PeriodSurvey[] = [];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  visible: boolean = false;
  visibleDetail: boolean = false;

  showadd!: boolean;
  FormPeriodSurvey!: FormGroup;
  MaDotKhaoSat!: string;
  IdDotKhaoSat!: number;
  Trangthai!: any;
  DSLoaiHinh: any[] = [];
  dateRangeError = false;
  checkdetail!:boolean
  oldNgayKetThuc!: string | null;
  datasDetail: TableSurvey[] = [];
  
  checkBtnDetail:boolean = false
  actionDetail!:any;
  minDate!:Date;
  

  modalTitle = '';
  constructor(
    private FormBuilder: FormBuilder,
    private PeriodSurveyService: PeriodSurveyService,
    private unitTypeService: UnitTypeService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private datePipe: DatePipe
  ) {}
 
  ngOnInit() {
    
    this.LoadLoaiHinh();
    this.FormPeriodSurvey = this.FormBuilder.group(
      {
        MaDotKhaoSat: ['', Validators.required],
        IdLoaiHinh: ['', Validators.required],
        TenDotKhaoSat: ['', Validators.required],
        NgayBatDau: ['', Validators.required],
        NgayKetThuc: ['', Validators.required],
        TrangThai: [],
      },
      { validator: this.dateRangeValidator }
    );
    this.minDate = new Date(1900, 0, 1);
   
  }
  
  getDetailBangKhaoSat(data:number){
    debugger
    this.visibleDetail = !this.visibleDetail;
    this.PeriodSurveyService.getDotKhaoSatByDotKhaoSat(data).subscribe((res:any)=>{
      this.datasDetail = res;
    })
  }

  detail(data:any){
    this.checkBtnDetail = true
    this.visible = !this.visible;
    this.modalTitle = 'Chi tiết đợt khảo sát';
    this.FormPeriodSurvey?.disable();
    this.FormPeriodSurvey.controls['MaDotKhaoSat'].setValue(data.maDotKhaoSat);
    this.FormPeriodSurvey.controls['IdLoaiHinh'].setValue(data.idLoaiHinh);
    this.FormPeriodSurvey.controls['TenDotKhaoSat'].setValue(data.tenDotKhaoSat);
    const ngayBatDauFormatted = this.datePipe.transform(data.ngayBatDau,'dd/MM/yyyy');
    const ngayKetThucFormatted = this.datePipe.transform( data.ngayKetThuc,'dd/MM/yyyy');
    this.FormPeriodSurvey.controls['NgayBatDau'].setValue(ngayBatDauFormatted);
    this.FormPeriodSurvey.controls['NgayKetThuc'].setValue(ngayKetThucFormatted );

  }


  dateRangeValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    const startDate = control.get('NgayBatDau')?.value;
    const endDate = control.get('NgayKetThuc')?.value;
    if (startDate && endDate && startDate > endDate) {
      return { dateRangeError: true };
    }

    return null;
  }

  searchOnChange(value: string) {
    this.paging.keyword = this.keyWord;
    this.PeriodSurveyService.getByCondition(this.paging).subscribe({
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
    this.PeriodSurveyService.getByCondition(this.paging).subscribe({
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
    this.PeriodSurveyService.getByCondition(this.paging).subscribe({
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

  LoadLoaiHinh() {
    this.unitTypeService.getAll().subscribe((data) => {
      this.DSLoaiHinh = data;
    });
  }

  GetNameById(maLoaiHinh: number): string {
    const loaiHinh = this.DSLoaiHinh.find((item) => item.id == maLoaiHinh);
    return loaiHinh ? loaiHinh.tenLoaiHinh : maLoaiHinh;
  }

  Add() {
    this.checkBtnDetail = false; // check ẩn hiện button 
    this.modalTitle  = 'Thêm mới đợt khảo sát';
    this.FormPeriodSurvey.enable();
    this.showadd = true//check save
    this.visible = !this.visible;
    this.FormPeriodSurvey.reset();
  }

  Edit(data: any) {
    this.FormPeriodSurvey.enable();
    this.FormPeriodSurvey.get("MaDotKhaoSat")?.disable();
    this.modalTitle = 'Cập nhật đợt khảo sát';
    this.checkBtnDetail = false; // check ẩn hiện button 
    this.visible = !this.visible;
    this.showadd = false//check save
    this.IdDotKhaoSat = data.id;
    this.MaDotKhaoSat = data.maDotKhaoSat;
    this.Trangthai = data.trangThai;
    this.FormPeriodSurvey.controls['MaDotKhaoSat'].setValue(data.maDotKhaoSat);
    this.FormPeriodSurvey.controls['IdLoaiHinh'].setValue(data.idLoaiHinh);
    this.FormPeriodSurvey.controls['TenDotKhaoSat'].setValue(
      data.tenDotKhaoSat
    );
    const ngayBatDauFormatted = this.datePipe.transform(
      data.ngayBatDau,
      'dd/MM/yyyy'
    );
    const ngayKetThucFormatted = this.datePipe.transform(
      data.ngayKetThuc,
      'dd/MM/yyyy'
    );
    this.FormPeriodSurvey.controls['NgayBatDau'].setValue(ngayBatDauFormatted);
    this.FormPeriodSurvey.controls['NgayKetThuc'].setValue(
      ngayKetThucFormatted
    );
  }

  Save() {
    if (this.showadd == true) {
      this.SaveAdd();
    } else if(this.showadd == false) {
      this.SaveEdit();
    }
  }

  SaveAdd() {
    if (this.FormPeriodSurvey.valid) {
      const ObjPeriodSurvey = this.FormPeriodSurvey.value;
      ObjPeriodSurvey.NgayBatDau = Utils.plusDate(ObjPeriodSurvey.NgayBatDau, 'DD/MM/YYYY');
      ObjPeriodSurvey.NgayKetThuc = Utils.plusDate(ObjPeriodSurvey.NgayKetThuc, 'DD/MM/YYYY');
      this.PeriodSurveyService.create(ObjPeriodSurvey).subscribe({
        next: (res:any) => {
          if (res.success == true) {
            Utils.messageSuccess(this.messageService, res.message);
            this.table.reset();
            this.FormPeriodSurvey.reset();
            this.visible = false;
          } 
          else {
            Utils.messageError(this.messageService, res.message);
          }
        },
        error: (e) => {
          Utils.messageError(this.messageService, e.message)
        }
      });
    }
  }

  SaveEdit() {
    const ObjPeriodSurvey = this.FormPeriodSurvey.value;
    ObjPeriodSurvey.NgayBatDau = Utils.plusDate(ObjPeriodSurvey.NgayBatDau, 'DD/MM/YYYY');
    ObjPeriodSurvey.NgayKetThuc = Utils.plusDate(ObjPeriodSurvey.NgayKetThuc, 'DD/MM/YYYY');

    ObjPeriodSurvey['id'] = this.IdDotKhaoSat;
    ObjPeriodSurvey['maDotKhaoSat'] = this.MaDotKhaoSat;
    ObjPeriodSurvey['Trangthai'] = this.Trangthai;
    this.PeriodSurveyService.update(ObjPeriodSurvey).subscribe({
      next: (res: any) => {

        if (res.success == true){
          Utils.messageSuccess(this.messageService, res.message);
          this.table.reset();
          this.FormPeriodSurvey.reset();
          this.visible = false;
        }
        else{
          Utils.messageError(this.messageService, res.message);
          this.table.reset();
          this.FormPeriodSurvey.reset();
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
        this.PeriodSurveyService.delete(data.id).subscribe((res: any) => {
          if (res.success == true){
            Utils.messageSuccess(this.messageService, res.message);
            this.table.reset();
            this.FormPeriodSurvey.reset();
          }
          else{
            Utils.messageError(this.messageService, res.message);
            this.table.reset();
            this.FormPeriodSurvey.reset();
          }
        });
      },
    });
  }
  confirmDeleteMultiple() {
    let ids: number[] = [];
    this.selectedPeriodSurvey.forEach((el) => {
      ids.push(el.id);
    });

    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} đợt khảo sát này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.PeriodSurveyService.deleteMultiple(ids).subscribe({
          next: (res: any) => {
            if (res.success == false) {
              Utils.messageError(this.messageService, res.message);
            } else {
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} đợt khảo sát thành công!`
              );
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
}
