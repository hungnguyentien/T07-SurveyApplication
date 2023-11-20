import { Component } from '@angular/core';
import { Model } from 'survey-core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Router } from '@angular/router';

import { PhieuKhaoSatService } from '@app/services';
import Utils from '@app/helpers/utils';
import {
  CreateBaoCaoCauHoi,
  CreateBaoCaoCauHoiCommand,
  GeneralInfo,
  SaveSurvey,
} from '@app/models';
import 'survey-core/survey.i18n';
import { KqTrangThai } from '@app/enums';
import { environment } from '@environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-survey-enterprise',
  templateUrl: './survey-enterprise.component.html',
  styleUrls: ['./survey-enterprise.component.css'],
})
export class SurveyEnterpriseComponent {
  model!: Model;
  generalInfo!: GeneralInfo;
  saveSurvey!: SaveSurvey;
  lstBaoCaoCauHoi: CreateBaoCaoCauHoi[] = [];
  note: boolean = true;
  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private messageService: MessageService,
    private phieuKhaoSatService: PhieuKhaoSatService,
    private confirmationService: ConfirmationService
  ) {}

  async ngOnInit() {
    this.generalInfo = history.state;
    let ipClient = localStorage.getItem('grand_client');
    if (!ipClient) {
      try {
        ipClient = JSON.stringify(
          await (await fetch(environment.apiIp, { mode: 'no-cors' })).json()
        );
        localStorage.setItem('grand_client', Utils.encrypt(ipClient));
      } catch (e) {
        ipClient = '';
        console.log(e);
      }
    }

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
          this.lstBaoCaoCauHoi = Utils.addDataBaoCao(
            configCauHoi,
            dataKq,
            status,
            [],
            this.generalInfo
          );
          this.saveSurvey = {
            data: JSON.stringify(dataKq),
            guiEmail: this.generalInfo.data ?? '',
            trangThai: status,
            ipAddressClient: ipClient ? Utils.decrypt(ipClient) : '',
          };
          this.spinner.show();
          this.phieuKhaoSatService.saveSurvey(this.saveSurvey).subscribe({
            next: (res) => {
              res.success
                ? Utils.messageSuccess(this.messageService, res.message)
                : Utils.messageError(
                    this.messageService,
                    res.errors?.at(0) ?? ''
                  );
              //TODO đồng bộ kết quả sau khi hoàn thành khảo sát
              if (res.success && status === KqTrangThai.HoanThanh) {
                Utils.downloadPhieu(
                  this.phieuKhaoSatService,
                  this.generalInfo.data
                );

                let data = {
                  lstBaoCaoCauHoi: this.lstBaoCaoCauHoi,
                  idGuiEmail: this.generalInfo.data,
                } as CreateBaoCaoCauHoiCommand;
                this.phieuKhaoSatService.dongBoBaoCaoCauHoi(data).subscribe({
                  next: (res) => {
                    console.log(res);
                  },
                  error: (e) => {
                    console.error(e);
                  },
                });
              }
            },
            error: (e) => {
              this.spinner.hide();
            },
            complete: () => {
              this.spinner.hide();
              this.note = false;
            },
          });
        },
        `${this.generalInfo.data}`,
        surveyData,
        trangThai,
        this.messageService,
        this.phieuKhaoSatService,
        'phieu/khao-sat-thong-tin-chung',
        'phieu/khao-sat-doanh-nghiep',
        this.confirmationService
      );
    };

    this.spinner.show();
    this.phieuKhaoSatService
      .getSurveyConfig(this.generalInfo.data)
      .subscribe((res) => {
        configSurvey(Utils.getJsonSurvey(res), res.kqSurvey, res.trangThaiKq);
        this.spinner.hide();
      });
  }
}
