import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators ,FormBuilder} from '@angular/forms';
import Utils from '@app/helpers/utils';
import { Paging } from '@app/models';
import { TinhThanh } from '@app/models/TinhThanh';
import { TinhThanhService } from '@app/services/tinh-thanh.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']
})
export class ProvinceComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas: TinhThanh[] = [];
  selectedTinhThanh!: TinhThanh[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  getId!:number;
  
  submitted: boolean = false;
  first = 0;
  showadd!: boolean;
  FormTinhThanh!: FormGroup;
  visible: boolean = false;
  constructor(
    private FormBuilder : FormBuilder,
    private TinhThanhService: TinhThanhService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
  }

  createForm = () => {
    this.FormTinhThanh = this.FormBuilder.group({
      code: ['', Validators.required],
      name: ['', Validators.required],
      type: ['', Validators.required],
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
    this.TinhThanhService.getByCondition(this.paging).subscribe({
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
    this.TinhThanhService.getByCondition(this.paging).subscribe({
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
        ? this.FormTinhThanh?.get(name)?.get(subName)
        : this.FormTinhThanh?.get(name)
    ) as FormControl;
  };

  onSubmit = () => {
    if (this.FormTinhThanh.invalid) return;
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
    this.getId = data.id
    this.FormTinhThanh.controls['code'].setValue(data.code);
    this.FormTinhThanh.controls['name'].setValue(data.name);
    this.FormTinhThanh.controls['type'].setValue(data.type);
  }

  SaveAdd() {
   
    const ObjTinhThanh = this.FormTinhThanh.value;
    this.TinhThanhService.create(ObjTinhThanh).subscribe({
      next: (res) => {
      
        if (res != null) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Thêm thành Công !',
          });
          
          this.table.reset();
          this.FormTinhThanh.reset();
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
    const ObjTinhThanh = this.FormTinhThanh.value;
    ObjTinhThanh['id']=this.getId;
    this.TinhThanhService.update(ObjTinhThanh).subscribe({
      
      next: (res: any) => {
        if (res.success == true) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          console.log("ress",res)
          this.table.reset();
          this.FormTinhThanh.reset();
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
        this.TinhThanhService.delete(data.id).subscribe((res: any) => {
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.table.reset();
          this.FormTinhThanh.reset();
        });
      },
    });
  }
  confirmDeleteMultiple() {
    debugger
    let ids: number[] = [];
    this.selectedTinhThanh.forEach((el) => {
      ids.push(el.id);
    });
    
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} Tỉnh Thành này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.TinhThanhService.deleteMultiple(ids).subscribe({
          next: (res:any) => {
          debugger
            
            if(res.success == false){
              Utils.messageError(this.messageService, res.message)
            }
            else{
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} Tỉnh Thành thành công!`
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
