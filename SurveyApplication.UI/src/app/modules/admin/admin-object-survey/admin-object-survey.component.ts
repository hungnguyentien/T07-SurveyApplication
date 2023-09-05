import { Component } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ObjectSurveyService } from '@app/services/object-survey.service';
import { CreateUnitAndRep } from '@app/models/CreateUnitAndRep';

@Component({
  selector: 'app-admin-object-survey',
  templateUrl: './admin-object-survey.component.html',
  styleUrls: ['./admin-object-survey.component.css']
})
export class AdminObjectSurveyComponent {
  selectedObjectSurvey!: CreateUnitAndRep[];
  datas : CreateUnitAndRep [] = [];

  first = 0;
  pageSize = 5; 
  pageIndex = 1; 
  TotalCount = 0; 
  keyword = '';
  
  showadd!: boolean;
  FormObjectSurvey!: FormGroup;
  FormRepresentative!:FormGroup;
  IdDonVi !:string
  Madonvi!:string

  IdNguoiDaiDien !:string
  MaNguoiDaiDien !:string
  IdLoaiHinh !:string
  listloaihinhdonvi: any[] = []
  ContainerAdd: any[] = []
  
  combinedArray: any[] = [];
  cities: any[] = [];
  districts: any[] = [];
  wards: any[] = []

  visible: boolean = false;

  selectedCountry: string | undefined;
  constructor(private FormBuilder :FormBuilder,private ObjectSurveyService:ObjectSurveyService,private messageService: MessageService,private confirmationService: ConfirmationService) {}
  ngOnInit() {
    this.GetObjectSurvey()
    this.GetUnitType()
    this.FormObjectSurvey = new FormGroup({
      MaLoaiHinh: new FormControl('', Validators.required),
      MaLinhVuc: new FormControl('', Validators.required),
      TenDonVi: new FormControl('', Validators.required),
      MaSoThue: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      WebSite: new FormControl('', Validators.required),
      SoDienThoai: new FormControl('', Validators.required),
      DiaChi: new FormControl(''),
      // DiaChi: new FormGroup({
      //   TinhThanh: new FormControl(''),
      //   QuanHuyen: new FormControl(''),
      //   PhuongXa: new FormControl(''),
      //   DiaChiChiTiet: new FormControl('')
      // }),
    });
    this.FormRepresentative= new FormGroup({
      HoTen: new FormControl('', Validators.required),
      ChucVu: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      SoDienThoai: new FormControl('', Validators.required),
      MoTa: new FormControl('')
    })
    this.ObjectSurveyService.getCities().subscribe((data: any) => {
      this.cities = data;
    });
  }
  onCityChange(event: any): void {
    
    const selectedCityId = event.target.value;
    const selectedCity = this.cities.find(city => city.Id === selectedCityId);
    this.districts = selectedCity?.Districts || [];
    this.wards = [];
    this.combineArrays();
  }

  onDistrictChange(event: any): void {
  
  const selectedDistrictId: string = event.target.value; 
  const selectedCityId: string | undefined = this.cities.find(city => city.Districts.some((district: { Id: string; }) => district.Id === selectedDistrictId))?.Id;

  const selectedCity = this.cities.find(city => city.Id === selectedCityId);
  const selectedDistrict = selectedCity?.Districts.find((district: { Id: string; }) => district.Id === selectedDistrictId);

  this.wards = selectedDistrict?.Wards || [];
  this.combineArrays();
  }
  combineArrays(): void {
    
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
    this.first = event.first;
    this.pageSize = event.rows;
    this.pageIndex = event.page + 1;
    this.GetObjectSurvey();
  }

  GetObjectSurvey() {
    this.ObjectSurveyService.SearchObjectSurvey(this.pageIndex, this.pageSize, this.keyword)
      .subscribe((response: any) => {  
        debugger
        this.datas = response.data;
        this.TotalCount = response.pageCount;
      });
  }
  Add(){
    this.FormObjectSurvey.reset();
    this.FormRepresentative.reset();
    this.showadd = true;
    this.visible = !this.visible;
  }
  Edit(data:any){
    this.showadd = false;
    this.visible = !this.visible; 

    this.IdDonVi = data.idDonVi;
    this.Madonvi =data.maDonVi;
    this.MaNguoiDaiDien = data.maNguoiDaiDien
    this.IdNguoiDaiDien =data.idNguoiDaiDien
    this.FormObjectSurvey.controls['MaLoaiHinh'].setValue(data.maLoaiHinh)
    this.FormObjectSurvey.controls['TenDonVi'].setValue(data.tenDonVi)
    this.FormObjectSurvey.controls['MaSoThue'].setValue(data.maSoThue)
    this.FormObjectSurvey.controls['Email'].setValue(data.emailDonVi)
    this.FormObjectSurvey.controls['WebSite'].setValue(data.webSite)
    this.FormObjectSurvey.controls['SoDienThoai'].setValue(data.soDienThoaiDonVi)
    this.FormObjectSurvey.controls['DiaChi'].setValue(data.diaChi)
    this.FormObjectSurvey.controls['MaLinhVuc'].setValue(data.maLinhVuc)

    this.FormRepresentative.controls['HoTen'].setValue(data.hoTen)
    this.FormRepresentative.controls['ChucVu'].setValue(data.chucVu)
    this.FormRepresentative.controls['Email'].setValue(data.emailNguoiDaiDien)
    this.FormRepresentative.controls['SoDienThoai'].setValue(data.soDienThoaiNguoiDaiDien)
    this.FormRepresentative.controls['MoTa'].setValue(data.moTa)

  }
  CloseModal(){
    this.visible = false; 
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
    if(this.FormObjectSurvey.valid && this.FormRepresentative.valid){  
      const obj:CreateUnitAndRep = {
        DonViDto: this.FormObjectSurvey.value,
        NguoiDaiDienDto: this.FormRepresentative.value
      };
      this.ObjectSurveyService.Insert(obj).subscribe({
        next:(res) => {
          if(res != null){
            this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Thêm thành Công !'});
            this.GetObjectSurvey();
            this.FormObjectSurvey.reset();
            this.FormRepresentative.reset(); 
            this.visible = false; 
          }else{
            this.messageService.add({severity:'error', summary: 'Lỗi', detail:'Lỗi vui Lòng kiểm tra lại !'});
          }
        }
      });
    }
  }
  SaveEdit(){
    
    const updatedFormObjectSurveyValue = { ...this.FormObjectSurvey.value };
    updatedFormObjectSurveyValue['Id'] = this.IdDonVi;
    updatedFormObjectSurveyValue['maDonVi'] = this.Madonvi;
    const updatedFormRepresentativeValue = { ...this.FormRepresentative.value };
    updatedFormRepresentativeValue['maDonVi'] = this.IdDonVi;
    updatedFormRepresentativeValue['MaNguoiDaiDien'] = this.MaNguoiDaiDien;
    updatedFormRepresentativeValue['Id'] = this.IdNguoiDaiDien;
    const obj:CreateUnitAndRep = {
      DonViDto: updatedFormObjectSurveyValue,
      NguoiDaiDienDto: updatedFormRepresentativeValue
    };
    this.ObjectSurveyService.Update(obj).subscribe({
      next:(res) => {
        if(res != null){
          this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Cập nhật Thành Công !'});  
          this.GetObjectSurvey(); 
          this.FormObjectSurvey.reset();
          this.FormRepresentative.reset(); 
          this.visible = false; 
        }
        else {
          this.messageService.add({severity:'error', summary: 'Lỗi', detail:'Lỗi vui Lòng kiểm tra lại !'});
        }
      }
    }
    )
  }
  Delete(DonVi:any,NguoiDaiDien:any){
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.ObjectSurveyService.Delete(DonVi,NguoiDaiDien).subscribe((res:any) =>{
          if(res.success == true){
            this.messageService.add({severity:'success', summary: 'Thành Công', detail:'Xoá Thành Công !'});  
            this.GetObjectSurvey(); 
            this.FormObjectSurvey.reset();
            this.FormRepresentative.reset();          
          }
          else{
            this.messageService.add({severity:'error', summary: 'Lỗi', detail:'Lỗi vui Lòng kiểm tra lại !'});
          }
        })
      }
    });
  }

}
