import { Component, ViewChild } from '@angular/core';
import {
  ConfirmEventType,
  ConfirmationService,
  MessageService,
} from 'primeng/api';
import { Table } from 'primeng/table';
import { Paging, CauHoi, CreateUpdateCauHoi, Select } from '@app/models';
import Utils from '@app/helpers/utils';
import { CauHoiService } from '@app/services';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { PrimeNGConfig } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css'],
})
export class QuestionComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  lstQuestion!: CauHoi[];
  selectedQuestion!: CauHoi[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  frmCauHoi!: FormGroup;

  question!: CreateUpdateCauHoi;
  lstLoaiCauHoi!: Select[];
  isOther: boolean = true;
  selectedLoaiCauHoi: string = '0';
  id: number = 0;
  isCreate?: boolean;
  visible: boolean = false;
  submitted: boolean = false;
  constructor(
    private cauHoiService: CauHoiService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private formBuilder: FormBuilder,
    private config: PrimeNGConfig,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.cauHoiService.getLoaiCauHoi().subscribe({
      next: (res) => {

        this.lstLoaiCauHoi = res;
        console.log("res",res)
      },
      error: (e) => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      },
      
    });
    this.createForm();
    Utils.translate('vi', this.translateService, this.config);
  }

  createForm = () => {
    this.frmCauHoi = this.formBuilder.group({
      id: new FormControl<number>(0),
      maCauHoi: ['', Validators.required],
      loaiCauHoi: ['', Validators.required],
      tieuDe: [''],
      isOther: new FormControl<boolean>(true),
      labelCauTraLoi: [''],
      noidung: [''],
      kichThuocFile: new FormControl<number>(0),
      soLuongFileToiDa: new FormControl<number>(0),
      lstCot: this.formBuilder.array([]),
      lstHang: this.formBuilder.array([]),
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
      orderBy: event.sortField
        ? `${event.sortField} ${event.sortOrder === 1 ? 'asc' : 'desc'}`
        : '',
    };
    this.cauHoiService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.lstQuestion = res.data;
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

  onSubmitSearch = () => {
    this.paging.keyword = this.keyWord;
    this.cauHoiService.getByCondition(this.paging).subscribe({
      next: (res) => {
        this.lstQuestion = res.data;
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

  f = (name: string, subName: string = ''): FormControl => {
    return (
      subName
        ? this.frmCauHoi?.get(name)?.get(subName)
        : this.frmCauHoi?.get(name)
    ) as FormControl;
  };

  onSubmit = (data: any) => {
    if (this.isCreate !== null)
      this.isCreate ? this.createSubmit(data) : this.updateSubmit(data);
  };

  createDialog = () => {
    this.isCreate = true;
    this.visible = true;
    this.submitted = false;
    this.createForm();
    this.selectedLoaiCauHoi = '0';
  };

  updateDialog = (id: number) => {
    this.isCreate = false;
    this.visible = true;
    this.submitted = false;
    this.createForm();
    this.cauHoiService.getById<CreateUpdateCauHoi>(id).subscribe({
      next: (res) => {
        let k = Object.keys(res);
        let v = Object.values(res);
        Utils.setValueForm(this.frmCauHoi, k, v);
        this.selectedLoaiCauHoi = res.loaiCauHoi.toString();
        this.isOther = res.isOther ?? false;
        res.lstCot.forEach((el, i) => {
          const newItem = this.formBuilder.group({
            id: el.id,
            maCot: el.maCot,
            noidung: el.noidung,
          });
          this.lstCot.push(newItem);
        });
        res.lstHang.forEach((el, i) => {
          const newItem = this.formBuilder.group({
            id: el.id,
            maHang: el.maHang,
            noidung: el.noidung,
          });
          this.lstHang.push(newItem);
        });
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
          complete: () => {
            this.table.reset();
          },
        });
      },
    });
  };

  createSubmit = (data: any) => {
    this.submitted = true;
    if (this.frmCauHoi.invalid) return;
    this.question = data.value;
    this.question.kichThuocFile = this.question.kichThuocFile
      ? this.question.kichThuocFile
      : 0;
    this.question.soLuongFileToiDa = this.question.soLuongFileToiDa
      ? this.question.soLuongFileToiDa
      : 0;
    this.question.batBuoc = false;
    this.question.priority = 0;
    this.cauHoiService.create<CreateUpdateCauHoi>(this.question).subscribe({
      next: (res) => {
        if (res.success) {
          this.table.reset();
          Utils.messageSuccess(this.messageService, res.message);
          this.visible = false;
        } else {
          Utils.messageError(this.messageService, res.errors.at(0) ?? '');
        }
      },
    });
  };

  updateSubmit = (data: any) => {
    this.submitted = true;
    if (this.frmCauHoi.invalid) return;
    this.question = data.value;
    this.question.kichThuocFile = this.question.kichThuocFile
      ? this.question.kichThuocFile
      : 0;
    this.question.soLuongFileToiDa = this.question.soLuongFileToiDa
      ? this.question.soLuongFileToiDa
      : 0;
    this.question.batBuoc = false;
    this.question.priority = 0;
    this.cauHoiService.update<CreateUpdateCauHoi>(this.question).subscribe({
      next: (res) => {
        if (res.success) {
          this.table.reset();
          Utils.messageSuccess(this.messageService, res.message);
          this.visible = false;
        } else {
          Utils.messageError(this.messageService, res.errors.at(0) ?? '');
        }
      },
    });
  };

  handlerChange = (e: any) => {
    this.isOther = e.target.checked;
    !e.target.checked && this.f('labelCauTraLoi').setValue('');
  };

  handlerChangeLCh = (e: any) => {
    this.selectedLoaiCauHoi = e.value;
    this.lstCot.clear();
    this.lstHang.clear();
    this.f('isOther')?.setValue(false);
    this.f('labelCauTraLoi')?.setValue('');
    this.f('maCauHoi')?.setValue('');
    this.f('tieuDe')?.setValue('');
    this.f('noidung')?.setValue('');
    this.f('kichThuocFile')?.setValue('');
    this.f('soLuongFileToiDa')?.setValue('');
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
