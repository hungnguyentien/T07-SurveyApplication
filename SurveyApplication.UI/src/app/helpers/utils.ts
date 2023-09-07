import { FormControl, FormGroup } from '@angular/forms';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { Model } from 'survey-core';
import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { BksTrangThai, TypeCauHoi } from '@app/enums';
import { SurveyConfig } from '@app/models';
import { jsonDataFake } from './json';
import { themeJson } from './theme';

export default class Utils {
  static translate = (
    lang: string,
    service: TranslateService,
    config: PrimeNGConfig
  ) => {
    service.use(lang);
    service.get('primeng').subscribe((res) => config.setTranslation(res));
  };

  static getFormControl = (
    frm: FormGroup<any>,
    name: string,
    subName: string = ''
  ): FormControl => {
    return (
      subName ? frm?.get(name)?.get(subName) : frm?.get(name)
    ) as FormControl;
  };

  static setValueFormNetted = (
    frm: FormGroup<any>,
    nettedField: string,
    keys: string[],
    values: any[]
  ) => {
    keys.forEach((el, i) => {
      Utils.getFormControl(
        frm,
        Utils.capitalizeFirstLetter(nettedField),
        Utils.capitalizeFirstLetter(el)
      )?.setValue(values[i].toString());
    });
  };

  static setValueForm = (
    frm: FormGroup<any>,
    keys: string[],
    values: any[],
    capitalize: boolean = false
  ) => {
    !capitalize
      ? keys.forEach((el, i) => {
          try {
            let value = values[i];
            if (typeof values[i] === 'string' && !value) value = '';
            Utils.getFormControl(frm, el)?.setValue(value);
          } catch {}
        })
      : keys.forEach((el, i) => {
          let value = values[i];
          if (typeof values[i] === 'string' && !value) value = '';
          Utils.getFormControl(frm, Utils.capitalizeFirstLetter(el))?.setValue(
            value
          );
        });
  };

  static capitalizeFirstLetter = (string: string): string => {
    return string.charAt(0).toUpperCase() + string.slice(1);
  };

  static messageSuccess = (messageService: MessageService, message: string) => {
    messageService.clear();
    messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: message,
    });
  };

  static messageError = (messageService: MessageService, message: string) => {
    messageService.clear();
    messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: message,
    });
  };

  static messageInfo = (messageService: MessageService, message: string) => {
    messageService.clear();
    messageService.add({
      severity: 'info',
      summary: 'info',
      detail: message,
    });
  };

  static messageWarring = (messageService: MessageService, message: string) => {
    messageService.clear();
    messageService.add({
      severity: 'warring',
      summary: 'Warring',
      detail: message,
    });
  };

  static resetForm = (frm: FormGroup<any>) => {
    return frm?.reset();
  };

  static configSurvey = (
    configJson: any,
    router: Router | undefined,
    subscribe: Function | undefined,
    data: string = '',
    surveyData: string = '',
    trangThai: number = 0
  ) => {
    let status = 2;
    const survey = new Model(configJson);
    surveyData && survey.setDataCore(JSON.parse(surveyData));
    // You can delete the line below if you do not use a customized theme
    survey.applyTheme(themeJson);
    survey.locale = 'vi';
    // Set label for btn Complete
    survey.completeText = 'Gửi thông tin';
    if (trangThai !== BksTrangThai.HoanThanh)
      survey.completedHtml = `<div style="margin: 40px 0 0 0; width:100%"><a href="phieu/thong-tin-khao-sat" style="margin: auto">Khảo sát lại</a></div>`;
    
    survey.onErrorCustomText.add((sender, options) => {
      if (options.name === 'exceedsize')
        options.text = options.text.replaceAll(
          'The file size should not exceed',
          'Kích thước tệp không được vượt quá'
        );
    });

    survey.onAfterRenderSurvey.add((sender, options) => {
      options.htmlElement
        .querySelectorAll(
          '#sv-nav-back-page, #sv-nav-clear-page, #sv-nav-complete'
        )
        .forEach((el, i) => {
          el.id === 'sv-nav-back-page' &&
            el.insertAdjacentHTML(
              'afterbegin',
              '<i class="icons icon-quay-lai"></i>'
            );

          el.id === 'sv-nav-clear-page' &&
            el.insertAdjacentHTML(
              'afterbegin',
              '<i class="icons icon-khai-lai-tu-dau"></i>'
            );

          el.id === 'sv-nav-complete' &&
            el.insertAdjacentHTML(
              'afterbegin',
              '<i class="icons icon-gui-thong-tin"></i>'
            );

          if (
            trangThai === BksTrangThai.HoanThanh &&
            el.id === 'sv-nav-complete'
          )
            el.classList.add('d-none');
        });
    });

    survey.addNavigationItem({
      id: 'sv-nav-back-page',
      title: 'Quay lại',
      visibleIndex: 47,
      action: () => {
        //TODO quay lại trang trước
        router &&
          router.navigate(
            ['/phieu/thong-tin-chung'],
            data
              ? {
                  queryParams: { data: data },
                }
              : undefined
          );
      },
      css: 'nav-button',
      innerCss: 'sd-btn nav-input',
    });

    trangThai !== BksTrangThai.HoanThanh &&
      survey.addNavigationItem({
        id: 'sv-nav-clear-page',
        title: 'Khai lại từ đầu',
        visibleIndex: 48,
        action: () => {
          survey.currentPage.questions.forEach((question: any) => {
            question.value = undefined;
            question.commentElements &&
              question.commentElements.forEach(
                (comment: any) => (comment.value = '')
              );
          });
        },
        css: 'nav-button',
        innerCss: 'sd-btn nav-input',
      });

    trangThai !== BksTrangThai.HoanThanh &&
      survey.addNavigationItem({
        id: 'sv-nav-luu-tam',
        title: 'Lưu tạm',
        visibleIndex: 49,
        action: () => {
          // Lưu tạm khảo sát
          status = 0;
          survey.completeLastPage();
        },
        css: 'nav-button',
        innerCss: 'sd-btn nav-input',
      });

    survey.onClearFiles.add((survey, options) => {
      // Get temp files for this question
      let tempFiles = new Array(options.name);
      let fileInfoToRemove = !!tempFiles
        ? tempFiles.filter(function (file: any) {
            return file.name === options.fileName;
          })[0]
        : undefined;
      if (fileInfoToRemove !== undefined) {
        var index = tempFiles.indexOf(fileInfoToRemove);
        tempFiles.splice(index, 1);
      }
      // Code to remove temporary stored files
      // Write your own code to remove files fron server if they were loaded already
      // and then invoke success and allow to proceed further
      options.callback('success');
    });

    survey.onUploadFiles.add((survey, options) => {
      let content = new Array();
      options.files.forEach((file) => {
        let fileReader = new FileReader();
        fileReader.onload = function (e) {
          content = content.concat([
            {
              name: file.name,
              type: file.type,
              content: fileReader.result,
              file: file,
            },
          ]);
          if (content.length === options.files.length) {
            options.callback(
              'success',
              content.map(function (fileContent) {
                return {
                  file: fileContent.file,
                  content: fileContent.content,
                };
              })
            );
          }
        };
        fileReader.readAsDataURL(file);
      });
    });

    survey.onComplete.add((sender, options) => {
      // Hoàn thành khảo sát
      subscribe && subscribe(sender, status);
    });

    return survey;
  };

  static configCauHoi = (res: SurveyConfig) => {
    let els = new Array();
    let readOnly = res.trangThai === BksTrangThai.HoanThanh;
    res.lstCauHoi.forEach((el, i) => {
      let loaiCauHoi = el.loaiCauHoi;
      let name = el.maCauHoi;
      let title = `${el.tieuDe}`;
      let isRequired = el.batBuoc ?? false;
      let requiredErrorText = isRequired
        ? 'Vui lòng nhập câu trả lời của bạn!'
        : '';
      let description = el.noidung;
      let choicesRadio = new Array();
      el.lstCot.forEach((el, i) => {
        choicesRadio.push({
          value: el.noidung,
          text: `${el.noidung}`,
        });
      });
      let choices = new Array();
      el.lstCot.forEach((el, i) => {
        choices.push({
          value: el.noidung,
          text: `${el.noidung}`,
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
      if (loaiCauHoi === TypeCauHoi.Radio) {
        els.push({
          type: 'radiogroup',
          name: name,
          title: title,
          isRequired: isRequired,
          requiredErrorText: requiredErrorText,
          description: description,
          choices: choicesRadio,
          showOtherItem: showOtherItem,
          otherPlaceholder: otherPlaceholder,
          otherText: otherText,
          readOnly: readOnly,
        });
      } else if (loaiCauHoi === TypeCauHoi.CheckBox) {
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
          readOnly: readOnly,
        });
      } else if (loaiCauHoi == TypeCauHoi.Text) {
        els.push({
          type: 'text',
          name: name,
          title: title,
          isRequired: isRequired,
          requiredErrorText: requiredErrorText,
          description: description,
          readOnly: readOnly,
        });
      } else if (loaiCauHoi == TypeCauHoi.LongText) {
        els.push({
          type: 'comment',
          name: name,
          title: title,
          isRequired: isRequired,
          requiredErrorText: requiredErrorText,
          description: description,
          readOnly: readOnly,
        });
      } else if (loaiCauHoi == TypeCauHoi.SingleSelectMatrix) {
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
          readOnly: readOnly,
        });
      } else if (loaiCauHoi == TypeCauHoi.MultiSelectMatrix) {
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
          readOnly: readOnly,
        });
      } else if (loaiCauHoi == TypeCauHoi.MultiTextMatrix) {
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
          readOnly: readOnly,
        });
      } else if (loaiCauHoi == TypeCauHoi.UploadFile) {
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
          readOnly: readOnly,
        });
      }
    });
    return els;
  };

  static getJsonSurvey = (res: any) => {
    let defaultJson = jsonDataFake.config;
    let els = this.configCauHoi(res);
    let pages = defaultJson.pages[0];
    pages.elements = [];
    els.map((el) => {
      (pages.elements as any[]).push(el);
    });
    return defaultJson;
  };

  static getThemeSurvey = () => {
    return themeJson;
  };

  static getParams = (keys: string[], values: string[]) => {
    let params = new HttpParams();
    keys.forEach((el, i) => params.set(el, values[i]));
    return params;
  };

  static getParamsQuery = (keys: string[], values: string[]) => {
    let params = new Array();
    keys.forEach((el, i) => {
      values[i] && params.push(`${el}=${values[i]}`);
    });
    return `?${params.join('&')}`;
  };
}
