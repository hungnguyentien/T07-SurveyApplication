import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators,FormBuilder } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-admin-send-email',
  templateUrl: './admin-send-email.component.html',
  styleUrls: ['./admin-send-email.component.css']
})
export class AdminSendEmailComponent {

  selectedSendEmail:any =[]
  datas:any =[]
  first = 0;
  pageSize = 5; 
  pageIndex = 1; 
  TotalCount = 0; 
  keyword = '';

  selectedCountry: string | undefined;
  constructor(private FormBuilder :FormBuilder,private messageService: MessageService,private confirmationService: ConfirmationService) {}
  ngOnInit() {
  }
}
