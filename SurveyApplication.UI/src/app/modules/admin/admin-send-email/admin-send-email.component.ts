import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators,FormBuilder } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-admin-send-email',
  templateUrl: './admin-send-email.component.html',
  styleUrls: ['./admin-send-email.component.css']
})
export class AdminSendEmailComponent {

  selectedObjectSurvey:any =[]
  datas:any =[]
  first = 0;
  pageSize = 5; 
  pageIndex = 1; 
  TotalCount = 0; 
  keyword = '';
  
  showadd!: boolean;
  FormObjectSurvey!: FormGroup;
  IdMaLoaiHinh !:string
  IdLoaiHinh !:string


  selectedCountry: string | undefined;
  constructor(private FormBuilder :FormBuilder,private messageService: MessageService,private confirmationService: ConfirmationService) {}
  ngOnInit() {

    this.FormObjectSurvey = new FormGroup({
      // MaDonVi: new FormControl(''),
      MaLoaiHinh: new FormControl('', Validators.required),
      MaLinhVuc: new FormControl('', Validators.required),
      TenDonVi: new FormControl('', Validators.required),
      MaSoThue: new FormControl('', Validators.required),
      Email: new FormControl('', Validators.required),
      WebSite: new FormControl('', Validators.required),
      SoDienThoai: new FormControl('', Validators.required),
      DiaChi: new FormControl(''),

    });
  }
}
