import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Utils from '@app/helpers/utils';
import { PhieuKhaoSatDoanhNghiep } from '@app/models';
import { PhieuKhaoSatService } from '@app/services';
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

  ngOnInit() {
    this.keyWord = this.activatedRoute.snapshot.queryParams['maDoanhNghiep'] ?? '';
  }

  onSubmitSearch = () => {
    this.phieuKhaoSatService.searchSurveyByDonVi(this.keyWord).subscribe({
      next: (res) => {
        if(res.data.length == 0){
          Utils.messageInfo(this.messageService, 'Không tìm thấy kết quả phù hợp');
        }
        else if(res.data.length > 1){
          this.datas = res.data;
        }else{
          this.router.navigate([`/phieu/khao-sat-thong-tin-chung/${res.data[0].linkKhaoSat}`]);
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
