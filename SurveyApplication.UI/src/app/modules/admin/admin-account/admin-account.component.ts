import { Component, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Account, Paging, Register } from '@app/models';
import { AccountService } from '@app/services/account.service';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-admin-account',
  templateUrl: './admin-account.component.html',
  styleUrls: ['./admin-account.component.css'],
})
export class AdminAccountComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas!: Account[];
  selectedAccount!: Account[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  frmAccount!: FormGroup;
  account!: Register;
  isCreate?: boolean;
  visible: boolean = false;
  submitted: boolean = false;

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.frmAccount = this.formBuilder.group({
      id: new FormControl<string>(''),
      userName: ['', Validators.required],
      matrixPermission: this.formBuilder.array([]),
    });
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
    this.accountService.getByCondition(this.paging).subscribe({
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

  onSubmitSearch = () => {
    this.paging.keyword = this.keyWord;
    this.accountService.getByCondition(this.paging).subscribe({
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

  f = (name: string, subName: string = ''): FormControl => {
    return (
      subName
        ? this.frmAccount?.get(name)?.get(subName)
        : this.frmAccount?.get(name)
    ) as FormControl;
  };

  onSubmit = (data: any) => {
    // if (this.isCreate !== null) this.createSubmit(data);
    // this.isCreate ? this.createSubmit(data) : this.updateSubmit(data);
  };

  createDialog = () => {
    this.isCreate = true;
    this.visible = true;
    this.submitted = false;
  };

  confirmDeleteMultiple = () => {};
}
