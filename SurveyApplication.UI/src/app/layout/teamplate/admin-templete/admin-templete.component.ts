import { Component } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { LoginService } from '@app/services';
import { MessageService } from 'primeng/api';
import { environment } from '@environments/environment';
import { AccountService } from '@app/services/account.service';
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
    this.userName = loginService.getCurrentUser()?.name;
    this.userId = loginService.getCurrentUser()?.uid;
  }
  ngOnInit() {
    this.FormProfile = this.FormBuilder.group({
      name: ['', Validators.required],
      userName: ['', Validators.required],
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
    formData.append('userName', updatedData.userName);
    formData.append('email', updatedData.email);
    formData.append('address', updatedData.address);
    formData.append('img', updatedData.image);
    this.accountService.update(formData).subscribe((res: any) => {
      console.log(res);
    });
  };

  getByIdUser() {
    this.loginService.getByIdUser(this.userId).subscribe((res: any) => {
      this.listDatasUser = res;
      this.Name = res.name;
      this.Role = res.userName;
      this.Address = res.address;
      this.FormProfile.controls['name'].setValue(this.listDatasUser.name);
      this.FormProfile.controls['userName'].setValue(
        this.listDatasUser.userName
      );
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
}
