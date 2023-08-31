import { Component } from '@angular/core';
import { ConfirmEventType, ConfirmationService } from 'primeng/api';
import { ServiceService } from '@app/services/service.service';
import { Customer, Representative } from '@app/models/customer';
import { MessageService } from 'primeng/api';
import { PeriodSurvey } from '@app/models';
import { PeriodSurveyService } from '@app/services/period-survey.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-admin-period-survey',
  templateUrl: './admin-period-survey.component.html',
  styleUrls: ['./admin-period-survey.component.css']
})
export class AdminPeriodSurveyComponent {
  selectedPeriodSurvey!: PeriodSurvey[];
  datas: PeriodSurvey[] = [];

  first: number = 0;
  TotalCount: number = 0;
  pageIndex: number = 1;
  pageSize: number = 5;
  keyword: string = '';

  showadd!: boolean;
  FormPeriodSurvey!: FormGroup;
  MaDotKhaoSat !: string;
  IdDotKhaoSat !: number

  DSLoaiHinh: any[] = [];



  constructor(private FormBuilder: FormBuilder, private PeriodSurveyService: PeriodSurveyService,
    private messageService: MessageService, private confirmationService: ConfirmationService,private datePipe: DatePipe) { }

  ngOnInit() {
    this.GetPeriodSurvey()


    this.FormPeriodSurvey = this.FormBuilder.group({
      MaDotKhaoSat: ['', Validators.required],
      MaLoaiHinh: ['', Validators.required],
      TenDotKhaoSat: ['', Validators.required],
      NgayBatDau: ['', Validators.required],
      NgayKetThuuc: ['', Validators.required]
    });

    this.LoadLoaiHinh();
  }




  LoadLoaiHinh() {

    this.PeriodSurveyService.GetAllUnitType().subscribe((data) => {
      this.DSLoaiHinh = data; // Lưu dữ liệu vào danh sách

    });
  }

  GetNameById(maLoaiHinh: number): string {

    const loaiHinh = this.DSLoaiHinh.find(item => item.id == maLoaiHinh);
    return loaiHinh ? loaiHinh.tenLoaiHinh : maLoaiHinh;

  }



  onPageChange(event: any) {
    this.first = event.first;
    this.pageSize = event.rows;
    this.pageIndex = event.page + 1;
    this.GetPeriodSurvey();
  }

  GetPeriodSurvey() {

    this.PeriodSurveyService.SearchPeriodSurvey(this.pageIndex, this.pageSize, this.keyword)
      .subscribe((response: any) => {

        this.datas = response;
        this.TotalCount = response.totalItems;

      });
  }


  Add() {
    this.showadd = true;
  }
  Edit(data: any) {
    debugger
    this.showadd = false;
    this.IdDotKhaoSat = data.id;
    this.MaDotKhaoSat = data.maDotKhaoSat;
    this.FormPeriodSurvey.controls['MaDotKhaoSat'].setValue(data.maDotKhaoSat)
    this.FormPeriodSurvey.controls['MaLoaiHinh'].setValue(data.maLoaiHinh)
    this.FormPeriodSurvey.controls['TenDotKhaoSat'].setValue(data.tenDotKhaoSat)
    // Định dạng ngày tháng từ chuỗi "yyyy-MM-ddThh:mm" sang "dd/MM/yyyy"
  const ngayBatDauFormatted = this.datePipe.transform(data.ngayBatDau, 'yyyy-MM-dd');
  const ngayKetThuucFormatted = this.datePipe.transform(data.ngayKetThuuc, 'yyyy-MM-dd');

  this.FormPeriodSurvey.controls['NgayBatDau'].setValue(ngayBatDauFormatted);
  this.FormPeriodSurvey.controls['NgayKetThuuc'].setValue(ngayKetThuucFormatted);
  console.log(ngayKetThuucFormatted)
  }


  Save() {
    debugger
    if (this.showadd) {
      this.SaveAdd()
    }
    else {
      this.SaveEdit();
    }
  }


  SaveAdd() {

    if (this.FormPeriodSurvey.valid) {
      const ObjPeriodSurvey = this.FormPeriodSurvey.value;
      this.PeriodSurveyService.Insert(ObjPeriodSurvey).subscribe({
        next: (res) => {
          debugger
          if (res != null) {
            this.messageService.add({ severity: 'success', summary: 'Thành Công', detail: 'Thêm thành Công !' });
            this.GetPeriodSurvey();
            this.FormPeriodSurvey.reset();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Lỗi', detail: 'Lỗi vui Lòng kiểm tra lại !' });
          }
        }
      });
    }
  }
  SaveEdit() {

    const ObjPeriodSurvey = this.FormPeriodSurvey.value;
    ObjPeriodSurvey['id'] = this.IdDotKhaoSat;
    ObjPeriodSurvey['maDotKhaoSat'] = this.MaDotKhaoSat;
    this.PeriodSurveyService.Update(ObjPeriodSurvey).subscribe({
      next: (res) => {

        if (res == null) {
          this.messageService.add({ severity: 'success', summary: 'Thành Công', detail: 'Cập nhật Thành Công !' });
          this.GetPeriodSurvey();
          this.FormPeriodSurvey.reset();
          console.log(res)
        }
      }
    }
    )
  }


  Delete(data: any) {

    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.PeriodSurveyService.Delete(data.id).subscribe((res: any) => {
          debugger
          if (res.success == true)
            this.messageService.add({ severity: 'success', summary: 'Thành Công', detail: 'Xoá Thành Công !' });
          this.GetPeriodSurvey();
          this.FormPeriodSurvey.reset();
          console.log(res)

        })
      }
    });
  }


}
