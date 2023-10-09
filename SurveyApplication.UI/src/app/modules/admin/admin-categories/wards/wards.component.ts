import { Component, ViewChild } from '@angular/core';
import { FormGroup,FormBuilder, Validators, FormControl } from '@angular/forms';
import Utils from '@app/helpers/utils';
import { Paging, TinhThanh, XaPhuong } from '@app/models';
import { QuanHuyenService } from '@app/services/quan-huyen.service';
import { XaPhuongService } from '@app/services/xa-phuong.service';
import { TinhThanhService } from '@app/services/tinh-thanh.service';
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
  selectedTinh: any;
  
  submitted: boolean = false;
  first = 0;
  showadd!: boolean;
  FormXaPhuong!: FormGroup;
  listFormXaPhuong!: [];
  visible: boolean = false;

  listDataHuyen : any = [];
  listDataTinh : any = [];
  IdXaPhuong: any;

  checkBtnDetail:boolean = false
  actionDetail!:any;
  modalTitle = '';
  constructor(
    private FormBuilder : FormBuilder,
    private XaPhuongService: XaPhuongService,
    private quanHuyenService: QuanHuyenService,
    private messageService: MessageService,
    private tinhTpService: TinhThanhService,
    private confirmationService: ConfirmationService,
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
    this.GetAllTinhTp();
  }

  createForm = () => {
    this.FormXaPhuong = this.FormBuilder.group({
      parentCode: ['', Validators.required],
      name: ['', Validators.required],
      type: ['', Validators.required],
      code: ['', Validators.required],
    });
  };

  GetAllTinhTp(){
    this.tinhTpService.getAll().subscribe(res=>{
      this.listDataTinh = res
    })
  }

  onTinhTpChange() {
    const code = this.selectedTinh;
    this.quanHuyenService.getQuanHuyenByTinhTp(code ?? '').subscribe((res) => {
      this.listDataHuyen = res;
    });
  }

  detail(data:any){
    this.checkBtnDetail = true
    this.visible = !this.visible;
    this.modalTitle = 'Chi tiết Xã/Phường';
    this.FormXaPhuong.disable();
    this.FormXaPhuong.controls['parentCode'].setValue(data.codeQuanHuyen);
    this.FormXaPhuong.controls['name'].setValue(data.name);
    this.FormXaPhuong.controls['type'].setValue(data.type);
    this.FormXaPhuong.controls['code'].setValue(data.code);
  }


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
    this.checkBtnDetail = false; // check ẩn hiện button 
    this.modalTitle  = 'Thêm mới Xã/Phường';
    this.FormXaPhuong.enable();

    this.visible = !this.visible;
    this.showadd = true;
    this.FormXaPhuong.reset();
  }

  Edit(data: any) {
    this.FormXaPhuong.enable();
    this.FormXaPhuong.get("code")?.disable();
    this.modalTitle = 'Cập nhật Xã/Phường';
    this.checkBtnDetail = false; // check ẩn hiện button 

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
  confirmDeleteMultiple() {
    
    let ids: number[] = [];
    this.selectedXaPhuong.forEach((el) => {
      ids.push(el.id);
    });
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} xã/phường này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.XaPhuongService.deleteMultiple(ids).subscribe({
          next: (res:any) => {
            if(res.success == false){
              Utils.messageError(this.messageService, res.message)
            }
            else{
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} xã/phường thành công!`
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
