import { Component } from '@angular/core';
import { Model } from 'survey-core';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';

import { PhieuKhaoSatService } from '@app/services';
import Utils from '@app/helpers/utils';
import { GeneralInfo, SaveSurvey } from '@app/models';
import 'survey-core/survey.i18n';

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
          this.saveSurvey = {
            data: JSON.stringify(sender.data),
            guiEmail: this.generalInfo.data ?? '',
            trangThai: status,
          };
          this.loading = true;
          this.phieuKhaoSatService.saveSurvey(this.saveSurvey).subscribe({
            next: (res) => {
              res.success
                ? Utils.messageSuccess(this.messageService, res.message)
                : Utils.messageError(
                    this.messageService,
                    res.errors?.at(0) ?? ''
                  );
            },
            error: (e) => {
              Utils.messageError(this.messageService, e.message);
              this.loading = false;
            },
            complete: () => {
              this.loading = false;
            },
          });
        },
        `${this.generalInfo.data}`,
        surveyData,
        trangThai
      );
    };

    this.loading = true;
    this.phieuKhaoSatService
      .getSurveyConfig(
        this.generalInfo.data
      )
      .subscribe((res) => {
        configSurvey(Utils.getJsonSurvey(res), res.kqSurvey, res.trangThai);
        this.loading = false;
      });
  }
}
