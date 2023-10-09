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
import { Account, CreateUpdateRole, LstPermission, MatrixPermission, Paging, Register, Role } from '@app/models';
import { RoleService } from '@app/services';
import { AccountService } from '@app/services/account.service';
import { MessageService, TreeNode } from 'primeng/api';
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
    
  visibleRole: boolean = false;

  submitted: boolean = false;

  lstRole: Role[] = [];
  selectedRole: string[] = [];
  matrixSelect: MatrixPermission[]=[];

  role!: CreateUpdateRole;
  treeData: TreeNode[] = [];
  selectedTreeData!: TreeNode<any> | TreeNode<any>[] | null;

  formData: any = {};
  constructor(
    private formBuilder: FormBuilder,
    private roleService: RoleService,
    private accountService: AccountService,
    private messageService: MessageService
  ) { 
    this.createFrom()
  }

  ngOnInit() {
    
    // this.createFrom()
    this.roleService.getAll().subscribe({
      next: (res) => {
        this.lstRole = res;
      },
    });
    this.treeData = [];
    this.selectedTreeData = [];
  }

  createFrom(){
    debugger
    this.frmAccount = this.formBuilder.group(
      {
        id: new FormControl<string>(''),
        userName: ['',[Validators.required, Validators.minLength(6)]],
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


    this.frmAccount.valueChanges.subscribe((data) => {
      this.formData = { ...data };
    });
   
  }
  createSubmitRole = (data: any) => {
    this.role = data.value;
    this.frmAccount.setValue(this.formData);
    let lstModule: MatrixPermission[] = [];
    let selectedTree = this.selectedTreeData as any[];
    selectedTree.filter((x) => !x.parent || typeof (x.parent) === 'object').forEach((el) => {
        if (el.parent) {
          lstModule.push({
            module: el.parent.data,
            nameModule: el.parent.label,
            lstPermission: selectedTree
              .filter((x) => x.parent && x.parent.key === el.parent.key)
              .map(
                (p) =>
                  Object({
                    name: p.label,
                    value: p.data,
                  }) as LstPermission
              ),
          } as MatrixPermission);
        } else
          lstModule.push({
            module: el.data,
            nameModule: el.label,
            lstPermission: selectedTree
              .filter((x) => x.parent && x.parent === el.key)
              .map(
                (p) =>
                  Object({
                    name: p.label,
                    value: p.data,
                  }) as LstPermission
              ),
          } as MatrixPermission);
      });
      this.matrixSelect = lstModule;
      this.visibleRole = false;
      console.log(lstModule)

  };



  addRole =()=>{
    this.visibleRole = true; 
    this.frmAccount.setValue(this.formData);
    this.roleService.getMatrixPermission().subscribe({
      next: (res) => {
        res.forEach((el) => {
          let data = {
            key: `${el.module.toString()}_${el.nameModule}`,
            label: el.nameModule,
            data: el.module,
            children: el.lstPermission.map((x) =>
              Object({
                key: `${el.module.toString()}_${el.nameModule}_${x.value.toString()}_${x.name}`,
                label: x.name,
                data: x.value,
                parent: `${el.module.toString()}_${el.nameModule}`,
              })
            ),
          };
          this.treeData.push(data);
        });
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
    this.isCreate ? this.createSubmit(data) : this.updateSubmit(data);
  };
  updateDialog(data:any){
    debugger
    this.isCreate = false;
    this.visible = true;
    this.frmAccount.controls['userName'].setValue(data.userName);
    this.frmAccount.controls['email'].setValue(data.email);
    this.frmAccount.controls['password'].setValue(data.password);
    this.frmAccount.controls['name'].setValue(data.name);
    this.frmAccount.controls['address'].setValue(data.address);

    this.roleService.getPermissionById(data.id).subscribe({
      next: (res) => {
        let k = Object.keys(res);
        let v = Object.values(res);
        Utils.setValueForm(this.frmAccount, k, v);
        // this.frmRole.controls['name'].disable();
        let selectedTreeData = this.selectedTreeData as any[];
        res.matrixPermission.forEach((el) => {
          let data = {
            key: `${el.module.toString()}_${el.nameModule}`,
            label: el.nameModule,
            data: el.module,
          };
          selectedTreeData.push(data);
          el.lstPermission.forEach((x) => {
            selectedTreeData.push({
              key: `${el.module.toString()}_${el.nameModule
                }_${x.value.toString()}_${x.name}`,
              label: x.name,
              data: x.value,
              parent: `${el.module.toString()}_${el.nameModule}`,
            });
          });
        });
      },
    });
    this.roleService.getMatrixPermission().subscribe({
      next: (res) => {
        res.forEach((el) => {
          let data = {
            key: `${el.module.toString()}_${el.nameModule}`,
            label: el.nameModule,
            data: el.module,
            children: el.lstPermission.map((x) =>
              Object({
                key: `${el.module.toString()}_${el.nameModule
                  }_${x.value.toString()}_${x.name}`,
                label: x.name,
                data: x.value,
                parent: `${el.module.toString()}_${el.nameModule}`,
                selectable: true,
              })
            ),
            selectable: true,
          };
          this.treeData.push(data);
        });
      },
    });

    
  }

  updateSubmit =(data:any)=>{
    this.submitted = true;
    if (this.frmAccount.invalid) return;
    this.account = data.value;
    this.account.lstRoleName = this.selectedRole;
    this.account.matrixPermission = this.matrixSelect;
    this.accountService.update<Account>(this.account).subscribe({
      next: (res) => {
        if (res.success) {
          this.table.reset();
          Utils.messageSuccess(this.messageService, res.message);
          this.visible = false;
          this.frmAccount.reset();
          this.account.lstRoleName = [];
          this.account.matrixPermission = [];
        } else {
          Utils.messageError(this.messageService, res.errors.at(0) ?? '');
        }
      },
    });
  }
  createSubmit = (data: any) => {
    debugger
    this.submitted = true;
    if (this.frmAccount.invalid) return;
    this.account = data.value;
    this.account.lstRoleName = this.selectedRole;
    this.account.matrixPermission = this.matrixSelect;
    this.accountService.register<Register>(this.account).subscribe({
      next: (res) => {
        if (res.success) {
          this.table.reset();
          Utils.messageSuccess(this.messageService, res.message);
          this.visible = false;
          this.frmAccount.reset();
          this.account.lstRoleName = [];
          this.account.matrixPermission = [];
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
    this.createFrom();
  };
  confirmDeleteMultiple = () => { };
}
