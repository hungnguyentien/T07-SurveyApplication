import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators ,FormBuilder} from '@angular/forms';
import Utils from '@app/helpers/utils';
import { Paging, QuanHuyen } from '@app/models';
import { QuanHuyenService } from '@app/services/quan-huyen.service';
import { TinhThanhService } from '@app/services/tinh-thanh.service';
import { PhieuKhaoSatService } from '@app/services/phieu-khao-sat.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  styleUrls: ['./district.component.css']
})
export class DistrictComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas: QuanHuyen[] = [];
  selectedQuanHuyen!: QuanHuyen[];
  uploadedFiles: any[] = [];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  
  submitted: boolean = false;
  first = 0;
  showadd!: boolean;
  FormQuanHuyen!: FormGroup;
  listDataTinh :any =  [];

  visible: boolean = false;
  showhide: boolean = false;

  IdQuanHuyen: any;

  checkBtnDetail:boolean = false
  actionDetail!:any;
  modalTitle = '';
  constructor(
    private FormBuilder : FormBuilder,
    private QuanHuyenService: QuanHuyenService,
    private Tinhservice: TinhThanhService,
    private PhieuKhaoSatService: PhieuKhaoSatService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
    this.GetAllTinh();
  }

  createForm = () => {
    this.FormQuanHuyen = this.FormBuilder.group({
      parentCode: ['', Validators.required],
      name: ['', Validators.required],
      type: ['', Validators.required],
      code: ['', Validators.required],
    });
  };

  detail(data:any){
    this.checkBtnDetail = true
    this.visible = !this.visible;
    this.modalTitle = 'Chi tiết Quận/Huyện';
    this.FormQuanHuyen.disable();
    this.FormQuanHuyen.controls['parentCode'].setValue(data.parent_code);
    this.FormQuanHuyen.controls['name'].setValue(data.name);
    this.FormQuanHuyen.controls['type'].setValue(data.type);
    this.FormQuanHuyen.controls['code'].setValue(data.code);

  }


  GetAllTinh(){
    this.Tinhservice.getAll().subscribe(res=>{
      this.listDataTinh = res
    })
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
    this.QuanHuyenService.getByCondition(this.paging).subscribe({
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
    this.QuanHuyenService.getByCondition(this.paging).subscribe({
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
        ? this.FormQuanHuyen?.get(name)?.get(subName)
        : this.FormQuanHuyen?.get(name)
    ) as FormControl;
  };

  
  onSubmit = () => {
    if (this.FormQuanHuyen.invalid) return;
    this.submitted = true;
    if (this.showadd !== null) this.showadd ? this.SaveAdd() : this.SaveEdit();
  };

  Add() {
    this.checkBtnDetail = false; // check ẩn hiện button 
    this.modalTitle  = 'Thêm mới Quận/Huyện';
    this.FormQuanHuyen.enable();

    this.visible = !this.visible;
    this.showadd = true;
    this.FormQuanHuyen.reset();
    
  }

  Edit(data: any) {
    this.FormQuanHuyen.enable();
    this.FormQuanHuyen.get("code")?.disable();
    this.modalTitle = 'Cập nhật Quận/Huyện';
    this.checkBtnDetail = false; // check ẩn hiện button 

    this.visible = !this.visible;
    this.showadd = false;
    this.IdQuanHuyen = data.id;
    this.FormQuanHuyen.controls['parentCode'].setValue(data.parent_code);
    this.FormQuanHuyen.controls['name'].setValue(data.name);
    this.FormQuanHuyen.controls['type'].setValue(data.type);
    this.FormQuanHuyen.controls['code'].setValue(data.code);
  }

  SaveAdd() {
    const ObjQuanHuyen = this.FormQuanHuyen.value;
    this.QuanHuyenService.create(ObjQuanHuyen).subscribe({
      next: (res) => {
        if (res != null) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Thêm thành Công !',
          });
          this.table.reset();
          this.FormQuanHuyen.reset();
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
    const ObjQuanHuyen = this.FormQuanHuyen.value;
    ObjQuanHuyen['id'] = this.IdQuanHuyen;
    this.QuanHuyenService.update(ObjQuanHuyen).subscribe({
      next: (res: any) => {
        if (res.success == true) {
          this.messageService.add({
            severity: 'success',
            summary: 'Thành Công',
            detail: 'Cập nhật Thành Công !',
          });
          this.table.reset();
          this.FormQuanHuyen.reset();
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
        this.QuanHuyenService.delete(data.id).subscribe((res: any) => {
          if (res.success == true)
            this.messageService.add({
              severity: 'success',
              summary: 'Thành Công',
              detail: 'Xoá Thành Công !',
            });
          this.table.reset();
          this.FormQuanHuyen.reset();
        });
      },
    });
  }
  confirmDeleteMultiple() {
    
    let ids: number[] = [];
    this.selectedQuanHuyen.forEach((el) => {
      ids.push(el.id);
    });
    
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} Quận/ Huyện này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.QuanHuyenService.deleteMultiple(ids).subscribe({
          next: (res:any) => {
          
            
            if(res.success == false){
              Utils.messageError(this.messageService, res.message)
            }
            else{
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} Quận/ Huyện thành công!`
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

  Import() {
    this.showhide = !this.showhide;
  }

  onUpload(event: any) {
    this.PhieuKhaoSatService.uploadFiles(event.files).subscribe((res: any) => {
    })
    for (const file of event.files) {
      this.uploadedFiles.push(file);
      const formData = new FormData();
      formData.append('file', file);
      this.QuanHuyenService.Import(formData).subscribe((res: any) => {
        this.messageService.add({severity: 'success', summary: 'File Uploaded', detail: ''});
      });
    }
  }
}
