import { Component } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { LoginService } from '@app/services';
import { MessageService } from 'primeng/api';
import { environment } from '@environments/environment';
import { AccountService } from '@app/services/account.service';
import Utils from '@app/helpers/utils';
@Component({
  selector: 'app-admin-templete',
  templateUrl: './admin-templete.component.html',
  styleUrls: ['./admin-templete.component.css'],
})
export class AdminTempleteComponent {
  userName: string = '';
  userId: any = '';
  FormProfile!: FormGroup;
  visible: boolean = false;
  urlFist!: string;
  Domain: string = environment.apiUrlImage;
  listDatasUser!: any;
  Name!: string;
  Role!: string;
  Address!: string;

  constructor(
    private loginService: LoginService,
    private FormBuilder: FormBuilder,
    private messageService: MessageService,
    private accountService: AccountService
  ) {
    const cUser = loginService.getCurrentUser();
    this.userName = cUser?.name;
    this.userId = cUser?.uid;
    this.urlFist = cUser?.img
      ? `${this.Domain}${cUser?.img}`
      : 'http://placehold.it/180';
  }
  ngOnInit() {
    this.FormProfile = this.FormBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      address: ['', Validators.required],
      img: [''],
    });
  }

  logout() {
    this.loginService.logout();
  }

  editProfile() {
    this.visible = !this.visible;
    this.getByIdUser();
  }

  save = () => {
    const formData = new FormData();
    const updatedData = this.FormProfile.value;
    formData.append('name', updatedData.name);
    formData.append('email', updatedData.email);
    formData.append('address', updatedData.address);
    formData.append('img', updatedData.img);
    formData.append('id', this.userId);
    this.accountService.update(formData).subscribe((res) => {
      if (res.success) {
        Utils.messageSuccess(this.messageService, res.message);
        this.visible = false;
      } else Utils.messageError(this.messageService, res.errors?.at(0) ?? '');
    });
  };

  getByIdUser() {
    this.loginService.getByIdUser(this.userId).subscribe((res: any) => {
      this.listDatasUser = res;
      this.Name = res.name;
      this.Role = res.userName;
      this.Address = res.address;
      this.FormProfile.controls['name'].setValue(this.listDatasUser.name);
      this.FormProfile.controls['email'].setValue(this.listDatasUser.email);
      this.FormProfile.controls['address'].setValue(this.listDatasUser.address);
      if (this.listDatasUser.image) {
        this.urlFist = this.Domain + this.listDatasUser.image; // Đường dẫn hình ảnh từ dữ liệu người dùng
      } else {
        this.urlFist = 'http://placehold.it/180'; // Đường dẫn mặc định nếu không có hình ảnh
      }
    });
  }

  // upload file images
  onFileChanged(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
      const file = inputElement.files[0];
      if (file.type.match(/image\/*/) === null) {
        console.error('Chỉ chấp nhận tệp hình ảnh.');
        return;
      }
      const reader = new FileReader();
      reader.onload = (e) => {
        this.urlFist = reader.result as string;
      };
      // Cập nhật FormControl 'img' trong FormProfile để lưu trữ tệp hình ảnh
      this.FormProfile.patchValue({
        img: file,
      });
      // Đọc dữ liệu từ tệp hình ảnh và gán cho FormControl 'img'
      reader.readAsDataURL(file);
    }
  }

  dropDown = () => {
    const dropDown = document.getElementById('dropdown-menu-login');
    if(dropDown){
      const checkShow = dropDown.classList.value.indexOf('show') >= 0;
      if(checkShow){
        dropDown.classList.remove('show');
      }else{
        dropDown.classList.add('show');
      }
    }
  }

  showModal = () => {
    const dropDown = document.getElementById('logoutModal');
    dropDown && dropDown.classList.add('show');
    dropDown && dropDown.classList.add('d-block');
  }

  closeModal = () => {
    const dropDown = document.getElementById('logoutModal');
    dropDown && dropDown.classList.remove('show');
    dropDown && dropDown.classList.remove('d-block');
  }

  pageTop = () => {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
  };
}
