import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PhieuKhaoSatDoanhNghiep } from '@app/models';
import { PhieuKhaoSatService } from '@app/services';

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
    private phieuKhaoSatService: PhieuKhaoSatService
  ) {}

  ngOnInit() {}

  onSubmitSearch = () => {
    this.phieuKhaoSatService.searchSurveyByDonVi(this.keyWord).subscribe({
      next: (res) => {
        this.datas = res.data;
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
