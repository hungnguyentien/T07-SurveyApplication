import { Component } from '@angular/core';
import { ServiceService } from '@app/services';
import { Customer, Representative, UnitType } from '@app/models';
import { UnitTypeService } from '@app/services/unit-type.service';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-admin-unit-type',
  templateUrl: './admin-unit-type.component.html',
  styleUrls: ['./admin-unit-type.component.css'],
})
export class AdminUnitTypeComponent {
  selectedUnitType!: UnitType[];
  datas : UnitType [] = [];

  first: number = 0;
  TotalCount: number = 0;
  pageIndex: number = 1;
  pageSize: number = 5;
  keyword: string = '';
  
  showadd!: boolean;
  FormUnitType!: FormGroup;
  MaLoaiHinh !:string

  constructor(private FormBuilder :FormBuilder,private UnitTypeService:UnitTypeService,private messageService: MessageService) {}
  ngOnInit() {
    this.GetUnitType()

    this.FormUnitType = this.FormBuilder.group({
      MaLoaiHinh: [{ value: this.MaLoaiHinh, disabled: true },'', Validators.required],
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

  GetUnitType() {
    console.log(this.keyword)
    debugger
    this.UnitTypeService.SearchUnitType(this.pageIndex, this.pageSize, this.keyword)
      .subscribe((response: any) => {
        this.datas = response;
        this.TotalCount = response.totalItems;
        
      });
  }
  Add(){
    debugger
    this.showadd = true;
    this.UnitTypeService.GetIdUnitType().subscribe({
      next:(res:any) => {
        debugger
        this.MaLoaiHinh = res
        console.log(this.MaLoaiHinh);
      }
    })
  }
  Edit(data:any){
    debugger
    this.showadd = false;
    this.MaLoaiHinh = data.id;
    this.FormUnitType.controls['MaLoaiHinh'].setValue(data.maLoaiHinh)
    this.FormUnitType.controls['TenLoaiHinh'].setValue(data.tenLoaiHinh)
    this.FormUnitType.controls['MoTa'].setValue(data.moTa)
  }


  Save(){
    debugger
    if(this.showadd){
      this.SaveAdd()
    }
    else{
      this.SaveEdit();
    }
  }


  SaveAdd(){
    if(this.FormUnitType.valid){
      debugger
      const ObjUnitType = this.FormUnitType.value; 
      this.UnitTypeService.Insert(ObjUnitType).subscribe({
        next:(res) => {
          console.log(res)
          debugger
          if(res != null){
            this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Thêm thành Công !'});
            this.GetUnitType();  
            this.FormUnitType.reset();
          }else{
            this.messageService.add({severity:'error', summary: 'Lỗi', detail:'Lỗi vui Lòng kiểm tra lại !'});
          }
        }
      });
    }
  }
  SaveEdit(){
    const ObjUnitType = this.FormUnitType.value; 
    ObjUnitType['id'] = this.MaLoaiHinh;
    this.UnitTypeService.Update(ObjUnitType).subscribe({
      next:(res) => {
        if(res ==null){
          this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Cập nhật Thành Công !'});  
          this.GetUnitType(); 
          this.FormUnitType.reset();
          console.log(res)
        }
      }
    }
    )
  }
}
