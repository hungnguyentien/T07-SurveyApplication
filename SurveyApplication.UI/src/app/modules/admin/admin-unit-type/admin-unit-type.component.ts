import { Component } from '@angular/core';
import { ServiceService } from '@app/services';
import { Customer, Representative, UnitType } from '@app/models';
import { UnitTypeService } from '@app/services/unit-type.service';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-admin-unit-type',
  templateUrl: './admin-unit-type.component.html',
  styleUrls: ['./admin-unit-type.component.css'],
})
export class AdminUnitTypeComponent {
  selectedUnitType!: UnitType[];
  datas : UnitType [] = [];

  first = 0;
  pageSize = 5; 
  pageIndex = 1; 
  TotalCount = 0; 
  keyword = '';
  
  showadd!: boolean;
  FormUnitType!: FormGroup;
  MaLoaiHinh !:string
  IdLoaiHinh !:string
  visible: boolean = false;
  constructor(private FormBuilder :FormBuilder,private UnitTypeService:UnitTypeService,private messageService: MessageService,private confirmationService: ConfirmationService) {}
  ngOnInit() {
    this.GetUnitType()
    this.FormUnitType = this.FormBuilder.group({
      MaLoaiHinh: [{ value: this.MaLoaiHinh, disabled: true },''],
      TenLoaiHinh: ['', Validators.required],
      MoTa: ['', Validators.required]
    });
  }


  onPageChange(event: any) { 
    this.first = event.first;
    this.pageSize = event.rows;
    this.pageIndex = event.page + 1;
    this.GetUnitType();
  }
  CloseModal(){
    this.visible = false; 
  }
  GetUnitType() {
    
    this.UnitTypeService.SearchUnitType(this.pageIndex, this.pageSize, this.keyword)
      .subscribe((response: any) => {
        this.datas = response.data;
        this.TotalCount = response.pageCount;        
      });
  }
  Add(){
    this.visible = !this.visible;
    this.showadd = true;
    this.FormUnitType.reset();
    this.UnitTypeService.GetIdUnitType().subscribe({
      next:(res:any) => {
        this.FormUnitType.controls['MaLoaiHinh'].setValue(res.maLoaiHinh)
        this.MaLoaiHinh = res.maLoaiHinh
        console.log(this.MaLoaiHinh);
      }
    })
  }
  Edit(data:any){
    this.visible = !this.visible;
    this.showadd = false;
    this.IdLoaiHinh = data.id;
    this.MaLoaiHinh = data.maLoaiHinh;
    this.FormUnitType.controls['MaLoaiHinh'].setValue(data.maLoaiHinh)
    this.FormUnitType.controls['TenLoaiHinh'].setValue(data.tenLoaiHinh)
    this.FormUnitType.controls['MoTa'].setValue(data.moTa)
  }


  Save(){
    
    if(this.showadd){
      this.SaveAdd()
    }
    else{
      this.SaveEdit();
    }
  }


  SaveAdd(){
    if(this.FormUnitType.valid){
      const ObjUnitType = this.FormUnitType.value; 
      ObjUnitType['maLoaiHinh'] = this.MaLoaiHinh;
      this.UnitTypeService.Insert(ObjUnitType).subscribe({
        next:(res) => {
        
          if(res != null){
            this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Thêm thành Công !'});
            this.GetUnitType();  
            this.FormUnitType.reset();
            this.visible = false; 
          }else{
            this.messageService.add({severity:'error', summary: 'Lỗi', detail:'Lỗi vui Lòng kiểm tra lại !'});
          }
        }
      });
    }
  }
  SaveEdit(){
    const ObjUnitType = this.FormUnitType.value; 
    ObjUnitType['id'] = this.IdLoaiHinh;
    ObjUnitType['maLoaiHinh'] = this.MaLoaiHinh;
    this.UnitTypeService.Update(ObjUnitType).subscribe({
      next:(res:any) => {
        if(res.success == true){
          this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Cập nhật Thành Công !'});  
          this.GetUnitType(); 
          this.FormUnitType.reset();
          this.visible = false; 
        }
      }
    }
    )
  }

  Delete(data:any){
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.UnitTypeService.Delete(data.id).subscribe((res:any) =>{
          if(res.success == true)
          this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Xoá Thành Công !'});  
          this.GetUnitType(); 
          this.FormUnitType.reset();
          
        })
      }
    });
  }

  
}
