import { Component } from '@angular/core';
import { Model } from 'survey-core';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';

import { themeJson } from './theme';
import { jsonDataFake } from './json';
import { PhieuKhaoSatService } from '@app/services';
import Utils from '@app/helpers/utils';
import { GeneralInfo, SaveSurvey } from '@app/models';
import 'survey-core/survey.i18n';

const defaultJson = jsonDataFake.config;

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
    const configSurvey = (configJson: any) => {
      this.model = Utils.configSurvey(
        configJson,
        themeJson,
        this.router,
        (sender: Model) => {
          this.saveSurvey = {
            data: JSON.stringify(sender.data),
            idBangKhaoSat: 1,
            idDonVi: this.generalInfo.donVi.id,
            idNguoiDaiDien: this.generalInfo.nguoiDaiDien.id,
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
        }
      );
    };

    this.loading = true;
    this.phieuKhaoSatService.getSurveyConfig().subscribe((res) => {
      let pages = defaultJson.pages[0];
      let els = new Array();
      res.forEach((el, i) => {
        let loaiCauHoi = el.loaiCauHoi;
        let name = el.maCauHoi;
        let title = `${el.tieuDe}`;
        let isRequired = el.batBuoc ?? false;
        let requiredErrorText = isRequired
          ? 'Vui lòng nhập câu trả lời của bạn!'
          : '';
        let description = el.noidung;

        let labelTrue = el.lstCot[0]?.noidung;
        let labelFalse = el.lstCot[1]?.noidung;
        let choices = new Array();
        el.lstCot.forEach((el, i) => {
          choices.push({
            value: el.noidung,
            text: `${i + 1}. ${el.noidung}`,
          });
        });
        let showOtherItem = el.isOther ?? false;
        let otherPlaceholder = showOtherItem ? 'Câu trả lời của bạn' : '';
        let otherText = showOtherItem ? el.labelCauTraLoi : '';

        let columns = new Array();
        let isMatrixdropdown = loaiCauHoi == 4;
        el.lstCot.forEach((el, i) => {
          columns.push(
            isMatrixdropdown
              ? {
                  value: el.maCot,
                  text: el.noidung,
                }
              : {
                  name: el.maCot,
                  title: el.noidung,
                }
          );
        });
        let rows = new Array();
        el.lstHang.forEach((el, i) => {
          rows.push({
            value: el.maHang,
            text: el.noidung,
          });
        });

        let maxSize = el.kichThuocFile;
        if (loaiCauHoi === 0) {
          els.push({
            type: 'boolean',
            name: name,
            title: title,
            defaultValue: 'true',
            labelTrue: labelTrue,
            labelFalse: labelFalse,
            isRequired: isRequired,
            requiredErrorText: requiredErrorText,
            description: description,
          });
        } else if (loaiCauHoi === 1) {
          els.push({
            type: 'checkbox',
            name: name,
            title: title,
            isRequired: isRequired,
            requiredErrorText: requiredErrorText,
            description: description,
            choices: choices,
            showOtherItem: showOtherItem,
            otherPlaceholder: otherPlaceholder,
            otherText: otherText,
          });
        } else if (loaiCauHoi == 2) {
          els.push({
            type: 'text',
            name: name,
            title: title,
            isRequired: isRequired,
            requiredErrorText: requiredErrorText,
            description: description,
          });
        } else if (loaiCauHoi == 3) {
          els.push({
            type: 'comment',
            name: name,
            title: title,
            isRequired: isRequired,
            requiredErrorText: requiredErrorText,
            description: description,
          });
        } else if (loaiCauHoi == 4) {
          els.push({
            type: 'matrix',
            name: name,
            title: title,
            isRequired: isRequired,
            requiredErrorText: requiredErrorText,
            description: description,
            alternateRows: true,
            columns: columns,
            rows: rows,
          });
        } else if (loaiCauHoi == 5) {
          els.push({
            type: 'matrixdropdown',
            name: name,
            title: title,
            isRequired: isRequired,
            requiredErrorText: requiredErrorText,
            description: description,
            alternateRows: true,
            columns: columns,
            rows: rows,
            choices: [
              {
                value: '1',
                text: 'Có',
              },
            ],
            cellType: 'checkbox',
            columnColCount: 1,
          });
        } else if (loaiCauHoi == 6) {
          els.push({
            type: 'matrixdropdown',
            name: name,
            title: title,
            isRequired: isRequired,
            requiredErrorText: requiredErrorText,
            description: description,
            alternateRows: true,
            columns: columns,
            rows: rows,
            choices: [
              {
                value: '1',
                text: 'Có',
              },
            ],
            cellType: 'text',
            columnColCount: 1,
          });
        } else if (loaiCauHoi == 7) {
          els.push({
            type: 'file',
            name: name,
            title: title,
            isRequired: isRequired,
            requiredErrorText: requiredErrorText,
            description: description,
            storeDataAsText: false,
            allowMultiple: true,
            maxSize: maxSize,
            showCommentArea: true,
            commentText: 'Ghi chú',
          });
        }
      });

      pages.elements = els;
      configSurvey(defaultJson);
      this.loading = false;
    });
  }
}
