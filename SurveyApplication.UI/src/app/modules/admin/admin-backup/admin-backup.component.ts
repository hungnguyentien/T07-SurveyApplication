import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import Utils from '@app/helpers/utils';
import { BackupRestore, Paging, Select } from '@app/models';
import { BackupService } from '@app/services';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-admin-backup',
  templateUrl: './admin-backup.component.html',
  styleUrls: ['./admin-backup.component.css'],
})
export class AdminBackupComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas: BackupRestore[] = [];
  selectedBackupRestore!: BackupRestore[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  visible: boolean = false;
  formBackupRestore!: FormGroup;
  listScheduleDayofweek: Select[] = [];
  listScheduleHour: Select[] = [];
  listScheduleMinute: Select[] = [];

  selectedScheduleDayofweek: string = '0';
  selectedScheduleHour: string = '0';
  selectedScheduleMinute: string = '0';

  titleConfirm :string =""
  constructor(
    private formBuilder: FormBuilder,
    private backupService: BackupService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
  ) {}

  ngOnInit() {
    this.loading = true;
    this.formBackupRestore = this.formBuilder.group({
      scheduleDayofweek: [0],
      scheduleHour: [0],
      scheduleMinute: [0],
    });

    this.backupService
      .getScheduleDayofweek()
      .subscribe((data) => (this.listScheduleDayofweek = data));

    this.backupService
      .getScheduleHour()
      .subscribe((data) => (this.listScheduleHour = data));

    this.backupService
      .getScheduleMinute()
      .subscribe((data) => (this.listScheduleMinute = data));
  }

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
    this.backupService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.dataTotalRecords = res.totalFilter;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };

  onSubmitSearch = () => {
    this.paging.keyword = this.keyWord;
    this.backupService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.datas = res.data;
        this.dataTotalRecords = res.totalFilter;
      },
      error: (e) => {
        Utils.messageError(this.messageService, e.message);
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
    });
  };

  backupNow = () => {
    this.titleConfirm ="Xác nhận sao lưu ngay"
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn sao lưu không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.backupService.backupNow().subscribe({
          next: (res) => {
            if (res.success) {
              Utils.messageSuccess(this.messageService, res.message);
              setTimeout(() => {
                this.table.reset();
              }, 3000);
            } else {
              Utils.messageError(this.messageService, res.errors?.at(0) ?? '');
            }
          },
        });
      },
    });
  };

  restoreData = (fileName: string) => {
    this.titleConfirm ="Xác nhận khôi phục dữ liệu"
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn khôi phục không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.backupService.restoreData(fileName).subscribe({
          next: (res) => {
            if (res.success) {
              Utils.messageSuccess(this.messageService, res.message);
              this.visible = false;
            } else Utils.messageError(this.messageService, res.errors?.at(0) ?? '');
          },
        });
      },
    });
    
  };

  openDialog = () => {
    this.visible = !this.visible;
    this.backupService.getConfigBackup().subscribe((data) => {
      this.selectedScheduleDayofweek = data.scheduleDayofweek.toString();
      this.selectedScheduleHour = data.scheduleHour.toString();
      this.selectedScheduleMinute = data.scheduleMinute.toString();
    });
  };

  onSubmit = () => {
    this.backupService.configBackup(this.formBackupRestore.value).subscribe({
      next: (res) => {
        if (res.success) {
          Utils.messageSuccess(this.messageService, res.message);
          this.visible = false;
        } else Utils.messageError(this.messageService, res.errors?.at(0) ?? '');
      },
    });
  };
}
