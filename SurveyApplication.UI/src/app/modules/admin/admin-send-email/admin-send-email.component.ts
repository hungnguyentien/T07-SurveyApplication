import { Component, ViewChild } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { GuiEmailBks, Paging } from '@app/models';
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

  constructor(
    private FormBuilder: FormBuilder,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private guiEmailService: GuiEmailService,
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
}
