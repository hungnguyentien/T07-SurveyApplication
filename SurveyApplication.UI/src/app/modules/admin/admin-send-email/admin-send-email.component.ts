import { Component, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormArray } from '@angular/forms';
import Utils from '@app/helpers/utils';
import {
  CreateGuiEmail,
  DonVi,
  GuiEmail,
  GuiEmailBks,
  GuiLaiGuiEmail,
  Paging,
  PagingGuiEmailBks,
  ThuHoiGuiEmail,
} from '@app/models';
import { GuiEmailService, ObjectSurveyService } from '@app/services';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { GuiEmailTrangThai } from '@app/enums';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-admin-send-email',
  templateUrl: './admin-send-email.component.html',
  styleUrls: ['./admin-send-email.component.css'],
})
export class AdminSendEmailComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas!: GuiEmailBks[];
  selectedSendEmail!: GuiEmailBks[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  visible: boolean = false;
  maBangKhaoSat: string = '';
  tenBangKhaoSat: string = '';
  idBangKhaoSat: number = 0;
  trangThaiGuiEmail: number | null = null;

  @ViewChild('dtGe') tableGe!: Table;
  loadingGe: boolean = true;
  datasGe!: GuiEmail[];
  selectedGe!: GuiEmail[];
  pagingGe!: PagingGuiEmailBks;
  dataTotalRecordsGe!: number;

  activeIndex: number = 0;

  frmGuiEmail!: FormGroup;
  visibleGuiEmail: boolean = false;
  lstDonVi!: DonVi[];
  selectedDonVi!: number[];
  lstIdDonViError: boolean = false;

  frmThuHoiEmail!: FormGroup;
  visibleThuHoiEmail: boolean = false;

  trangThai: number = 0;

  constructor(
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private objectSurveyService: ObjectSurveyService,
    private guiEmailService: GuiEmailService,
    private datePipe: DatePipe
  ) {}
  ngOnInit() {}

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: '',
      orderBy: event.sortField
        ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
        : '',
    };
    this.guiEmailService
      .getByConditionTepm<GuiEmailBks>(this.paging)
      .subscribe({
        next: (res) => {
          this.datas = res.data;
          this.dataTotalRecords = res.totalFilter;
        },
        error: (e) => {
          this.loading = false;
        },
        complete: () => {
          this.loading = false;
        },
      });
  };

  detailDialog = (rowData: GuiEmailBks) => {
    
    //TODO reset popup
    this.maBangKhaoSat = '';
    this.tenBangKhaoSat = '';
    this.idBangKhaoSat = 0;
    this.activeIndex = 0;
    this.selectedGe = [];

    this.visible = true;
    this.maBangKhaoSat = rowData.maBangKhaoSat;
    this.tenBangKhaoSat = rowData.tenBangKhaoSat;
    this.idBangKhaoSat = rowData.idBangKhaoSat;
  };

  loadListLazyGe = (event: any) => {
    this.loadingGe = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.pagingGe = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: '',
      orderBy: event.sortField
        ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
        : '',
      idBanhgKhaoSat: this.idBangKhaoSat,
      trangThaiGuiEmail: this.trangThaiGuiEmail,
    };
    this.guiEmailService.getByIdBangKhaoSat(this.pagingGe).subscribe({
      next: (res: any) => {
        this.datasGe = res.data;
        this.dataTotalRecordsGe = res.totalFilter;
      },
      error: (e) => {
        this.loadingGe = false;
      },
      complete: () => {
        this.loadingGe = false;
      },
    });
  };

  activeTabIndex = (index: number) => {
    this.activeIndex = index;
    this.trangThaiGuiEmail = index != 0 ? index - 1 : null;
    this.tableGe.reset();
  };

  checkTrangThai(trangThai: number) {
    return this.selectedGe?.find(
      (x) => x.trangThai === trangThai && !x.isKhaoSat
    );
  }

  onSubmitSearch = () => {
    this.paging.keyword = this.keyWord;
    this.guiEmailService
      .getByConditionTepm<GuiEmailBks>(this.paging)
      .subscribe({
        next: (res) => {
          this.datas = res.data;
          this.dataTotalRecords = res.totalFilter;
        },
        error: (e) => {
          this.loading = false;
        },
        complete: () => {
          this.loading = false;
        },
      });
  };

  openGuiEmail = () => {
    this.visibleGuiEmail = true;
    this.selectedDonVi = [];
    if (this.frmGuiEmail) {
      this.frmGuiEmail.reset();
      this.frmGuiEmail.get('lstIdDonVi')?.reset();
      this.frmGuiEmail.get('lstBangKhaoSat')?.reset();
    }

    let noidung = 'Ấn vào đường dẫn bên dưới để thức hiện khảo sát';
    let tieuDe = `${this.tenBangKhaoSat} mã khảo sát ${
      this.maBangKhaoSat
    } gửi lại (${this.datePipe.transform(new Date(), 'dd/MM/yyyy hh:mm')})`;
    this.frmGuiEmail = this.formBuilder.group({
      noidung: [noidung, Validators.required],
      tieuDe: [tieuDe, Validators.required],
      lstIdDonVi: this.formBuilder.array([] as number[], Validators.required),
      lstBangKhaoSat: this.formBuilder.array(
        [this.idBangKhaoSat] as number[],
        Validators.required
      ),
    });

    this.objectSurveyService.getAllByObj<DonVi>().subscribe((res) => {
      this.lstDonVi = res;
      this.selectedDonVi = this.selectedGe
        .filter((x) => x.trangThai !== GuiEmailTrangThai.ThanhCong)
        .map((x) => x.idDonVi)
        .filter(Utils.onlyUnique);
    });
  };

  onSubmitGuiEmail = () => {
    this.selectedDonVi.forEach((el) =>
      (this.frmGuiEmail.get('lstIdDonVi') as FormArray).push(
        this.formBuilder.control(el)
      )
    );

    if (this.frmGuiEmail.invalid) return;
    let dataGuiEmail = this.frmGuiEmail.value as CreateGuiEmail;
    this.lstIdDonViError = dataGuiEmail.lstIdDonVi.length === 0;
    if (this.lstIdDonViError) return;
    let data: GuiLaiGuiEmail = {
      guiEmailDto: dataGuiEmail,
      lstIdGuiMail: this.selectedGe
        .filter((x) => x.trangThai !== GuiEmailTrangThai.ThanhCong)
        .map((x) => x.id),
    };
    this.guiEmailService.guiLaiEmail(data).subscribe({
      next: (res) => {
        if (res.success) {
          Utils.messageSuccess(this.messageService, res.message);
        } else {
          Utils.messageError(this.messageService, res.errors.at(0) ?? '');
        }
      },
      error: () => {
        this.visibleThuHoiEmail = false;
      },
      complete: () => {
        this.table.reset();
        this.tableGe.reset();
        this.visibleGuiEmail = false;
        this.selectedGe = [];
      },
    });
  };

  openThuHoiEmail = () => {
    this.visibleThuHoiEmail = true;
    if (this.frmThuHoiEmail) {
      this.frmThuHoiEmail.reset();
      this.frmThuHoiEmail.get('lstIdGuiMail')?.reset();
    }

    let lstThuHoi = this.selectedGe.filter(
      (x) => x.trangThai === GuiEmailTrangThai.ThanhCong
    );
    let noidung = `<p>Link bị thu hồi:</p><ol>`;
    lstThuHoi.forEach((el) => {
      noidung += ` <li><i><a href="${el.linkKhaoSat}">${el.linkKhaoSat}</a></i></li>`;
    });
    noidung += `</ol>`;
    let tieuDe = `Thu hồi ${this.tenBangKhaoSat} mã khảo sát ${this.maBangKhaoSat}`;
    this.frmThuHoiEmail = this.formBuilder.group({
      diaChiNhan: ['', Validators.required],
      noidung: [noidung, Validators.required],
      tieuDe: [tieuDe, Validators.required],
      lstIdGuiMail: this.formBuilder.array(
        lstThuHoi.map((x) => x.id),
        Validators.required
      ),
    });
  };

  onSubmitThuHoiEmail = () => {
    if (this.frmThuHoiEmail.invalid) return;
    let data = this.frmThuHoiEmail.value as ThuHoiGuiEmail;
    this.guiEmailService.thuHoiEmail(data).subscribe({
      next: (res) => {
        if (res.success) {
          Utils.messageSuccess(this.messageService, res.message);
        } else {
          Utils.messageError(this.messageService, res.errors.at(0) ?? '');
        }
      },
      error: () => {
        this.visibleThuHoiEmail = false;
      },
      complete: () => {
        this.table.reset();
        this.tableGe.reset();
        this.visibleThuHoiEmail = false;
        this.selectedGe = [];
      },
    });
  };
}
