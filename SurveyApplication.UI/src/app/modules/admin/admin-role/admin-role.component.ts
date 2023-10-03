import { Component, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import Utils from '@app/helpers/utils';
import {
  CreateUpdateRole,
  LstPermission,
  MatrixPermission,
  Paging,
  Role,
} from '@app/models';
import { RoleService } from '@app/services';
import { ConfirmationService, MessageService, TreeNode } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-admin-role',
  templateUrl: './admin-role.component.html',
  styleUrls: ['./admin-role.component.css'],
})
export class AdminRoleComponent {
  @ViewChild('dt') table!: Table;
  loading: boolean = true;
  datas!: Role[];
  selectedRole!: Role[];
  paging!: Paging;
  dataTotalRecords!: number;
  keyWord!: string;

  frmRole!: FormGroup;
  role!: CreateUpdateRole;
  isCreate?: boolean;
  checkBtnDetail?:boolean;
  visible: boolean = false;
  submitted: boolean = false;
  modaltitle:string='';

  treeData: TreeNode[] = [];
  selectedTreeData!: TreeNode<any> | TreeNode<any>[] | null;

  constructor(
    private roleService: RoleService,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
  ) {}

  ngOnInit() {
    this.createForm();
  }

  createForm = () => {
    this.frmRole = this.formBuilder.group({
      id: new FormControl<string>(''),
      name: ['', Validators.required],
      matrixPermission: this.formBuilder.array([]),
    });
    this.treeData = [];
    this.selectedTreeData = [];
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
    this.roleService.getByCondition(this.paging).subscribe({
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
    this.roleService.getByCondition(this.paging).subscribe({
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
      subName ? this.frmRole?.get(name)?.get(subName) : this.frmRole?.get(name)
    ) as FormControl;
  };

  onSubmit = (data: any) => {
    if (this.isCreate == true) this.createSubmit(data);
    this.isCreate ? this.createSubmit(data) : this.updateSubmit(data);
  };

  createSubmit = (data: any) => {
   debugger
    this.submitted = true;
    if (this.frmRole.invalid) return;
    this.role = data.value;
    let lstModule: MatrixPermission[] = [];
    let selectedTree = this.selectedTreeData as any[];
    selectedTree
      .filter((x) => !x.parent)
      .forEach((el) => {
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
    this.role.matrixPermission = lstModule;
    this.roleService.create<CreateUpdateRole>(this.role).subscribe({
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
    this.checkBtnDetail = false;
    this.modaltitle='Thêm mới nhóm quyền'
    this.visible = true;
    this.submitted = false;
    this.createForm();
    this.roleService.getMatrixPermission().subscribe({
      next: (res) => {
        res.forEach((el) => {
          let data = {
            key: `${el.module.toString()}_${el.nameModule}`,
            label: el.nameModule,
            data: el.module,
            children: el.lstPermission.map((x) =>
              Object({
                key: `${el.module.toString()}_${
                  el.nameModule
                }_${x.value.toString()}_${x.name}`,
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
  };

  detailDialog = (id: string) => {
  
    this.checkBtnDetail = true;
    this.isCreate = !this.isCreate;
    this.modaltitle='Chi tiết nhóm quyền'
    this.visible = true;
    this.submitted = false;
    this.createForm();
    this.roleService.getPermissionById(id).subscribe({
      next: (res) => {
        let k = Object.keys(res);
        let v = Object.values(res);
        Utils.setValueForm(this.frmRole, k, v);
        this.frmRole.controls['name'].disable();
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
              key: `${el.module.toString()}_${
                el.nameModule
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
                key: `${el.module.toString()}_${
                  el.nameModule
                }_${x.value.toString()}_${x.name}`,
                label: x.name,
                data: x.value,
                parent: `${el.module.toString()}_${el.nameModule}`,
                selectable: false,
              })
            ),
            selectable: false,
          };
          this.treeData.push(data);
        });
      },
    });
  };

 
  editDialog(id: string) {
    this.checkBtnDetail = false;
    this.isCreate = false;
    this.modaltitle = 'Cập nhật nhóm quyền';
    this.visible = true;
    this.submitted = false;
    this.createForm();
    // Lấy thông tin vai trò dựa trên 'id'.
    this.roleService.getPermissionById(id).subscribe({
      next: (res) => {
        let k = Object.keys(res);
        let v = Object.values(res);
        Utils.setValueForm(this.frmRole, k, v);
        let selectedTreeData = this.selectedTreeData as TreeNode[];
        selectedTreeData.length = 0; // Xóa tất cả dữ liệu cũ trước khi thêm dữ liệu mới.
        res.matrixPermission.forEach((el) => {
          let data: TreeNode = {
            key: `${el.module.toString()}_${el.nameModule}`,
            label: el.nameModule,
            data: el.module,
            children: el.lstPermission.map((x) => ({
              key: `${el.module.toString()}_${el.nameModule}_${x.value.toString()}_${x.name}`,
              label: x.name,
              data: x.value,
              parent: {} as TreeNode, 
              selectable: true, // Cho phép tích sửa tại đây
            })),
            selectable: true, // Cho phép tích sửa tại đây
          };
          selectedTreeData.push(data);
        });
      },
      error: (error: any) => {      
        Utils.messageError(this.messageService, 'Có lỗi xảy ra khi lấy thông tin vai trò.');
      },
    });
    this.roleService.getMatrixPermission().subscribe({
      next: (res) => {
        res.forEach((el) => {
          let data: TreeNode = {
            key: `${el.module.toString()}_${el.nameModule}`,
            label: el.nameModule,
            data: el.module,
            children: el.lstPermission.map((x) => ({
              key: `${el.module.toString()}_${el.nameModule}_${x.value.toString()}_${x.name}`,
              label: x.name,
              data: x.value,
              parent: {} as TreeNode, // Set parent as an empty TreeNode
              selectable: true, // Cho phép tích sửa tại đây
            })),
            selectable: true, // Cho phép tích sửa tại đây
          };
          this.treeData.push(data);
        });
      },
    });
  }

  
  updateSubmit = (data: any) => {
    debugger
    this.submitted = true;
    if (this.frmRole.invalid) return;
    this.role = data.value;
    let lstModule: MatrixPermission[] = [];
    let selectedTree = this.selectedTreeData as any[];
    
    // Tạo một hàm đệ quy để lấy cả mục con
    const collectPermissions = (parentKey: string): LstPermission[] => {
      return selectedTree
        .filter((x) => x.key.startsWith(parentKey)) // Lọc theo tiền tố key
        .map((p) => ({
          name: p.label,
          value: p.data,
        }) as LstPermission)
        .concat(
          selectedTree
            .filter((x) => x.parent && x.parent.key === parentKey)
            .map((el) => collectPermissions(el.key))
            .reduce((acc, curr) => acc.concat(curr), [])
        );
    };
  
    selectedTree
      .filter((x) => !x.parent)
      .forEach((el) => {
        lstModule.push({
          module: el.data,
          nameModule: el.label,
          lstPermission: collectPermissions(el.key), // Gọi hàm đệ quy để lấy cả mục con
        } as MatrixPermission);
      });
  
    this.role.matrixPermission = lstModule;
  
    this.roleService.update<CreateUpdateRole>(this.role).subscribe({
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
  
  
  
  Delete(data:any){
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xoá không ' + '?',
      header: 'delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.roleService.delete(data.id).subscribe((res: any) => {
          if (res.success == true){
            Utils.messageSuccess(this.messageService, res.message);
            this.table.reset();
            this.frmRole.reset();
          }
          else{
            Utils.messageError(this.messageService, res.message);
            this.table.reset();
            this.frmRole.reset();
          }
        });
      },
    });
  }

  confirmDeleteMultiple() {
    let ids: string[] = [];
    this.selectedRole.forEach((el) => {
      ids.push(el.id);
    });

    this.confirmationService.confirm({
      message: `Bạn có chắc chắn muốn xoá ${ids.length} đợt khảo sát này?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.roleService.deletePermissionMultiple(ids).subscribe({
          next: (res: any) => {
            if (res.success == false) {
              Utils.messageError(this.messageService, res.message);
            } else {
              Utils.messageSuccess(
                this.messageService,
                `Xoá ${ids.length} đợt khảo sát thành công!`
              );
              this.selectedRole=[];
            }
          },
          error: (e) => Utils.messageError(this.messageService, e.message),
          complete: () => {
            this.table.reset();
          },
        });
      },
      reject: () => {},
    });
  };

  
  
  

}
