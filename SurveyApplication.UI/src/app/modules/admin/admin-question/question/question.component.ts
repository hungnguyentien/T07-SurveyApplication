import {
  Component,
  ViewChild,
  Input,
  Output,
  EventEmitter,
} from '@angular/core';
import {
  ConfirmEventType,
  ConfirmationService,
  MessageService,
} from 'primeng/api';
import { Table } from 'primeng/table';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

import {
  Paging,
  CauHoi,
  CreateUpdateCauHoi,
  Cot,
  Hang,
  LoaiCauHoi,
} from '@app/models';
import Utils from '@app/helpers/utils';
import { CauHoiService } from '@app/services';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css'],
})
export class QuestionComponent {
  public Editor = ClassicEditor; // Tham chiếu đến ClassicEditor
  @Input() valueEditor: string = ''; // Khai báo biến lưu nội dung CKEditor
  @Input() inputValue: string = '';
  @Input() listAnswer: string[] = [];
  @Output() inputValueChange: EventEmitter<string> = new EventEmitter<string>();

  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  lstQuestion!: CauHoi[];
  selectedQuestion!: CauHoi[];

  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;
  frmCauHoi: FormGroup;

  question!: CreateUpdateCauHoi;
  columns!: Cot[];

  lstLoaiCauHoi!: LoaiCauHoi[];
  isOther!: boolean;
  selectedLoaiCauHoi: string = '0';
  id: number = 0;

  constructor(
    private cauHoiService: CauHoiService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private formBuilder: FormBuilder
  ) {
    this.frmCauHoi = this.formBuilder.group({
      id: [''],
      maCauHoi: ['', Validators.required],
      loaiCauHoi: ['', Validators.required],
      tieuDe: [''],
      isOther: [''],
      labelCauTraLoi: [''],
      noidung: [''],
      kichThuocFile: [''],
      soLuongFileToiDa: [''],
      lstCot: this.formBuilder.array([]),
      lstHang: this.formBuilder.array([]),
    });
  }

  ngOnInit() {
    this.loading = true;
    this.lstLoaiCauHoi = [
      { text: 'Chọn một đáp án', value: '0' },
      { text: 'Chọn nhiều đáp án', value: '1' },
      { text: 'Văn bản ngắn', value: '2' },
      { text: 'Văn bản dài', value: '3' },
      { text: 'Dạng bảng (một lựa chọn)', value: '4' },
      { text: 'Dạng bảng (nhiều lựa chọn)', value: '5' },
      { text: 'Dạng bảng (văn bản)', value: '6' },
      { text: 'Tải tệp tin', value: '7' },
    ];
  }
  //CKEditer
  onInputChange() {
    this.inputValueChange.emit(this.inputValue);
    this.inputValueChange.emit(this.valueEditor);
    console.log(this.inputValue);
  }

  onSubmitSearch = () => {
    this.paging.keyword = this.keyWord;
    this.cauHoiService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.lstQuestion = res.data;
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

  loadListLazy = (event: any) => {
    this.loading = true;
    let pageSize = event.rows;
    let pageIndex = event.first / pageSize + 1;
    this.paging = {
      pageIndex: pageIndex,
      pageSize: pageSize,
      keyword: '',
    };
    this.cauHoiService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.lstQuestion = res.data;
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

  confirmDeleteMultiple() {
    let ids: number[] = [];
    this.selectedQuestion.forEach((el) => {
      ids.push(el.id);
    });
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} câu hỏi này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.cauHoiService.deleteMultiple(ids).subscribe({
          next: (res) => {
            Utils.messageSuccess(
              this.messageService,
              `Xoá câu hỏi ${ids.length} thành công!`
            );
          },
          error: (e) => Utils.messageError(this.messageService, e.message),
          complete: () => {
            this.table.reset();
          },
        });
      },
      reject: (type: ConfirmEventType) => {},
    });
  }

  confirmDelete = (title: string, id: number) => {
    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá câu hỏi ${title} không?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.cauHoiService.delete(id).subscribe({
          next: (res) => {
            Utils.messageSuccess(
              this.messageService,
              `Xoá câu hỏi ${title} thành công!`
            );
          },
          error: (e) => Utils.messageError(this.messageService, e.message),
          complete: () => {
            this.table.reset();
          },
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
    this.cauHoiService.update<CreateUpdateCauHoi>(this.question).subscribe({
      next: (res) => {
        this.table.reset();
        Utils.messageSuccess(this.messageService, res.message);
      },
      error: (e) => Utils.messageError(this.messageService, e.message),
    });
  };

  submitForm() {
    if (this.selectedLoaiCauHoi === '0') {
      console.log('Input Value:', this.inputValue);
      console.log('ValueEditor:', this.valueEditor);
    } else if (this.selectedLoaiCauHoi === '1') {
      console.log('Input Value:', this.inputValue);
    }
  }

  onSubmit(data: any) {
    if (this.frmCauHoi.invalid) {
      return;
    }

    this.question = data.value;
    this.question.batBuoc = false;
    this.question.priority = 0;
    debugger;
    this.cauHoiService.create<CreateUpdateCauHoi>(this.question).subscribe({
      next: (res) => {
        this.table.reset();
        Utils.messageSuccess(this.messageService, res.message);
      },
      error: (e) => Utils.messageError(this.messageService, e.message),
    });
  }

  handlerChange = (e: any) => {
    this.isOther = e.target.checked;
    !e.target.checked && this.f('labelCauTraLoi').setValue('');
  };

  handlerChangeLCh = (e: any) => {
    this.selectedLoaiCauHoi = e.value;
  };

  f = (name: string, subName: string = ''): FormControl => {
    return (
      subName
        ? this.frmCauHoi?.get(name)?.get(subName)
        : this.frmCauHoi?.get(name)
    ) as FormControl;
  };

  get lstCot(): FormArray {
    return this.frmCauHoi.get('lstCot') as FormArray;
  }

  get lstHang(): FormArray {
    return this.frmCauHoi.get('lstHang') as FormArray;
  }

  addItem(isCot: boolean = true) {
    if (isCot) {
      const newItem = this.formBuilder.group({
        id: 0,
        maCot: '',
        noidung: '',
      });
      this.lstCot.push(newItem);
    } else {
      const newItem = this.formBuilder.group({
        id: 0,
        maHang: '',
        noidung: '',
      });
      this.lstHang.push(newItem);
    }
  }

  deleteItem(index: number, isCot: boolean = true) {
    isCot ? this.lstCot.removeAt(index) : this.lstHang.removeAt(index);
  }
}
