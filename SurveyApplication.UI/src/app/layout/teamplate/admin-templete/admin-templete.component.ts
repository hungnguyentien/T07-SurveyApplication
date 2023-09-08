import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthGuardService } from '@app/helpers/auth-guard.service';
import { AuthService } from '@app/services/auth.service';
import { LoginserviceService } from '@app/services';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-admin-templete',
  templateUrl: './admin-templete.component.html',
  styleUrls: ['./admin-templete.component.css']
})
export class AdminTempleteComponent {
 constructor(private loginService: AuthService,private router: Router){}
 logout(){
  
    this.loginService.logout()
    this.router.navigate(['/login']);
  }
}
