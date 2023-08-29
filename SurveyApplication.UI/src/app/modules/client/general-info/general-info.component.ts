import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';
import { MessageService } from 'primeng/api';

import { GeneralInfo } from '@app/models';

@Component({
  selector: 'app-general-info',
  templateUrl: './general-info.component.html',
  styleUrls: ['./general-info.component.css'],
})
export class GeneralInfoComponent {
  generalInfo!: GeneralInfo;
  frmGeneralInfo!: FormGroup;
  submitCount!: number;
  submitted!: boolean;
  loading!: boolean;

  constructor(
    private router: Router,
    private titleService: Title,
    private formBuilder: FormBuilder,
    private messageService: MessageService
  ) {
    this.titleService.setTitle('Thông tin chung');
  }

  ngOnInit() {
    this.submitCount = 0;
    this.submitted = false;
    this.loading = false;
    this.frmGeneralInfo = this.formBuilder.group({
      CreateDonVi: this.formBuilder.group({
        TenDonVi: ['', Validators.required],
      }),
    });
  }

  f = (name: string, subName: string): FormControl => {
    return this.frmGeneralInfo?.get(name)?.get(subName) as FormControl;
  };

  onSubmit = () => {
    this.submitted = true;
    this.submitCount++;
    if (this.frmGeneralInfo.invalid) {
      return;
    }

    this.loading = true;
    this.messageService.clear();
    this.messageService.add({
      key: 'success',
      severity: 'success',
      summary: 'Success',
      detail: 'Gửi thông tin chung thành công',
    });
    setTimeout(() => {
      this.router.navigate(['/phieu/thong-tin-khao-sat']);
    }, 5000);
  };

  resetForm = () => {
    return this.frmGeneralInfo?.reset();
  };
}
