import { Component, ViewChild } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { GuiEmail, GuiEmailBks, Paging, PagingGuiEmailBks } from '@app/models';
import { GuiEmailService } from '@app/services';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

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
  keyword!: string;

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

  constructor(
    private FormBuilder: FormBuilder,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private guiEmailService: GuiEmailService
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
}
