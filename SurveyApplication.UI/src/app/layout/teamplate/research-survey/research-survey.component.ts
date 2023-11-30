import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Utils from '@app/helpers/utils';
import { LogNhanMail, PhieuKhaoSatDoanhNghiep } from '@app/models';
import { PhieuKhaoSatService } from '@app/services';
import { environment } from '@environments/environment';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-research-survey',
  templateUrl: './research-survey.component.html',
  styleUrls: ['./research-survey.component.css'],
})
export class ResearchSurveyComponent {
  keyWord!: string;
  datas!: PhieuKhaoSatDoanhNghiep[];
  constructor(
    private router: Router,
    private messageService: MessageService,
    private activatedRoute: ActivatedRoute,
    private phieuKhaoSatService: PhieuKhaoSatService
  ) {}

  async ngOnInit() {
    this.keyWord =
      this.activatedRoute.snapshot.queryParams['maDoanhNghiep'] ?? '';
    let ipClient = localStorage.getItem('grand_client');
    if (!ipClient) {
      try {
        const request = await fetch(environment.apiIp);
        const jsonResponse = await request.json();
        ipClient = Utils.encrypt(JSON.stringify(jsonResponse));
        localStorage.setItem('grand_client', ipClient);
      } catch (e) {
        ipClient = '';
        console.log(e);
      }
    }

    this.phieuKhaoSatService
      .logNhanMail({
        maDoanhNghiep: this.keyWord,
        data: Utils.decrypt(ipClient),
      } as LogNhanMail)
      .subscribe();
  }

  onSubmitSearch = () => {
    this.phieuKhaoSatService.searchSurveyByDonVi(this.keyWord).subscribe({
      next: (res) => {
        if (res.data.length == 0) {
          Utils.messageInfo(
            this.messageService,
            'Không tìm thấy kết quả phù hợp'
          );
        } else if (res.data.length > 1) {
          this.datas = res.data;
        } else {
          this.router.navigate([
            `/phieu/khao-sat-thong-tin-chung/${res.data[0].linkKhaoSat}`,
          ]);
        }
      },
      complete: () => {},
    });
  };

  khaoSat = (data: string) => {
    this.router.navigate([`/phieu/khao-sat-thong-tin-chung/${data}`]);
  };

  pageTop = () => {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
  };
}
