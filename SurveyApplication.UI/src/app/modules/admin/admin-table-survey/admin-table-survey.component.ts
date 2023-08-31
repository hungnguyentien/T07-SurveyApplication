import { Component } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TableSurveyService } from '@app/services/table-survey.service';
import { TableSurvey } from '@app/models';

@Component({
  selector: 'app-admin-table-survey',
  templateUrl: './admin-table-survey.component.html',
  styleUrls: ['./admin-table-survey.component.css'],
})
export class AdminTableSurveyComponent {
  selectedTableSurvey!: TableSurvey[];
  datas: TableSurvey[] = [];

  first: number = 0;
  TotalCount: number = 0;
  pageIndex: number = 1;
  pageSize: number = 5;
  keyword: string = '';

  showadd!: boolean;
  FormTableSurvey!: FormGroup;
  MaLoaiHinh!: string;
  IdLoaiHinh!: string;

  constructor(
    private FormBuilder: FormBuilder,
    private TableSurveyService: TableSurveyService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit() {
    this.GetTableSurvey();
    this.FormTableSurvey = this.FormBuilder.group({
      MaBangKhaoSat: [''],
      MaLoaiHinh: ['', Validators.required],
      MaDotKhaoSat: ['', Validators.required],
      TenBangKhaoSat: ['', Validators.required],
      MoTa: ['', Validators.required],
      NgayBatDau: ['', Validators.required],
      NgayKetThuc: ['', Validators.required],
    });
  }

  onPageChange(event: any) {
    this.first = event.first;
    this.pageSize = event.rows;
    this.pageIndex = event.page + 1;
    this.GetTableSurvey();
  }

  GetTableSurvey() {
    this.TableSurveyService.SearchTableSurvey(
      this.pageIndex,
      this.pageSize,
      this.keyword
    ).subscribe((response: any) => {
      this.datas = response.data;
      this.TotalCount = response.totalItems;
    });
  }
  Add() {
    this.showadd = true;
  }
  Edit(data: any) {
    debugger;
    this.showadd = false;
    this.IdLoaiHinh = data.id;
    this.MaLoaiHinh = data.maLoaiHinh;
    this.FormTableSurvey.controls['MaLoaiHinh'].setValue(data.maLoaiHinh);
    this.FormTableSurvey.controls['TenLoaiHinh'].setValue(data.tenLoaiHinh);
    this.FormTableSurvey.controls['MoTa'].setValue(data.moTa);
  }

  Save() {
    if (this.showadd) {
      this.SaveAdd();
    } else {
      this.SaveEdit();
    }
  }

  SaveAdd() {
    if (this.FormTableSurvey.valid) {
      const ObjTableSurvey = this.FormTableSurvey.value;
      this.TableSurveyService.Insert(ObjTableSurvey).subscribe({
        next: (res) => {
          if (res != null) {
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Thêm thành Công !',
            });
            this.GetTableSurvey();
            this.FormTableSurvey.reset();
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
    debugger;
    const ObjTableSurvey = this.FormTableSurvey.value;
    ObjTableSurvey['id'] = this.IdLoaiHinh;
    ObjTableSurvey['maLoaiHinh'] = this.MaLoaiHinh;
    this.TableSurveyService.Update(ObjTableSurvey).subscribe({
      next: (res) => {
        debugger;
        if (res == null) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.GetTableSurvey();
          this.FormTableSurvey.reset();
          console.log(res);
        }
      },
    });
  }

  Delete(data: any) {
    debugger;
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        debugger;
        this.TableSurveyService.Delete(data.id).subscribe((res: any) => {
          debugger;
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.GetTableSurvey();
          this.FormTableSurvey.reset();
        });
      },
    });
  }
}
