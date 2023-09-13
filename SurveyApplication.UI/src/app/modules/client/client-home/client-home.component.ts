import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';
import { MessageService } from 'primeng/api';

import {
  GeneralInfo,
  LinhVucHoatDong,
  UnitType,
} from '@app/models';
import Utils from '@app/helpers/utils';
import { UnitTypeService } from '@app/services/unit-type.service';
import { LinhVucHoatDongService, PhieuKhaoSatService } from '@app/services';

@Component({
  selector: 'app-client-home',
  templateUrl: './client-home.component.html',
  styleUrls: ['./client-home.component.css'],
})
export class ClientHomeComponent {
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

  lstLoaiHinhDonVi: UnitType[] | undefined;

  lstLinhVuc: LinhVucHoatDong[] | undefined;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private loaiHinhDonViService: UnitTypeService,
    private phieuKhaoSatService: PhieuKhaoSatService,
    private linhVucHoatDongService: LinhVucHoatDongService
  ) {}

  ngOnInit() {
    this.submitCount = 0;
    this.submitted = false;
    this.loading = false;
    this.quanHuyen = [];
    this.phuongXa = [];
    this.phieuKhaoSatService.getTinh().subscribe({
      next: (res) => {
        this.tinh = res;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });

    this.frmGeneralInfo = this.formBuilder.group({
      DonVi: this.formBuilder.group({
        TenDonVi: ['', Validators.required],
        Tinh: ['', Validators.required],
        QuanHuyen: [''],
        PhuongXa: [''],
        DiaChi: ['', Validators.required],
        IdLoaiHinh: ['', Validators.required],
        IdLinhVuc: ['', Validators.required],
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

    this.loaiHinhDonViService.getAll().subscribe({
      next: (res) => {
        this.loading = true;
        this.lstLoaiHinhDonVi = res;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });

    this.linhVucHoatDongService.getAll().subscribe({
      next: (res) => {
        this.loading = true;
        this.lstLinhVuc = res;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  }

  f = (name: string, subName: string = ''): FormControl => {
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

    Utils.messageSuccess(
      this.messageService,
      'Nhập thông tin chung thành công!'
    );
    setTimeout(() => {
      this.router.navigateByUrl('/phieu/thong-tin-khao-sat', {
        state: this.generalInfo,
      });
    }, 3000);
  };

  resetForm = () => Utils.resetForm(this.frmGeneralInfo);

  handlerChangeTinh = (e: any) => {
    this.quanHuyen = [];
    this.phuongXa = [];
    if (e) {
      if (e.value) {
        this.setDiaChi();
        this.phieuKhaoSatService.getQuanHuyen(e.value).subscribe({
          next: (res) => {
            this.quanHuyen = res;
          },
          error: (e) => {
            Utils.messageError(this.messageService, e.message);
            this.loading = false;
          },
          complete: () => {
            this.loading = false;
          },
        });
      }
    }
  };

  handlerChangeQuanHuyen = (e: any) => {
    this.phuongXa = [];
    if (e) {
      if (e.value) {
        this.setDiaChi();
        this.phieuKhaoSatService.getPhuongXa(e.value).subscribe({
          next: (res) => {
            this.phuongXa = res;
          },
          error: (e) => {
            Utils.messageError(this.messageService, e.message);
            this.loading = false;
          },
          complete: () => {
            this.loading = false;
          },
        });
      }
    }
  };

  handlerChangePhuongXa = (e: any) => {
    this.setDiaChi();
  };

  setDiaChi = () => {
    let arr = [
      this.tinh?.find((x) => x.code == this.selectedTinh)?.['name_with_type'],
      this.quanHuyen?.find((x) => x.code == this.selectedQuanHuyen)?.[
        'name_with_type'
      ],
      this.phuongXa?.find((x) => x.code == this.selectedPhuongXa)?.[
        'name_with_type'
      ],
    ];

    this.frmGeneralInfo
      ?.get('DonVi')
      ?.get('DiaChi')
      ?.setValue(arr.filter((x) => x).join(' ,'));
  };
}
