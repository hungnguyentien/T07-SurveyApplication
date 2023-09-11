import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators,FormBuilder } from '@angular/forms';
import Utils from '@app/helpers/utils';
import { FieldOfActivity, Paging } from '@app/models';
import { FieldOfActivityService } from '@app/services/field-of-activity.service';
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
  datas: FieldOfActivity[] = [];
  selectedFieldOfActivity!: FieldOfActivity[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  
  submitted: boolean = false;
  first = 0;
  showadd!: boolean;
  FormFieldOfActivity!: FormGroup;
  listFormFieldOfActivity!: [];
  visible: boolean = false;
  constructor(
    private FormBuilder : FormBuilder,
    private FieldOfActivityService: FieldOfActivityService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
    this.getAll();
  }

  createForm = () => {
    this.FormFieldOfActivity = this.FormBuilder.group({
      MaLinhVuc: ['', Validators.required],
      TenLinhVuc: ['', Validators.required],
      MoTa: ['', Validators.required],
    });
  };

  // loadListLazy = (event: any) => {
  //   this.loading = true;
  //   let pageSize = event.rows;
  //   let pageIndex = event.first / pageSize + 1;
  //   this.paging = {
  //     pageIndex: pageIndex,
  //     pageSize: pageSize,
  //     keyword: '',
  //     orderBy: event.sortField
  //       ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
  //       : '',
  //   };
  //   this.FieldOfActivityService.getByCondition(this.paging).subscribe({
  //     next: (res) => {
  //       this.datas = res.data;
  //       this.dataTotalRecords = res.totalFilter;
  //     },
  //     error: (e) => {
  //       Utils.messageError(this.messageService, e.message);
  //       this.loading = false;
  //     },
  //     complete: () => {
  //       this.loading = false;
  //     },
  //   });
  // };

  // onSubmitSearch = () => {
  //   this.paging.keyword = this.keyWord;
  //   this.FieldOfActivityService.getByCondition(this.paging).subscribe({
  //     next: (res) => {
  //       this.datas = res.data;
  //       this.dataTotalRecords = res.totalFilter;
  //     },
  //     error: (e) => {
  //       Utils.messageError(this.messageService, e.message);
  //       this.loading = false;
  //     },
  //     complete: () => {
  //       this.loading = false;
  //     },
  //   });
  // };

  f = (name: string, subName: string = ''): FormControl => {
    return (
      subName
        ? this.FormFieldOfActivity?.get(name)?.get(subName)
        : this.FormFieldOfActivity?.get(name)
    ) as FormControl;
  };


  getAll(){
    this.FieldOfActivityService.GetAll().subscribe((res:any)=>{
      this.listFormFieldOfActivity = res
      console.log(this.listFormFieldOfActivity)
    })
  }


  onSubmit = () => {
    if (this.FormFieldOfActivity.invalid) return;
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
   
    this.FormFieldOfActivity.controls['MaLoaiHinh'].setValue(data.maLoaiHinh);
    this.FormFieldOfActivity.controls['TenLoaiHinh'].setValue(data.tenLoaiHinh);
    this.FormFieldOfActivity.controls['MoTa'].setValue(data.moTa);
  }

  SaveAdd() {
    const ObjFieldOfActivity = this.FormFieldOfActivity.value;
    // this.FieldOfActivityService.create(ObjFieldOfActivity).subscribe({
    //   next: (res) => {
    //     if (res != null) {
    //       this.messageService.add({
    //         severity: 'success',
    //         summary: 'Thành Công',
    //         detail: 'Thêm thành Công !',
    //       });
    //       this.table.reset();
    //       this.FormFieldOfActivity.reset();
    //       this.visible = false;
    //     } else {
    //       this.messageService.add({
    //         severity: 'error',
    //         summary: 'Lỗi',
    //         detail: 'Lỗi vui Lòng kiểm tra lại !',
    //       });
    //     }
    //   },
    // });
  }

  SaveEdit() {
    const ObjFieldOfActivity = this.FormFieldOfActivity.value;
  
    // this.FieldOfActivityService.update(ObjFieldOfActivity).subscribe({
    //   next: (res: any) => {
    //     if (res.success == true) {
    //       this.messageService.add({
    //         severity: 'success',
    //         summary: 'Thành Công',
    //         detail: 'Cập nhật Thành Công !',
    //       });
    //       this.table.reset();
    //       this.FormFieldOfActivity.reset();
    //       this.visible = false;
    //     }
    //   },
    // });
  }

  Delete(data: any) {
    // this.confirmationService.confirm({
    //   message: `Bạn có chắc chắn muốn xoá không?`,
    //   header: 'delete',
    //   icon: 'pi pi-exclamation-triangle',
    //   accept: () => {
    //     this.FieldOfActivityService.delete(data.id).subscribe((res: any) => {
    //       if (res.success == true)
    //         this.messageService.add({
    //           severity: 'success',
    //           summary: 'Thành Công',
    //           detail: 'Xoá Thành Công !',
    //         });
    //       this.table.reset();
    //       this.FormFieldOfActivity.reset();
    //     });
    //   },
    // });
  }
//   confirmDeleteMultiple() {
//     let ids: number[] = [];
//     this.selectedFieldOfActivity.forEach((el) => {
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

