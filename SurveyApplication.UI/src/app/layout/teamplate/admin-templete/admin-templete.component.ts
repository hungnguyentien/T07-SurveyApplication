import { Component } from '@angular/core';
import { Validators ,FormBuilder,FormGroup} from '@angular/forms';
import { LoginService } from '@app/services';
import { MessageService } from 'primeng/api';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-admin-templete',
  templateUrl: './admin-templete.component.html',
  styleUrls: ['./admin-templete.component.css'],
  providers: [MessageService]
})
export class AdminTempleteComponent{
  userName: string = '';
  FormProfile!:FormGroup;
  visible: boolean = false;
  urlFist!: string;
  valueIdUser!:any;

  listDatasUser!:any;
   Name!:string;
   Role!:string;
   Address!:string

  constructor(private loginService: LoginService,
    private FormBuilder :FormBuilder,
    private messageService: MessageService,
    private cookieService:CookieService,
    ) {
    
    this.userName = loginService.getCurrentUser()?.name;
  }
  ngOnInit() {
    this.FormProfile = this.FormBuilder.group(
      {
        
        name: ['', Validators.required],
        userName: ['', Validators.required],
        email: ['', Validators.required],
        address: ['', Validators.required],
        FileImage:['',Validators.required]
        // password: ['', Validators.required],
      }
    );
    this.getByIdUser();
  }
  logout() {
    this.loginService.logout();
    
  }
  EditHS(){
    this.visible = !this.visible;
    this.getByIdUser()
  }
  Save = () =>{

  }

  getByIdUser(){
    this.valueIdUser = this.cookieService.get('IdcurrentUser');
    this.loginService.getByIdUser(this.valueIdUser).subscribe((res:any)=>{
      this.listDatasUser = res
      this.Name = res.name
      this.Role = res.userName
      this.Address=res.address
    })
    this.FormProfile.controls['name'].setValue(this.listDatasUser.name);
    this.FormProfile.controls['userName'].setValue(this.listDatasUser.userName);
    this.FormProfile.controls['email'].setValue(this.listDatasUser.email);
    this.FormProfile.controls['address'].setValue(this.listDatasUser.address);
    // this.FormProfile.controls['name'].setValue(this.listDatasUser.name);
  }


  onFileChanged(event:any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = e => this.urlFist = reader.result as string;
      this.FormProfile.patchValue({
        FileImage : file,
      });
      reader.readAsDataURL(file)
    }
  }
}
