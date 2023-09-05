import { Component } from '@angular/core';
import { TableSurvey } from '@app/models';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TableSurveyService } from '@app/services/table-survey.service';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-admin-table-survey',
  templateUrl: './admin-table-survey.component.html',
  styleUrls: ['./admin-table-survey.component.css']
})
export class AdminTableSurveyComponent {
  selectedTableSurvey!: TableSurvey[];
  datas : TableSurvey [] = [];

  first: number = 0;
  TotalCount: number = 0;
  pageIndex: number = 1;
  pageSize: number = 5;
  keyword: string = '';
  
  showadd : boolean= false;
  FormTableSurvey!: FormGroup;
  MaLoaiHinh !:string
  IdLoaiHinh !:string

  DSLoaiHinh: any[] = [];
  DSDotKhaoSat :any[]=[];
  showHeader: boolean = true;

  constructor(private FormBuilder:FormBuilder, private TableSurveyService:TableSurveyService,private messageService: MessageService,private confirmationService: ConfirmationService) {}
  ngOnInit() {
    this.GetTableSurvey() 
    this.LoadUnitType()
    this.LoadPeriodSurvey()
    this.FormTableSurvey = this.FormBuilder.group({
      MaBangKhaoSat: [''],
      MaLoaiHinh: ['', Validators.required],
      MaDotKhaoSat: ['', Validators.required],
      TenBangKhaoSat: ['', Validators.required],
      MoTa: ['', Validators.required],
      NgayBatDau: ['', Validators.required],
      NgayKetThuc: ['', Validators.required],
    });
  }


  onPageChange(event: any) {
    this.first = event.first;
    this.pageSize = event.rows;
    this.pageIndex = event.page + 1;
    this.GetTableSurvey();
  }

  
  
  LoadUnitType() {

    this.TableSurveyService.GetAllUnitType().subscribe((data) => {
      this.DSLoaiHinh = data; // Lưu dữ liệu vào danh sách

    });
  }
  LoadPeriodSurvey() {

    this.TableSurveyService.GetAllPeriodSurvey().subscribe((data) => {
      this.DSDotKhaoSat = data; // Lưu dữ liệu vào danh sách

    });
  }

  GetTableSurvey() {
    this.TableSurveyService.SearchTableSurvey(this.pageIndex, this.pageSize, this.keyword)
      .subscribe((response: any) => {
        this.datas = response.data;
        this.TotalCount = response.pageCount;
        
      });
  }

  toggleHeader() {
    this.showHeader = !this.showHeader; // Đảo ngược giá trị của biến showHeader
}
  Add(){
    this.showadd = true;
  }
  Edit(data:any){
   
    this.showadd = false;
    this.IdLoaiHinh = data.id;
    this.MaLoaiHinh = data.maLoaiHinh;
    this.FormTableSurvey.controls['MaLoaiHinh'].setValue(data.maLoaiHinh)
    this.FormTableSurvey.controls['TenLoaiHinh'].setValue(data.tenLoaiHinh)
    this.FormTableSurvey.controls['MoTa'].setValue(data.moTa)
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

    debugger
    if(this.FormTableSurvey.valid){
      const ObjTableSurvey = this.FormTableSurvey.value; 
      this.TableSurveyService.Insert(ObjTableSurvey).subscribe({
        next:(res) => {
        
          if(res != null){
            this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Thêm thành Công !'});
            this.GetTableSurvey();  
            this.FormTableSurvey.reset();
          }else{
            this.messageService.add({severity:'error', summary: 'Lỗi', detail:'Lỗi vui Lòng kiểm tra lại !'});
          }
        }
      });
    }
  }
  SaveEdit(){
    
    const ObjTableSurvey = this.FormTableSurvey.value; 
    ObjTableSurvey['id'] = this.IdLoaiHinh;
    ObjTableSurvey['maLoaiHinh'] = this.MaLoaiHinh;
    this.TableSurveyService.Update(ObjTableSurvey).subscribe({
      next:(res) => {
        debugger
        if(res ==null){
          this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Cập nhật Thành Công !'});  
          this.GetTableSurvey(); 
          this.FormTableSurvey.reset();
          console.log(res)
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
        debugger
        this.TableSurveyService.Delete(data.id).subscribe((res:any) =>{
          debugger
          if(res.success == true)
          this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Xoá Thành Công !'});  
          this.GetTableSurvey(); 
          this.FormTableSurvey.reset();
        })
      }
    });
  }
}
