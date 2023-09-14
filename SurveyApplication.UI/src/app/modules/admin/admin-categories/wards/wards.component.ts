import { Component, ViewChild } from '@angular/core';
import { FormGroup,FormBuilder, Validators, FormControl } from '@angular/forms';
import Utils from '@app/helpers/utils';
import { Paging, XaPhuong } from '@app/models';
import { QuanHuyenService } from '@app/services/quan-huyen.service';
import { XaPhuongService } from '@app/services/xa-phuong.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-wards',
  templateUrl: './wards.component.html',
  styleUrls: ['./wards.component.css']
})
export class WardsComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas: XaPhuong[] = [];
  selectedXaPhuong!: XaPhuong[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  
  submitted: boolean = false;
  first = 0;
  showadd!: boolean;
  FormXaPhuong!: FormGroup;
  listFormXaPhuong!: [];
  visible: boolean = false;

  listDataHuyen : any = [];
  IdXaPhuong: any;

  constructor(
    private FormBuilder : FormBuilder,
    private XaPhuongService: XaPhuongService,
    private messageService: MessageService,
    private HuyenService:QuanHuyenService,
    private confirmationService: ConfirmationService,
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
    this.GetAllHuyen();
  }

  createForm = () => {
    this.FormXaPhuong = this.FormBuilder.group({
      parentCode: ['', Validators.required],
      name: ['', Validators.required],
      type: ['', Validators.required],
      code: ['', Validators.required],
    });
  };

  GetAllHuyen(){
    this.HuyenService.getAll().subscribe(res=>{
      this.listDataHuyen = res
    })
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
    this.XaPhuongService.getByCondition(this.paging).subscribe({
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
    this.XaPhuongService.getByCondition(this.paging).subscribe({
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
        ? this.FormXaPhuong?.get(name)?.get(subName)
        : this.FormXaPhuong?.get(name)
    ) as FormControl;
  };

  onSubmit = () => {
    if (this.FormXaPhuong.invalid) return;
    this.submitted = true;
    if (this.showadd !== null) this.showadd ? this.SaveAdd() : this.SaveEdit();
  };

  Add() {
    this.visible = !this.visible;
    this.showadd = true;
    
  }

  Edit(data: any) {
    this.visible = !this.visible;
    this.showadd = false;
    this.IdXaPhuong = data.id;
    this.FormXaPhuong.controls['parentCode'].setValue(data.codeQuanHuyen);
    this.FormXaPhuong.controls['name'].setValue(data.name);
    this.FormXaPhuong.controls['type'].setValue(data.type);
    this.FormXaPhuong.controls['code'].setValue(data.code);
  }

  SaveAdd() {
    const ObjXaPhuong = this.FormXaPhuong.value;
    this.XaPhuongService.create(ObjXaPhuong).subscribe({
      next: (res) => {
        if (res != null) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Thêm thành Công !',
          });
          this.table.reset();
          this.FormXaPhuong.reset();
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
    const ObjXaPhuong = this.FormXaPhuong.value;
    ObjXaPhuong['id'] = this.IdXaPhuong;
    this.XaPhuongService.update(ObjXaPhuong).subscribe({
      next: (res: any) => {
        if (res.success == true) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.table.reset();
          this.FormXaPhuong.reset();
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
        this.XaPhuongService.delete(data.id).subscribe((res: any) => {
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.table.reset();
          this.FormXaPhuong.reset();
        });
      },
    });
  }
//   confirmDeleteMultiple() {
//     let ids: number[] = [];
//     this.selectedXaPhuong.forEach((el) => {
//       ids.push(el.Id);
//     });
//     this.confirmationService.confirm({
//       message: `Bạn có chắc chắn muốn xoá ${ids.length} loại đơn vị này?`,
//       icon: 'pi pi-exclamation-triangle',
//       accept: () => {
//         this.cauHoiService.deleteMultiple(ids).subscribe({
//           next: (res) => {
//             Utils.messageSuccess(
//               this.messageService,
//               `Xoá câu hỏi ${ids.length} thành công!`
//             );
//           },
//           error: (e) => Utils.messageError(this.messageService, e.message),
//           complete: () => {
//             this.table.reset();
//           },
//         });
//       },
//       reject: () => {},
//     });
//   }
}
