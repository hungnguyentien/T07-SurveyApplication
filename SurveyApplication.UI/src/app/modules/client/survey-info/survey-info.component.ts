import { Component } from '@angular/core';
import { Model } from 'survey-core';
import { MessageService } from 'primeng/api';
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
  lstBaoCaoCauHoi: CreateBaoCaoCauHoi[] = [];

  constructor(
    private phieuKhaoSatService: PhieuKhaoSatService,
    private router: Router,
    private messageService: MessageService
  ) {}

  addOtherItem = (...args: any[]) => {
    const [el, dataKq] = args;
    if (el && el.showOtherItem && dataKq) {
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
  };

  addDataBaoCao = (...args: any[]) => {
    const [configCauHoi, dataKq, status] = args;
    if (configCauHoi && dataKq && status == KqTrangThai.HoanThanh) {
      (configCauHoi as any[]).forEach((el) => {
        if (el.type === 'radiogroup' || el.type === 'checkbox') {
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
              loaiCauHoi:
                el.type === 'radiogroup'
                  ? TypeCauHoi.Radio
                  : TypeCauHoi.CheckBox,
              maCauTraLoi: choice.text,
              cauTraLoi:
                dataKq[el.name] === choice.value ? dataKq[el.name] : '',
            } as CreateBaoCaoCauHoi);
          });
          this.addOtherItem(el, dataKq);
        } else if (el.type === 'comment' || el.type === 'text') {
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
            loaiCauHoi:
              el.type === 'comment' ? TypeCauHoi.LongText : TypeCauHoi.Text,
            maCauTraLoi: '',
            cauTraLoi: dataKq[el.name] ? JSON.stringify(dataKq[el.name]) : '',
          } as CreateBaoCaoCauHoi);
        } else if (el.type === 'matrixdropdown') {
          (el.rows as any[]).forEach((rEl) => {
            (el.columns as any[]).forEach((cEl) => {
              this.lstBaoCaoCauHoi.push({
                idBangKhaoSat: 0,
                idCauHoi: 0,
                idDotKhaoSat: 0,
                idLoaiHinhDonVi: this.generalInfo.donVi.idLoaiHinh,
                tenDaiDienCq: this.generalInfo.nguoiDaiDien.hoTen,

                maCauHoi: el.name,
                cauHoi: el.title,
                cauHoiPhu: rEl.text,
                maCauHoiPhu: rEl.value,
                loaiCauHoi:
                  el.cellType === 'checkbox'
                    ? TypeCauHoi.MultiSelectMatrix
                    : TypeCauHoi.MultiTextMatrix,
                maCauTraLoi: cEl.name,
                cauTraLoi: dataKq[el.name][rEl.value][cEl.value]
                  ? JSON.stringify(dataKq[el.name][rEl.value][cEl.value])
                  : '',
              } as CreateBaoCaoCauHoi);
            });
          });
        } else if (el.type === 'matrix') {
          (el.rows as any[]).forEach((rEl) => {
            (el.columns as any[]).forEach((cEl) => {
              this.lstBaoCaoCauHoi.push({
                idBangKhaoSat: 0,
                idCauHoi: 0,
                idDotKhaoSat: 0,
                idLoaiHinhDonVi: this.generalInfo.donVi.idLoaiHinh,
                tenDaiDienCq: this.generalInfo.nguoiDaiDien.hoTen,

                maCauHoi: el.name,
                cauHoi: el.title,
                cauHoiPhu: rEl.title,
                maCauHoiPhu: rEl.name,
                loaiCauHoi: TypeCauHoi.SingleSelectMatrix,
                maCauTraLoi: cEl.name,
                cauTraLoi: dataKq[el.name][rEl.name]
                  ? cEl.value == dataKq[el.name][rEl.name]
                    ? cEl.text
                    : ''
                  : '',
              } as CreateBaoCaoCauHoi);
            });
          });
        } else if (el.type === 'file') {
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
            loaiCauHoi: TypeCauHoi.UploadFile,
            maCauTraLoi: '',
            cauTraLoi: dataKq[el.name] ? JSON.stringify(dataKq[el.name]) : '',
          } as CreateBaoCaoCauHoi);
        }
      });
    }
  };

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
          this.addDataBaoCao(configCauHoi, dataKq, status);
          this.saveSurvey = {
            data: JSON.stringify(dataKq),
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
              //TODO đồng bộ kết quả sau khi lưu khảo sát
              if (res.success && status === KqTrangThai.HoanThanh) {
                let data = {
                  lstBaoCaoCauHoi: this.lstBaoCaoCauHoi,
                  idGuiEmail: '0dAdviHHkd5T2Odc-MCP2gCHq__WZ6EH',
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
      .getSurveyConfig(this.generalInfo.data)
      .subscribe((res) => {
        configSurvey(Utils.getJsonSurvey(res), res.kqSurvey, res.trangThaiKq);
        this.loading = false;
      });
  }
}
