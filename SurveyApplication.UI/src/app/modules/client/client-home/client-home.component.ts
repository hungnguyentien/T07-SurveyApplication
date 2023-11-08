import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';

import {
  GeneralInfo,
  HanhChinhVn,
  LinhVucHoatDong,
  UnitType,
  UpdateDoanhNghiep,
} from '@app/models';
import { PhieuKhaoSatService } from '@app/services';
import Utils from '@app/helpers/utils';
import { lstRegExp } from '@app/helpers';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';

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

  tinh: HanhChinhVn[] | undefined;
  selectedTinh: number | undefined;

  quanHuyen: HanhChinhVn[] | undefined;
  selectedQuanHuyen: number | undefined;

  phuongXa: HanhChinhVn[] | undefined;
  selectedPhuongXa: number | undefined;

  dataArr: any[] | undefined;

  lstLoaiHinhDonVi: UnitType[] | undefined;
  selectedLoaiHinhDonVi: number | undefined;

  lstLinhVuc: LinhVucHoatDong[] | undefined;
  selectedLinhVuc: number | undefined;
  showBtnReset: boolean = true;
  dataCheck: UpdateDoanhNghiep | undefined;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
    private phieuKhaoSatService: PhieuKhaoSatService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    let data = this.activatedRoute.snapshot.paramMap.get('data') ?? '';
    !data &&
      this.router.navigate(['/error-500'], {
        queryParams: { message: 'Không tìm thấy dữ liệu' },
      });
    this.submitCount = 0;
    this.submitted = false;
    this.spinner.show();
    this.phieuKhaoSatService.getTinh().subscribe({
      next: (res) => {
        this.tinh = res;
      },
      error: (e) => {
        this.spinner.hide();
      },
      complete: () => {
        this.spinner.hide();
      },
    });

    this.phieuKhaoSatService.getQuanHuyen().subscribe({
      next: (res) => {
        this.quanHuyen = res;
      },
    });

    this.phieuKhaoSatService.getPhuongXa().subscribe({
      next: (res) => {
        this.phuongXa = res;
      },
    });

    this.frmGeneralInfo = this.formBuilder.group({
      donVi: this.formBuilder.group({
        tenDonVi: ['', Validators.required],
        idTinhTp: [''],
        idQuanHuyen: [''],
        idXaPhuong: [''],
        diaChi: ['', Validators.required],
        idLoaiHinh: ['', Validators.required],
        idLinhVuc: ['', Validators.required],
        maSoThue: [''],
        webSite: [
          '',
          // [Validators.required, Validators.pattern(lstRegExp.webSite)],
        ],
        email: ['', [Validators.required, Validators.email]],
        soDienThoai: [
          '',
          [Validators.required, Validators.pattern(lstRegExp.soDienThoai)],
        ],
      }),
      nguoiDaiDien: this.formBuilder.group({
        hoTen: ['', Validators.required],
        chucVu: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        soDienThoai: [
          '',
          [Validators.required, Validators.pattern(lstRegExp.soDienThoai)],
        ],
      }),
      idGuiEmail: [''],
    });

    this.phieuKhaoSatService.getAllLoaiHinhDonVi().subscribe({
      next: (res) => {
        this.spinner.show();
        this.lstLoaiHinhDonVi = res;
      },
      error: (e) => {
        this.spinner.hide();
      },
      complete: () => {
        this.spinner.hide();
      },
    });

    this.phieuKhaoSatService.getAllLinhVucHoatDong().subscribe({
      next: (res) => {
        this.spinner.show();
        this.lstLinhVuc = res;
      },
      error: (e) => {
        this.spinner.hide();
      },
      complete: () => {
        this.spinner.hide();
      },
    });

    this.phieuKhaoSatService.getGeneralInfo(data).subscribe({
      next: (res) => {
        this.spinner.show();
        this.generalInfo = res;
        this.generalInfo.data = data;
        Utils.setValueFormNetted(
          this.frmGeneralInfo,
          'donVi',
          Object.keys(res.donVi),
          Object.values(res.donVi),
          false
        );
        Utils.setValueFormNetted(
          this.frmGeneralInfo,
          'nguoiDaiDien',
          Object.keys(res.nguoiDaiDien),
          Object.values(res.nguoiDaiDien),
          false
        );
        this.selectedLoaiHinhDonVi = res.donVi.idLoaiHinh;
        this.selectedLinhVuc = res.donVi.idLinhVuc;
        this.selectedTinh = res.donVi.idTinhTp;
        this.selectedQuanHuyen = res.donVi.idQuanHuyen;
        this.selectedPhuongXa = res.donVi.idXaPhuong;
        //this.frmGeneralInfo.disable();
        this.generalInfo.trangThaiKq === 2 && this.frmGeneralInfo.disable();
        this.showBtnReset = this.generalInfo.trangThaiKq !== 2;
        this.dataCheck = this.frmGeneralInfo.value as UpdateDoanhNghiep;
      },
      error: (e) => {
        this.spinner.hide();
      },
      complete: () => {
        this.spinner.hide();
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
    if (this.frmGeneralInfo.invalid) return;
    let data = this.frmGeneralInfo.value as UpdateDoanhNghiep;
    if (
      Utils.shallowObjectEqual(data.donVi, this.dataCheck?.donVi) &&
      Utils.shallowObjectEqual(data.nguoiDaiDien, this.dataCheck?.nguoiDaiDien)
    ) {
      setTimeout(() => {
        this.router.navigateByUrl('/phieu/khao-sat-doanh-nghiep', {
          state: this.generalInfo,
        });
      }, 500);
    } else {
      data.idGuiEmail = this.activatedRoute.snapshot.paramMap.get('data') ?? '';
      this.phieuKhaoSatService.updateDoanhNghiep(data).subscribe({
        next: (res) => {
          res.success && Utils.messageSuccess(this.messageService, res.message);
          setTimeout(() => {
            this.router.navigateByUrl('/phieu/khao-sat-doanh-nghiep', {
              state: this.generalInfo,
            });
          }, 3000);
        },
      });
    }
  };

  resetForm = () => Utils.resetForm(this.frmGeneralInfo);

  handlerChangeTinh = (e: any) => {
    this.quanHuyen = [];
    this.phuongXa = [];
    if (e) {
      if (e.value) {
        this.setDiaChi();
        // this.phieuKhaoSatService.getQuanHuyen(e.value).subscribe({
        //   next: (res) => {
        //     this.quanHuyen = res;
        //   },
        //   error: (e) => {
        //     this.loading = false;
        //   },
        //   complete: () => {
        //     this.loading = false;
        //   },
        // });
      }
    }
  };

  handlerChangeQuanHuyen = (e: any) => {
    this.phuongXa = [];
    if (e) {
      if (e.value) {
        this.setDiaChi();
        // this.phieuKhaoSatService.getPhuongXa(e.value).subscribe({
        //   next: (res) => {
        //     this.phuongXa = res;
        //   },
        //   error: (e) => {
        //     this.loading = false;
        //   },
        //   complete: () => {
        //     this.loading = false;
        //   },
        // });
      }
    }
  };

  handlerChangePhuongXa = (e: any) => {
    this.setDiaChi();
  };

  setDiaChi = () => {
    // let arr = [
    //   this.tinh?.find((x) => x.code == this.selectedTinh)?.['name_with_type'],
    //   this.quanHuyen?.find((x) => x.code == this.selectedQuanHuyen)?.[
    //     'name_with_type'
    //   ],
    //   this.phuongXa?.find((x) => x.code == this.selectedPhuongXa)?.[
    //     'name_with_type'
    //   ],
    // ];
    // this.frmGeneralInfo
    //   ?.get('DonVi')
    //   ?.get('DiaChi')
    //   ?.setValue(arr.filter((x) => x).join(' ,'));
  };
}
