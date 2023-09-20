import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators,FormBuilder } from '@angular/forms';
import Utils from '@app/helpers/utils';
import { LinhVucHoatDong, Paging } from '@app/models';
import { LinhVucHoatDongService } from '@app/services';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-field-of-activity',
  templateUrl: './field-of-activity.component.html',
  styleUrls: ['./field-of-activity.component.css']
})
export class FieldOfActivityComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas: LinhVucHoatDong[] = [];
  selectedFieldOfActivity!: LinhVucHoatDong[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  
  submitted: boolean = false;
  first = 0;
  showadd!: boolean;
  FormFieldOfActivity!: FormGroup;
  visible: boolean = false;
  IdLinhVuc: any;
  constructor(
    private FormBuilder : FormBuilder,
    private FieldOfActivityService: LinhVucHoatDongService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
  }

  createForm = () => {
    this.FormFieldOfActivity = this.FormBuilder.group({
      maLinhVuc: ['', Validators.required],
      tenLinhVuc: ['', Validators.required],
      moTa: ['', Validators.required],
    });
  };

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
    this.FieldOfActivityService.getByCondition(this.paging).subscribe({
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
    this.FieldOfActivityService.getByCondition(this.paging).subscribe({
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

  f = (name: string, subName: string = ''): FormControl => {
    return (
      subName
        ? this.FormFieldOfActivity?.get(name)?.get(subName)
        : this.FormFieldOfActivity?.get(name)
    ) as FormControl;
  };

  onSubmit = () => {
    if (this.FormFieldOfActivity.invalid) return;
    this.submitted = true;
    if (this.showadd !== null) this.showadd ? this.SaveAdd() : this.SaveEdit();
  };

  Add() {
    this.visible = !this.visible;
    this.showadd = true;
    this.FormFieldOfActivity.reset();
  }

  Edit(data: any) {
    this.visible = !this.visible;
    this.showadd = false;

    this.IdLinhVuc = data.id;
    this.FormFieldOfActivity.controls['maLinhVuc'].setValue(data.maLinhVuc);
    this.FormFieldOfActivity.controls['tenLinhVuc'].setValue(data.tenLinhVuc);
    this.FormFieldOfActivity.controls['moTa'].setValue(data.moTa);
  }

  SaveAdd() {
    const ObjFieldOfActivity = this.FormFieldOfActivity.value;
    this.FieldOfActivityService.create(ObjFieldOfActivity).subscribe({
      next: (res) => {
        if (res != null) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Thêm thành Công !',
          });
          this.table.reset();
          this.FormFieldOfActivity.reset();
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

  SaveEdit() {
    const ObjFieldOfActivity = this.FormFieldOfActivity.value;
    ObjFieldOfActivity['id'] = this.IdLinhVuc;
    this.FieldOfActivityService.update(ObjFieldOfActivity).subscribe({
      next: (res: any) => {
        if (res.success == true) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.table.reset();
          this.FormFieldOfActivity.reset();
          this.visible = false;
        }
      },
    });
  }

  Delete(data: any) {
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá không?`,
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.FieldOfActivityService.delete(data.id).subscribe((res: any) => {
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.table.reset();
          this.FormFieldOfActivity.reset();
        });
      },
    });
  }
  confirmDeleteMultiple() {
    let ids: number[] = [];
    this.selectedFieldOfActivity.forEach((el) => {
      ids.push(el.id);
    });
    
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} lĩnh vực này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.FieldOfActivityService.deleteMultiple(ids).subscribe({
          next: (res:any) => {
            if(res.success == false){
              Utils.messageError(this.messageService, res.message)
            }
            else{
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} lĩnh vực thành công!`
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

