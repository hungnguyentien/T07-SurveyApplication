import { Component } from '@angular/core';
import {
  ConfirmEventType,
  ConfirmationService,
  MessageService,
} from 'primeng/api';
import { Paging, CauHoi, UpdateCauHoi } from '@app/models';
import Utils from '@app/helpers/utils';
import { CauHoiService } from '@app/services';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css'],
})
export class QuestionComponent {
  loading: boolean = true;

  paging!: Paging;
  lstQuestion!: CauHoi[];
  selectedQuestion!: CauHoi[];
  question!: UpdateCauHoi;

  constructor(
    private cauHoiService: CauHoiService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}
  inputValue: string = '';
  valueEditor: string = '';
  selectedOption: string = 'motdapan';
  submitForm() {
    if (this.selectedOption === 'motdapan') {
      console.log('Input Value:', this.inputValue);
      console.log('ValueEditor:', this.valueEditor);
    } else if (this.selectedOption === 'nhieudapan') {
      console.log('Input Value:', this.inputValue);
    }
  }
  ngOnInit() {
    this.paging = {
      pageIndex: 1,
      pageSize: 100,
      keyword: '',
    };
    this.cauHoiService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.lstQuestion = res.data;
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
  confirm1() {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá câu hỏi này?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Có',
      rejectLabel: 'Đóng',
      accept: () => {
        this.messageService.add({
          severity: 'info',
          summary: 'Confirmed',
          detail: 'You have accepted',
        });
      },
      reject: (type: ConfirmEventType) => {
        switch (type) {
          case ConfirmEventType.REJECT:
            this.messageService.add({
              severity: 'error',
              summary: 'Rejected',
              detail: 'You have rejected',
            });
            break;
          case ConfirmEventType.CANCEL:
            this.messageService.add({
              severity: 'warn',
              summary: 'Cancelled',
              detail: 'You have cancelled',
            });
            break;
        }
      },
    });
  }

  confirmDelete = (title: string, id: number) => {
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá câu hỏi ${title} không?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.cauHoiService.delete(id).subscribe({
          next: (res) => {
            this.messageService.add({
              severity: 'info',
              summary: 'Confirmed',
              detail: `Xoá câu hỏi ${title} thành công!`,
            });
          },
          error: (e) => Utils.messageError(this.messageService, e.message),
        });
      },
    });
  };

  update = () => {
    // this.question = {
    //   maCauHoi: 'string16',
    //   loaiCauHoi: 0,
    //   batBuoc: true,
    //   tieuDe: 'string',
    //   kichThuocFile: 0,
    //   isOther: true,
    //   labelCauTraLoi: 'string',
    //   priority: 0,
    //   noiDung: 'aa',
    //   soLuongFileToiDa: 1,
    //   id: 16
    // }
    this.cauHoiService
      .update<UpdateCauHoi>(this.question)
      .subscribe({
        next: (res) => {
          Utils.messageSuccess(this.messageService, res.message)
        },
        error: (e) => Utils.messageError(this.messageService, e.message),
      });
  };
}