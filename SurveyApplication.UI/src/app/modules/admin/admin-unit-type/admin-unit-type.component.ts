import { Component, ViewChild } from '@angular/core';
import { UnitTypeService } from '@app/services/unit-type.service';
import {
  Validators,
  FormGroup,
  FormBuilder,
  FormControl,
} from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Paging, UnitType } from '@app/models';
import { Table } from 'primeng/table';
import Utils from '@app/helpers/utils';

@Component({
  selector: 'app-admin-unit-type',
  templateUrl: './admin-unit-type.component.html',
  styleUrls: ['./admin-unit-type.component.css'],
})
export class AdminUnitTypeComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas: UnitType[] = [];
  selectedUnitType!: UnitType[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  
  submitted: boolean = false;

  first = 0;
  showadd!: boolean;
  FormUnitType!: FormGroup;
  MaLoaiHinh!: string;
  IdLoaiHinh!: string;
  visible: boolean = false;
  constructor(
    private FormBuilder: FormBuilder,
    private UnitTypeService: UnitTypeService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
  }

  createForm = () => {
    this.FormUnitType = this.FormBuilder.group({
      MaLoaiHinh: [{ value: this.MaLoaiHinh, disabled: true }],
      TenLoaiHinh: ['', Validators.required],
      MoTa: ['', Validators.required],
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
    this.UnitTypeService.getByCondition(this.paging).subscribe({
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
    this.UnitTypeService.getByCondition(this.paging).subscribe({
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
        ? this.FormUnitType?.get(name)?.get(subName)
        : this.FormUnitType?.get(name)
    ) as FormControl;
  };

  onSubmit = () => {
    if (this.FormUnitType.invalid) return;
    this.submitted = true;
    if (this.showadd !== null) this.showadd ? this.SaveAdd() : this.SaveEdit();
  };

  Add() {
    this.visible = !this.visible;
    this.showadd = true;
    this.FormUnitType.reset();
    this.UnitTypeService.generateMaLoaiHinh().subscribe({
      next: (res: any) => {
        this.FormUnitType.controls['MaLoaiHinh'].setValue(res.maLoaiHinh);
        this.MaLoaiHinh = res.maLoaiHinh;
        console.log(this.MaLoaiHinh);
      },
    });
  }

  Edit(data: any) {
    this.visible = !this.visible;
    this.showadd = false;
    this.IdLoaiHinh = data.id;
    this.MaLoaiHinh = data.maLoaiHinh;
    this.FormUnitType.controls['MaLoaiHinh'].setValue(data.maLoaiHinh);
    this.FormUnitType.controls['TenLoaiHinh'].setValue(data.tenLoaiHinh);
    this.FormUnitType.controls['MoTa'].setValue(data.moTa);
  }

  SaveAdd() {
    const ObjUnitType = this.FormUnitType.value;
    ObjUnitType['maLoaiHinh'] = this.MaLoaiHinh;
    this.UnitTypeService.create(ObjUnitType).subscribe({
      next: (res) => {
        if (res != null) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Thêm thành Công !',
          });
          this.table.reset();
          this.FormUnitType.reset();
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
    const ObjUnitType = this.FormUnitType.value;
    ObjUnitType['id'] = this.IdLoaiHinh;
    ObjUnitType['maLoaiHinh'] = this.MaLoaiHinh;
    this.UnitTypeService.update(ObjUnitType).subscribe({
      next: (res: any) => {
        if (res.success == true) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.table.reset();
          this.FormUnitType.reset();
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
        this.UnitTypeService.delete(data.id).subscribe((res: any) => {
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.table.reset();
          this.FormUnitType.reset();
        });
      },
    });
  }
  confirmDeleteMultiple() {
    debugger
    let ids: number[] = [];
    this.selectedUnitType.forEach((el) => {
      ids.push(el.id);
    });
    
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} loại hình đơn vị này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.UnitTypeService.deleteMultiple(ids).subscribe({
          next: (res:any) => {
          debugger
            
            if(res.success == false){
              Utils.messageError(this.messageService, res.message)
            }
            else{
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} loại hình đơn vị thành công!`
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
