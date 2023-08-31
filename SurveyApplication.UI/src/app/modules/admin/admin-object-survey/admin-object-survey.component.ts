import { Component } from '@angular/core';
import { ObjectSurvey , Representative } from '@app/models';
import { UnitTypeService } from '@app/services/unit-type.service';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { ObjectSurveyService } from '@app/services/object-survey.service';

@Component({
  selector: 'app-admin-object-survey',
  templateUrl: './admin-object-survey.component.html',
  styleUrls: ['./admin-object-survey.component.css']
})
export class AdminObjectSurveyComponent {
  selectedObjectSurvey!: ObjectSurvey[];
  datas : ObjectSurvey [] = [];

  first: number = 0;
  TotalCount: number = 0;
  pageIndex: number = 1;
  pageSize: number = 5;
  keyword: string = '';
  
  showadd!: boolean;
  FormObjectSurvey!: FormGroup;
  MaLoaiHinh !:string
  IdLoaiHinh !:string
  listloaihinhdonvi: any[] = []

  combinedArray: any[] = [];
  cities: any[] = [];
  districts: any[] = [];
  wards: any[] = []


  selectedCountry: string | undefined;
  constructor(private FormBuilder :FormBuilder,private ObjectSurveyService:ObjectSurveyService,private messageService: MessageService,private confirmationService: ConfirmationService) {}
  ngOnInit() {
    this.GetObjectSurvey()
    this.GetUnitType()
    this.FormObjectSurvey = new FormGroup({
      // MaDonVi: new FormControl(''),
      MaLoaiHinh: new FormControl('', Validators.required),
      MaLinhVuc: new FormControl('', Validators.required),
      TenDonVi: new FormControl('', Validators.required),
      MaSoThue: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      WebSite: new FormControl('', Validators.required),
      SoDienThoai: new FormControl('', Validators.required),
      DiaChi: new FormGroup({
        TinhThanh: new FormControl(''),
        QuanHuyen: new FormControl(''),
        PhuongXa: new FormControl(''),
        DiaChiChiTiet: new FormControl('')
      }),
      listRepresentative: new FormGroup({
        // MaNguoiDaiDien: new FormControl(''),
        HoTen: new FormControl('', Validators.required),
        ChucVu: new FormControl('', Validators.required),
        Email: new FormControl('', Validators.required),
        SoDienThoai: new FormControl('', Validators.required),
        MoTa: new FormControl('')
      })
    });
    this.ObjectSurveyService.getCities().subscribe((data: any) => {
      this.cities = data;
    });
  }
  onCityChange(event: any): void {
    debugger
    const selectedCityId = event.target.value;
    const selectedCity = this.cities.find(city => city.Id === selectedCityId);
    this.districts = selectedCity?.Districts || [];
    this.wards = [];
    this.combineArrays();
  }

  onDistrictChange(event: any): void {
  debugger
  const selectedDistrictId: string = event.target.value; 
  const selectedCityId: string | undefined = this.cities.find(city => city.Districts.some((district: { Id: string; }) => district.Id === selectedDistrictId))?.Id;

  const selectedCity = this.cities.find(city => city.Id === selectedCityId);
  const selectedDistrict = selectedCity?.Districts.find((district: { Id: string; }) => district.Id === selectedDistrictId);

  this.wards = selectedDistrict?.Wards || [];
  this.combineArrays();
  }
  combineArrays(): void {
    debugger
    this.combinedArray = [...this.cities, ...this.districts, ...this.wards];
  }

  getCombinedArrayAsString(): string {
    debugger
    return this.combinedArray.map(item => item.Id).join(',');
  }



  GetUnitType(){
    this.ObjectSurveyService.GetUnitType()
    .subscribe((response: any) => {
      this.listloaihinhdonvi = response;
      this.TotalCount = response.totalItems;
      
    });
  }
  
  onPageChange(event: any) {
    debugger
    this.first = event.first;
    this.pageSize = event.rows;
    this.pageIndex = event.page + 1;
    this.GetObjectSurvey();
  }

  GetObjectSurvey() {
    this.ObjectSurveyService.SearchObjectSurvey(this.pageIndex, this.pageSize, this.keyword)
      .subscribe((response: any) => {
        this.datas = response;
        this.TotalCount = response.totalItems;
        
      });
  }
  Add(){
    this.showadd = true;
  }
  Edit(data:any){
    debugger
    this.showadd = false;
    this.IdLoaiHinh = data.id;
    this.MaLoaiHinh = data.maLoaiHinh;
    this.FormObjectSurvey.controls['MaLoaiHinh'].setValue(data.maLoaiHinh)
    this.FormObjectSurvey.controls['TenLoaiHinh'].setValue(data.tenLoaiHinh)
    this.FormObjectSurvey.controls['MoTa'].setValue(data.moTa)
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
    debugger
    if(this.FormObjectSurvey.valid){
      const ObjObjectSurvey = this.FormObjectSurvey.value; 
      console.log(ObjObjectSurvey)
      this.ObjectSurveyService.Insert(ObjObjectSurvey).subscribe({
        next:(res) => {
        
          if(res != null){
            this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Thêm thành Công !'});
            this.GetObjectSurvey();  
            this.FormObjectSurvey.reset();
          }else{
            this.messageService.add({severity:'error', summary: 'Lỗi', detail:'Lỗi vui Lòng kiểm tra lại !'});
          }
        }
      });
    }
  }
  SaveEdit(){
    debugger
    const ObjObjectSurvey = this.FormObjectSurvey.value; 
    ObjObjectSurvey['id'] = this.IdLoaiHinh;
    ObjObjectSurvey['maLoaiHinh'] = this.MaLoaiHinh;
    this.ObjectSurveyService.Update(ObjObjectSurvey).subscribe({
      next:(res) => {
        debugger
        if(res ==null){
          this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Cập nhật Thành Công !'});  
          this.GetObjectSurvey(); 
          this.FormObjectSurvey.reset();
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
        this.ObjectSurveyService.Delete(data.id).subscribe((res:any) =>{
          if(res.success == true)
          this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Xoá Thành Công !'});  
          this.GetObjectSurvey(); 
          this.FormObjectSurvey.reset();
          console.log(res)
          
        })
      }
    });
  }

}
