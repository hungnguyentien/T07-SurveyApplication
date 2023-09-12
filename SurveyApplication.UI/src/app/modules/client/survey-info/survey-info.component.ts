import { Component } from '@angular/core';
import { Model } from 'survey-core';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';

import { PhieuKhaoSatService } from '@app/services';
import Utils from '@app/helpers/utils';
import { CreateBaoCaoCauHoi, GeneralInfo, SaveSurvey } from '@app/models';
import 'survey-core/survey.i18n';
import { KqTrangThai, TypeCauHoi } from '@app/enums';

@Component({
  selector: 'app-survey-info',
  templateUrl: './survey-info.component.html',
  styleUrls: ['./survey-info.component.css'],
})
export class SurveyInfoComponent {
  model!: Model;
  loading!: boolean;
  generalInfo!: GeneralInfo;
  saveSurvey!: SaveSurvey;
  lstBaoCaoCauHoi!: CreateBaoCaoCauHoi[];

  constructor(
    private phieuKhaoSatService: PhieuKhaoSatService,
    private router: Router,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.generalInfo = history.state;
    const configSurvey = (
      configJson: any,
      surveyData: string,
      trangThai: number
    ) => {
      this.model = Utils.configSurvey(
        configJson,
        this.router,
        (sender: Model, status: number) => {
          let dataKq = sender.data;
          let configCauHoi: any[] = (sender as any).jsonObj?.pages[0]?.elements;
          // if(configCauHoi && dataKq && status == KqTrangThai.HoanThanh){
          if (configCauHoi && dataKq) {
            configCauHoi.forEach((el) => {
              if (el.type === 'radiogroup') {
                (el.choices as any[]).forEach((choice) => {
                  this.lstBaoCaoCauHoi.push({
                    idBangKhaoSat: 0,
                    idCauHoi: 0,
                    idDotKhaoSat: 0,
                    idLoaiHinhDonVi: this.generalInfo.donVi.idLoaiHinh,
                    tenDaiDienCq: this.generalInfo.nguoiDaiDien.hoTen,

                    maCauHoi: el.name,
                    cauHoi: el.title,
                    cauHoiPhu: '',
                    maCauHoiPhu: '',
                    loaiCauHoi: TypeCauHoi.Radio,
                    maCauTraLoi: choice.text,
                    cauTraLoi:
                      dataKq[el.name] === choice.value ? dataKq[el.name] : '',
                  } as CreateBaoCaoCauHoi);
                });
                if (el.showOtherItem) {
                  this.lstBaoCaoCauHoi.push({
                    idBangKhaoSat: 0,
                    idCauHoi: 0,
                    idDotKhaoSat: 0,
                    idLoaiHinhDonVi: this.generalInfo.donVi.idLoaiHinh,
                    tenDaiDienCq: this.generalInfo.nguoiDaiDien.hoTen,

                    maCauHoi: el.name,
                    cauHoi: el.title,
                    cauHoiPhu: '',
                    maCauHoiPhu: '',
                    loaiCauHoi: TypeCauHoi.Radio,
                    maCauTraLoi: `${el.name}-Comment`,
                    cauTraLoi: dataKq[`${el.name}-Comment`],
                  } as CreateBaoCaoCauHoi);
                }
              }
            });
          }
          debugger;

          this.saveSurvey = {
            data: JSON.stringify(dataKq),
            guiEmail: this.generalInfo.data ?? '',
            trangThai: status,
          };
          this.loading = true;
          // this.phieuKhaoSatService.saveSurvey(this.saveSurvey).subscribe({
          //   next: (res) => {
          //     res.success
          //       ? Utils.messageSuccess(this.messageService, res.message)
          //       : Utils.messageError(
          //           this.messageService,
          //           res.errors?.at(0) ?? ''
          //         );
          //   },
          //   error: (e) => {
          //     Utils.messageError(this.messageService, e.message);
          //     this.loading = false;
          //   },
          //   complete: () => {
          //     this.loading = false;
          //   },
          // });
        },
        `${this.generalInfo.data}`,
        surveyData,
        trangThai
      );
    };

    this.loading = true;
    this.phieuKhaoSatService
      .getSurveyConfig(this.generalInfo.data)
      .subscribe((res) => {
        configSurvey(Utils.getJsonSurvey(res), res.kqSurvey, res.trangThaiKq);
        this.loading = false;
      });
  }
}
