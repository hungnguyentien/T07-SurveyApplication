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

import { GeneralInfo, TinhQuanHuyen } from '@app/models';
import { jsonDataFake } from './json';

@Component({
  selector: 'app-general-info',
  templateUrl: './general-info.component.html',
  styleUrls: ['./general-info.component.css'],
})
export class GeneralInfoComponent {
  generalInfo?: GeneralInfo;
  frmGeneralInfo!: FormGroup;
  submitCount!: number;
  submitted!: boolean;
  loading!: boolean;

  tinh: any[] | undefined;
  selectedTinh: string | undefined;

  quanHuyen: any[] | undefined;
  selectedQuanHuyen: string | undefined;

  phuongXa: any[] | undefined;
  selectedPhuongXa: string | undefined;

  dataArr: any[] | undefined;

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
    this.tinh = [];
    this.quanHuyen = [];
    this.phuongXa = [];
    this.dataArr = Object.entries(jsonDataFake);
    this.dataArr.forEach((el, i) => {
      let tinhQuanHuyen = el.at(1) as TinhQuanHuyen;
      this.tinh?.push({ name: tinhQuanHuyen.name, code: tinhQuanHuyen.code });
    });
    // this.selectedTinh = '10';
    this.frmGeneralInfo = this.formBuilder.group({
      DonVi: this.formBuilder.group({
        TenDonVi: ['', Validators.required],
        Tinh: ['', Validators.required],
        QuanHuyen: [''],
        PhuongXa: [''],
        DiaChi: ['', Validators.required],
        MaLoaiHinh: ['', Validators.required],
        MaLinhVuc: ['', Validators.required],
        MaSoThue: [''],
        WebSite: [
          '',
          [
            Validators.required,
            Validators.pattern(
              /((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/
            ),
          ],
        ],
        Email: ['', [Validators.required, Validators.email]],
        SoDienThoai: [
          '',
          [
            Validators.required,
            Validators.pattern(
              /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/
            ),
          ],
        ],
      }),
      NguoiDaiDien: this.formBuilder.group({
        HoTen: ['', Validators.required],
        ChucVu: ['', Validators.required],
        Email: ['', [Validators.required, Validators.email]],
        SoDienThoai: [
          '',
          [
            Validators.required,
            Validators.pattern(
              /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/
            ),
          ],
        ],
      }),
    });
  }

  f = (name: string, subName: string): FormControl => {
    return (
      subName
        ? this.frmGeneralInfo?.get(name)?.get(subName)
        : this.frmGeneralInfo?.get(name)
    ) as FormControl;
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

  handlerChangeTinh = (e: any) => {
    this.quanHuyen = [];
    this.phuongXa = [];
    if (e) {
      if (e.value) {
        this.setDiaChi();
        let arr = Object.entries(
          this.dataArr?.find((x) => x.at(0) == e.value).at(1)['quan-huyen']
        );
        arr.forEach((el, i) => {
          let tinhQuanHuyen = el.at(1) as TinhQuanHuyen;
          this.quanHuyen?.push({
            name: tinhQuanHuyen.name,
            code: tinhQuanHuyen.code,
          });
        });
      }
    }
  };

  handlerChangeQuanHuyen = (e: any) => {
    this.phuongXa = [];
    if (e) {
      if (e.value) {
        this.setDiaChi();
        let arr = Object.entries(
          this.dataArr?.find((x) => x.at(0) == this.selectedTinh).at(1)[
            'quan-huyen'
          ][this.selectedQuanHuyen ?? '']['xa-phuong']
        );
        arr.forEach((el, i) => {
          let tinhQuanHuyen = el.at(1) as TinhQuanHuyen;
          this.phuongXa?.push({
            name: tinhQuanHuyen.name,
            code: tinhQuanHuyen.code,
          });
        });
      }
    }
  };

  handlerChangePhuongXa = (e: any) => {
    this.setDiaChi();
  };

  setDiaChi = () => {
    let arr = [
      this.dataArr?.find((x) => x.at(0) == this.selectedTinh).at(1)?.['name_with_type'],
      this.dataArr?.find((x) => x.at(0) == this.selectedTinh).at(1)?.[
        'quan-huyen'
      ]?.[this.selectedQuanHuyen ?? '']?.['name_with_type'],
      this.dataArr?.find((x) => x.at(0) == this.selectedTinh).at(1)?.[
        'quan-huyen'
      ]?.[this.selectedQuanHuyen ?? '']?.['xa-phuong']?.[
        this.selectedPhuongXa ?? ''
      ]?.['name_with_type'],
    ];
    this.frmGeneralInfo
      ?.get('DonVi')
      ?.get('DiaChi')
      ?.setValue(arr.filter((x) => x).join(' ,'));
  };

  resetForm = () => {
    return this.frmGeneralInfo?.reset();
  };
}
