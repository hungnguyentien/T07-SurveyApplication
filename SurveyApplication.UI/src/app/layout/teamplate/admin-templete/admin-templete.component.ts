import { Component } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { LoginService } from '@app/services';

@Component({
  selector: 'app-admin-templete',
  templateUrl: './admin-templete.component.html',
  styleUrls: ['./admin-templete.component.css'],
})

export class AdminTempleteComponent {
  userName: string = '';
  FormProfile!: FormGroup;
  visible: boolean = false;
  urlFist!: string;
  valueIdUser!: any;

  Name!: string;
  Role!: string;
  Address!: string;

  constructor(
    private loginService: LoginService,
    private FormBuilder: FormBuilder
  ) {
    this.userName = loginService.getCurrentUser()?.name;
  }
  ngOnInit() {
    this.FormProfile = this.FormBuilder.group({
      name: ['', Validators.required],
      userName: ['', Validators.required],
      email: ['', Validators.required],
      address: ['', Validators.required],
      FileImage: ['', Validators.required],
    });
  }

  logout() {
    this.loginService.logout();
  }

  editProfile() {
    this.visible = !this.visible;
    this.getByIdUser();
  }

  save = () => {};

  getByIdUser() {
    this.loginService
      .getByIdUser(this.loginService.getCurrentUser()?.uid ?? '')
      .subscribe((res: any) => {
        this.Name = res.name;
        this.Role = res.userName;
        this.Address = res.address;

        this.FormProfile.controls['name'].setValue(res.name);
        this.FormProfile.controls['userName'].setValue(res.userName);
        this.FormProfile.controls['email'].setValue(res.email);
        this.FormProfile.controls['address'].setValue(res.address);
      });
  }

  onFileChanged(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e) => (this.urlFist = reader.result as string);
      this.FormProfile.patchValue({
        FileImage: file,
      });
      reader.readAsDataURL(file);
    }
  }
}
