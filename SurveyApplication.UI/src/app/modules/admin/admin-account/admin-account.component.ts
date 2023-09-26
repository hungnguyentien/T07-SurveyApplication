import { Component, ViewChild } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import Utils from '@app/helpers/utils';
import { Account, Paging, Register, Role } from '@app/models';
import { RoleService } from '@app/services';
import { AccountService } from '@app/services/account.service';
import { MessageService } from 'primeng/api';
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

  lstRole: Role[] = [];
  selectedRole: string[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private roleService: RoleService,
    private accountService: AccountService,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.frmAccount = this.formBuilder.group(
      {
        id: new FormControl<string>(''),
        userName: ['', [Validators.required, Validators.minLength(6)]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        passwordConfirmed: [''],
        name: [''],
        address: [''],
        lstRoleName: this.formBuilder.array([]),
        matrixPermission: this.formBuilder.array([]),
      },
      { validators: this.checkPasswords }
    );

    this.roleService.getAll().subscribe({
      next: (res) => {
        this.lstRole = res;
      },
    });
  }

  checkPasswords: ValidatorFn = (
    group: AbstractControl
  ): ValidationErrors | null => {
    let pass = group.get('password')?.value;
    let confirmPass = group.get('passwordConfirmed')?.value;
    return pass === confirmPass ? null : { notSame: true };
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
    if (this.isCreate !== null) this.createSubmit(data);
    // this.isCreate ? this.createSubmit(data) : this.updateSubmit(data);
  };

  createSubmit = (data: any) => {
    this.submitted = true;
    if (this.frmAccount.invalid) return;
    this.account = data.value;
    this.account.lstRoleName = this.selectedRole;
    debugger
    this.accountService.register<Register>(this.account).subscribe({
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

  createDialog = () => {
    this.isCreate = true;
    this.visible = true;
    this.submitted = false;
  };

  confirmDeleteMultiple = () => {};
}
