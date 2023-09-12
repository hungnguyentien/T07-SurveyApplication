import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators ,FormBuilder} from '@angular/forms';
import { Paging, QuanHuyen } from '@app/models';
import { TinhQuanHuyenService } from '@app/services/tinh-quan-huyen.service';
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
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  
  submitted: boolean = false;
  first = 0;
  showadd!: boolean;
  FormQuanHuyen!: FormGroup;
  listFormQuanHuyen!: [];
  visible: boolean = false;
  constructor(
    private FormBuilder : FormBuilder,
    private QuanHuyenService: TinhQuanHuyenService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  ngOnInit() {
    this.loading = true;
    this.createForm();
    this.getAll();
  }

  createForm = () => {
    this.FormQuanHuyen = this.FormBuilder.group({
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
  //   this.QuanHuyenService.getByCondition(this.paging).subscribe({
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
  //   this.QuanHuyenService.getByCondition(this.paging).subscribe({
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
        ? this.FormQuanHuyen?.get(name)?.get(subName)
        : this.FormQuanHuyen?.get(name)
    ) as FormControl;
  };


  getAll(){
    this.QuanHuyenService.GetAllHuyen().subscribe((res:any)=>{
      this.listFormQuanHuyen = res
    })
  }

  
  onSubmit = () => {
    if (this.FormQuanHuyen.invalid) return;
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
   
    this.FormQuanHuyen.controls['MaLoaiHinh'].setValue(data.maLoaiHinh);
    this.FormQuanHuyen.controls['TenLoaiHinh'].setValue(data.tenLoaiHinh);
    this.FormQuanHuyen.controls['MoTa'].setValue(data.moTa);
  }

  SaveAdd() {
    const ObjQuanHuyen = this.FormQuanHuyen.value;
    // this.QuanHuyenService.create(ObjQuanHuyen).subscribe({
    //   next: (res) => {
    //     if (res != null) {
    //       this.messageService.add({
    //         severity: 'success',
    //         summary: 'Thành Công',
    //         detail: 'Thêm thành Công !',
    //       });
    //       this.table.reset();
    //       this.FormQuanHuyen.reset();
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
    const ObjQuanHuyen = this.FormQuanHuyen.value;
  
    // this.QuanHuyenService.update(ObjQuanHuyen).subscribe({
    //   next: (res: any) => {
    //     if (res.success == true) {
    //       this.messageService.add({
    //         severity: 'success',
    //         summary: 'Thành Công',
    //         detail: 'Cập nhật Thành Công !',
    //       });
    //       this.table.reset();
    //       this.FormQuanHuyen.reset();
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
    //     this.QuanHuyenService.delete(data.id).subscribe((res: any) => {
    //       if (res.success == true)
    //         this.messageService.add({
    //           severity: 'success',
    //           summary: 'Thành Công',
    //           detail: 'Xoá Thành Công !',
    //         });
    //       this.table.reset();
    //       this.FormQuanHuyen.reset();
    //     });
    //   },
    // });
  }
//   confirmDeleteMultiple() {
//     let ids: number[] = [];
//     this.selectedQuanHuyen.forEach((el) => {
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
