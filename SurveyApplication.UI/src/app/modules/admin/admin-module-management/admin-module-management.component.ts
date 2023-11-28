import { Component, ViewChild } from '@angular/core';
import { FormGroup, Validators,FormBuilder, FormControl } from '@angular/forms';
import Utils from '@app/helpers/utils';
import { Module, Paging } from '@app/models';
import { ModuleService } from '@app/services';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-admin-module-management',
  templateUrl: './admin-module-management.component.html',
  styleUrls: ['./admin-module-management.component.css']
})
export class AdminModuleManagementComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas: Module[] = [];
  selectedModule!: Module[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  getId!:number;
  
  submitted: boolean = false;
  first = 0;
  showadd!: boolean;
  FormModule!: FormGroup;
  visible: boolean = false;
  constructor(
    private FormBuilder : FormBuilder,
    private ModuleService: ModuleService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
  }
  createForm = () => {
    this.FormModule = this.FormBuilder.group({
      id: [''],
      name: ['', Validators.required],
      routerLink: ['', Validators.required],
      icon: [''],
      codeModule: [''],
      idParent: [''],
      priority: [''],
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
    this.ModuleService.getByCondition(this.paging).subscribe({
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
    this.ModuleService.getByCondition(this.paging).subscribe({
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
        ? this.FormModule?.get(name)?.get(subName)
        : this.FormModule?.get(name)
    ) as FormControl;
  };

  onSubmit = () => {
    if (this.FormModule.invalid) return;
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
    this.FormModule.controls['code'].setValue(data.code);
    this.FormModule.controls['name'].setValue(data.name);
    this.FormModule.controls['type'].setValue(data.type);
  }

  SaveAdd() {
   
    const ObjModule = this.FormModule.value;
    this.ModuleService.create(ObjModule).subscribe({
      next: (res) => {
      
        if (res != null) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Thêm thành Công !',
          });
          
          this.table.reset();
          this.FormModule.reset();
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
    const ObjModule = this.FormModule.value;
    ObjModule['id']=this.getId;
    this.ModuleService.update(ObjModule).subscribe({
      
      next: (res: any) => {
        if (res.success == true) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          console.log("ress",res)
          this.table.reset();
          this.FormModule.reset();
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
        this.ModuleService.delete(data.id).subscribe((res: any) => {
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.table.reset();
          this.FormModule.reset();
        });
      },
    });
  }
  confirmDeleteMultiple() {
    let ids: number[] = [];
    this.selectedModule.forEach((el) => {
      ids.push(el.id);
    });
    
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} Tỉnh Thành này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.ModuleService.deleteMultiple(ids).subscribe({
          next: (res:any) => {
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
