import { FormControl, FormGroup } from '@angular/forms';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { Model } from 'survey-core';
import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { trigger } from '@angular/animations';

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
    themeJson: any,
    router: Router,
    subscribe: Function,
    data: string = '',
    surveyData: string = ''
  ) => {
    let status = 1;
    const survey = new Model(configJson);
    surveyData && survey.setDataCore(JSON.parse(surveyData));
    // You can delete the line below if you do not use a customized theme
    survey.applyTheme(themeJson);
    survey.locale = 'vi';
    // Set label for btn Complete
    survey.completeText = 'Gửi thông tin';
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

          if (surveyData && el.id === 'sv-nav-complete')
            el.classList.add('d-none');
        });
    });

    survey.addNavigationItem({
      id: 'sv-nav-back-page',
      title: 'Quay lại',
      visibleIndex: 47,
      action: () => {
        //TODO quay lại trang trước
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

    !surveyData &&
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

    !surveyData &&
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
      subscribe(sender, status);
    });

    return survey;
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
