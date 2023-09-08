import { Component, ViewChild } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/api';
import { Paging, PeriodSurvey } from '@app/models';
import { FormGroup, Validators, FormBuilder,AbstractControl } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { UnitTypeService, PeriodSurveyService } from '@app/services';
import { Table } from 'primeng/table';
import Utils from '@app/helpers/utils';

@Component({
  selector: 'app-admin-period-survey',
  templateUrl: './admin-period-survey.component.html',
  styleUrls: ['./admin-period-survey.component.css'],
})
export class AdminPeriodSurveyComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  selectedPeriodSurvey!: PeriodSurvey[];
  datas: PeriodSurvey[] = [];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  visible: boolean = false;

  showadd!: boolean;
  FormPeriodSurvey!: FormGroup;
  MaDotKhaoSat!: string;
  IdDotKhaoSat!: number;
  Trangthai!:any;
  DSLoaiHinh: any[] = [];
  dateRangeError = false;
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
    this.FormPeriodSurvey = this.FormBuilder.group({
      MaDotKhaoSat: ['', Validators.required],
      IdLoaiHinh: ['', Validators.required],
      TenDotKhaoSat: ['', Validators.required],
      NgayBatDau: ['', Validators.required],
      NgayKetThuc: ['', Validators.required],
      TrangThai: []
    }, { validator: this.dateRangeValidator });   
      }
  
  dateRangeValidator(control: AbstractControl): { [key: string]: boolean } | null {
    
    const startDate = control.get('NgayBatDau')?.value;
    const endDate = control.get('NgayKetThuc')?.value;
  
    if (startDate && endDate && startDate > endDate) {
      return { 'dateRangeError': true };
    }
  
    return null;
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
    this.showadd = true;
    this.visible = !this.visible;
    this.FormPeriodSurvey.reset();
  }

  Edit(data: any) {
    this.showadd = false;
    this.visible = !this.visible;
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
      'yyyy-MM-dd'
    );
    const ngayKetThucFormatted = this.datePipe.transform(
      data.ngayKetThuc,
      'yyyy-MM-dd'
    );
    this.FormPeriodSurvey.controls['NgayBatDau'].setValue(ngayBatDauFormatted);
    this.FormPeriodSurvey.controls['NgayKetThuc'].setValue(ngayKetThucFormatted);
    console.log(ngayKetThucFormatted);
  }

  Save() {
    if (this.showadd) {
      this.SaveAdd();
    } else {
      this.SaveEdit();
    }
  }

  SaveAdd() {
    if (this.FormPeriodSurvey.valid) {
      const ObjPeriodSurvey = this.FormPeriodSurvey.value;
      this.PeriodSurveyService.create(ObjPeriodSurvey).subscribe({
        next: (res) => {
          if (res != null) {
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Thêm thành Công !',
            });
            this.table.reset();
            this.FormPeriodSurvey.reset();
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
    const ObjPeriodSurvey = this.FormPeriodSurvey.value;
    ObjPeriodSurvey['id'] = this.IdDotKhaoSat;
    ObjPeriodSurvey['maDotKhaoSat'] = this.MaDotKhaoSat;
    ObjPeriodSurvey['Trangthai'] = this.Trangthai
    this.PeriodSurveyService.update(ObjPeriodSurvey).subscribe({
      next: (res: any) => {
        if (res.success == true) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.table.reset();
          this.FormPeriodSurvey.reset();
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
        this.PeriodSurveyService.delete(data.id).subscribe((res: any) => {
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.table.reset();
          this.FormPeriodSurvey.reset();
          console.log(res);
        });
      },
    });
  }
  confirmDeleteMultiple() {
    let ids: number[] = [];
    this.selectedPeriodSurvey.forEach((el) => {
      ids.push(el.Id);
    });
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} đợt khảo sát này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        // this.cauHoiService.deleteMultiple(ids).subscribe({
        //   next: (res) => {
        //     Utils.messageSuccess(
        //       this.messageService,
        //       `Xoá câu hỏi ${ids.length} thành công!`
        //     );
        //   },
        //   error: (e) => Utils.messageError(this.messageService, e.message),
        //   complete: () => {
        //     this.table.reset();
        //   },
        // });
      },
      reject: () => {},
    });
  }
}
