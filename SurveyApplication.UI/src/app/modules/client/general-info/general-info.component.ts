import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';
import { MessageService } from 'primeng/api';

import {
  GeneralInfo,
  HanhChinhVn,
  LinhVucHoatDong,
  UnitType,
} from '@app/models';
import { LinhVucHoatDongService, PhieuKhaoSatService } from '@app/services';
import Utils from '@app/helpers/utils';
import { UnitTypeService } from '@app/services/unit-type.service';

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

  tinh: HanhChinhVn[] | undefined;
  selectedTinh: string | undefined;

  quanHuyen: HanhChinhVn[] | undefined;
  selectedQuanHuyen: string | undefined;

  phuongXa: HanhChinhVn[] | undefined;
  selectedPhuongXa: string | undefined;

  dataArr: any[] | undefined;

  lstLoaiHinhDonVi: UnitType[] | undefined;
  selectedLoaiHinhDonVi: number | undefined;

  lstLinhVuc: LinhVucHoatDong[] | undefined;
  selectedLinhVuc: number | undefined;
  showBtnReset: boolean = true;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private phieuKhaoSatService: PhieuKhaoSatService,
    private loaiHinhDonViService: UnitTypeService,
    private linhVucHoatDongService: LinhVucHoatDongService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    let data = this.activatedRoute.snapshot.queryParamMap.get('data') ?? '';
    !data && this.router.navigate(['/login']);
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
        Tinh: [''],
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

    this.phieuKhaoSatService.getGeneralInfo(data).subscribe({
      next: (res) => {
        this.loading = true;
        this.generalInfo = res;
        this.generalInfo.data = data;
        Utils.setValueFormNetted(
          this.frmGeneralInfo,
          'donVi',
          Object.keys(res.donVi),
          Object.values(res.donVi)
        );
        Utils.setValueFormNetted(
          this.frmGeneralInfo,
          'nguoiDaiDien',
          Object.keys(res.nguoiDaiDien),
          Object.values(res.nguoiDaiDien)
        );
        this.selectedLoaiHinhDonVi = res.donVi.idLoaiHinh;
        this.selectedLinhVuc = res.donVi.idLinhVuc;
        this.generalInfo.trangThai === 2 && this.frmGeneralInfo.disable();
        this.showBtnReset = this.generalInfo.trangThai !== 2;
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
    }, 1000);
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
        // let arr = Object.entries(
        //   this.dataArr?.find((x) => x.at(0) == e.value).at(1)['quan-huyen']
        // );
        // arr.forEach((el, i) => {
        //   let tinhQuanHuyen = el.at(1) as TinhQuanHuyen;
        //   this.quanHuyen?.push({
        //     name: tinhQuanHuyen.name,
        //     code: tinhQuanHuyen.code,
        //   });
        // });
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
        // let arr = Object.entries(
        //   this.dataArr?.find((x) => x.at(0) == this.selectedTinh).at(1)[
        //     'quan-huyen'
        //   ][this.selectedQuanHuyen ?? '']['xa-phuong']
        // );
        // arr.forEach((el, i) => {
        //   let tinhQuanHuyen = el.at(1) as TinhQuanHuyen;
        //   this.phuongXa?.push({
        //     name: tinhQuanHuyen.name,
        //     code: tinhQuanHuyen.code,
        //   });
        // });
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
