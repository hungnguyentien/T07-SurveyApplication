import { FormControl, FormGroup } from '@angular/forms';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { Model } from 'survey-core';
import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { KqSurveyCheckBox, KqTrangThai, TypeCauHoi } from '@app/enums';
import {
  CauHoiConfig,
  CreateBaoCaoCauHoi,
  FileQuestion,
  SurveyConfig,
} from '@app/models';
import { jsonDataFake } from './json';
import { themeJson } from './theme';
import { environment } from '@environments/environment';
import * as crypto from 'crypto-js';
import * as moment from 'moment';
import { PhieuKhaoSatService } from '@app/services';

export default class Utils {
  static messageService: MessageService;
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
      values[i] != null &&
        values[i] != undefined &&
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
      summary: 'Thành công',
      detail: message,
    });
  };

  static messageError = (messageService: MessageService, message: string) => {
    messageService.clear();
    messageService.add({
      severity: 'error',
      summary: 'Lỗi',
      detail: message,
    });
  };

  static messageInfo = (messageService: MessageService, message: string) => {
    messageService.clear();
    messageService.add({
      severity: 'info',
      summary: 'Thông tin',
      detail: message,
    });
  };

  static messageWarring = (messageService: MessageService, message: string) => {
    messageService.clear();
    messageService.add({
      severity: 'warning',
      summary: 'Cảnh báo',
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
    trangThai: number = 0,
    messageService: MessageService | undefined = undefined,
    phieuKhaoSatService: PhieuKhaoSatService | undefined = undefined
  ) => {
    let status = KqTrangThai.HoanThanh;
    const survey = new Model(configJson);
    surveyData && survey.setDataCore(JSON.parse(surveyData));
    // You can delete the line below if you do not use a customized theme
    survey.applyTheme(themeJson);
    survey.locale = 'vi';
    // Set label for btn Complete
    survey.completeText = 'Gửi thông tin';
    survey.onErrorCustomText.add((sender, options) => {
      if (options.name === 'exceedsize') {
        options.text = options.text.replaceAll(
          'The file size should not exceed',
          'Kích thước tệp không được vượt quá'
        );
      }
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
            trangThai === KqTrangThai.HoanThanh &&
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
        router && router.navigate([`/phieu/thong-tin-chung/${data}`]);
      },
      css: 'nav-button',
      innerCss: 'sd-btn nav-input',
    });

    trangThai !== KqTrangThai.HoanThanh &&
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

    trangThai !== KqTrangThai.HoanThanh &&
      survey.addNavigationItem({
        id: 'sv-nav-luu-tam',
        title: 'Lưu tạm',
        visibleIndex: 49,
        action: () => {
          // Lưu tạm khảo sát
          status = KqTrangThai.LuuNhap;
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

    survey.onUploadFiles.add((survey, options: any) => {
      const maxFile = options.question.jsonObj.maxFile;
      if (options.question.prevPreviewLength >= maxFile) {
        messageService &&
          this.messageError(messageService, 'Số lượng tệp đã tối đa');
        options.callback('error');
        // options.files.clear();
      } else if (options.files.length > maxFile) {
        messageService &&
          this.messageError(
            messageService,
            'Bạn đã chọn quá số lượng tệp cho phép vui lòng chọn lại'
          );
        options.callback('error');
        // options.files.clear();
      } else {
        let content = new Array();
        phieuKhaoSatService?.uploadFiles(options.files).subscribe({
          next: (res) => {
            options.files.forEach((file: any) => {
              let fileReader = new FileReader();
              fileReader.onload = function (e) {
                // debugger
                content = content.concat([
                  {
                    name: file.name,
                    type: file.type,
                    content: fileReader.result,
                    path: '',
                    file: file,
                  } as FileQuestion,
                ]);
                if (content.length === options.files.length) {
                  options.callback(
                    'success',
                    content.map(function (fileContent) {
                      return {
                        file: fileContent.file,
                        // content: fileContent.content,
                        path: fileContent.path,
                      };
                    })
                  );
                }
              };
              fileReader.readAsDataURL(file);
            });
          },
        });
      }
    });

    survey.onComplete.add((sender, options) => {
      // Hoàn thành khảo sát
      let data = sender.data;
      if (status !== KqTrangThai.HoanThanh)
        survey.completedHtml = `<div class='custom-complete'> ${
          Object.keys(data).length !== 0
            ? ''
            : `<div><b>Bạn chưa nhập câu trả lời vui lòng khảo sát lại!</b></div>`
        } <a href="phieu/thong-tin-khao-sat">Khảo sát lại</a></div>`;

      Object.keys(data).length !== 0 && subscribe && subscribe(sender, status);
    });

    return survey;
  };

  /**
   * Sắp xếp mảng theo property
   * @param property Thếm dấu trừ là DESC
   * @returns
   */
  static dynamicSort(property: string) {
    let sortOrder = 1;
    if (property[0] === '-') {
      sortOrder = -1;
      property = property.substr(1);
    }
    return (a: any, b: any) => {
      const result =
        a[property] < b[property] ? -1 : a[property] > b[property] ? 1 : 0;
      return result * sortOrder;
    };
  }
  /**
   * configCauHoiEl
   * @param el
   * @param readOnly
   * @param els
   */
  static configCauHoiEl(
    el: CauHoiConfig,
    readOnly: boolean,
    els: any[],
    index: number
  ) {
    let loaiCauHoi = el.loaiCauHoi;
    let name = el.maCauHoi;
    let title = `${index + 1}. ${el.tieuDe}`;
    let isRequired = el.batBuoc ?? false;
    let requiredErrorText = isRequired
      ? 'Vui lòng nhập câu trả lời của bạn!'
      : '';
    let description = el.noidung?.replace(/<\/?[^>]+(>|$)/g, ''); //Remove html tag from string
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
    let isMatrixdropdown = loaiCauHoi == TypeCauHoi.SingleSelectMatrix;
    el.lstCot.forEach((el, i) => {
      columns.push(
        isMatrixdropdown
          ? {
              value: el.maCot,
              text: el.noidung,
            }
          : loaiCauHoi == TypeCauHoi.MultiSelectMatrix
          ? {
              name: el.maCot,
              showInMultipleColumns: true,
              cellType: 'checkbox',
              colCount: 0,
              choices: [
                {
                  value: el.noidung,
                  text: el.noidung,
                },
              ],
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
            value: KqSurveyCheckBox.value,
            text: KqSurveyCheckBox.text,
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
        maxSize: maxSize * 1024 * 1024,
        maxFile: el.soLuongFileToiDa,
        showCommentArea: true,
        commentText: 'Ghi chú',
        readOnly: readOnly,
      });
    }
  }

  static configCauHoi = (res: SurveyConfig): any[] => {
    const els: any[] = [];
    const readOnly = res.trangThaiKq === KqTrangThai.HoanThanh;
    const lstPanelTitle = res.lstCauHoi
      .map((x) => x.panelTitle)
      .filter((x) => x)
      .filter(this.onlyUnique);
    if (lstPanelTitle.length > 0)
      lstPanelTitle.forEach((panelTitle, i) => {
        const elsPanel: any[] = [];
        res.lstCauHoi
          .filter((x) => x.panelTitle === panelTitle)
          .forEach((el, i) => this.configCauHoiEl(el, readOnly, elsPanel, i));
        els.push({
          type: 'panel',
          name: `panel${i}`,
          elements: elsPanel,
          title: `${this.romanize(i + 1)}. ${panelTitle}`,
        });
      });
    else
      res.lstCauHoi.forEach((el, i) =>
        this.configCauHoiEl(el, readOnly, els, i)
      );
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

  static getParamsQuery = (keys: string[], values: any[]) => {
    let params = new Array();
    keys.forEach((el, i) => {
      (values[i] || values[i] === 0) && params.push(`${el}=${values[i]}`);
    });
    return `?${params.join('&')}`;
  };
  /** Distinct mảng */
  static onlyUnique = (value: any, index: number, array: any[]) => {
    return array.indexOf(value) === index;
  };
  /**
   * addOtherItem cho báo cáo
   * @param args [el, dataKq, lstBaoCaoCauHoi, dataDefault]
   */
  static addOtherItem = (...args: any[]) => {
    const [el, dataKq, lstBaoCaoCauHoi, dataDefault] = args;
    if (el && el.showOtherItem && dataKq) {
      lstBaoCaoCauHoi.push({
        ...dataDefault,
        maCauHoi: el.name,
        cauHoi: el.title,
        cauHoiPhu: '',
        maCauHoiPhu: '',
        loaiCauHoi:
          el.type === 'radiogroup' ? TypeCauHoi.Radio : TypeCauHoi.CheckBox,
        maCauTraLoi: `${el.name}-Comment`,
        cauTraLoi: dataKq[`${el.name}-Comment`],
      } as CreateBaoCaoCauHoi);
    }
  };
  /**
   * addDataBaoCao lưu kết quả sau khi ấn hoàn thành
   * @param args
   * @returns
   */
  static addDataBaoCao = (...args: any[]): CreateBaoCaoCauHoi[] => {
    const configCauHoiGroup: any[] = [];
    const [configCauHoi, dataKq, status, lstBaoCaoCauHoi, generalInfo] = args;
    const isHasGroup = configCauHoi.find((x: any) => x.type === 'panel');
    isHasGroup &&
      configCauHoi.forEach((x: any) => configCauHoiGroup.push(x.elements));
    // if (configCauHoi && dataKq) { // dùng để debug
    if (configCauHoi && dataKq && status == KqTrangThai.HoanThanh) {
      let dataDefault = {
        idBangKhaoSat: 0,
        idCauHoi: 0,
        idDotKhaoSat: 0,
        idLoaiHinhDonVi: generalInfo.donVi.idLoaiHinh,
        tenDaiDienCq: generalInfo.nguoiDaiDien.hoTen,
      };
      ((isHasGroup ? configCauHoiGroup.flat() : configCauHoi) as any[]).forEach(
        (el) => {
          if (el.type === 'radiogroup' || el.type === 'checkbox') {
            (el.choices as any[]).forEach((choice, i) => {
              lstBaoCaoCauHoi.push({
                ...dataDefault,
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
                  el.type === 'radiogroup'
                    ? dataKq[el.name] === choice.value
                      ? dataKq[el.name]
                      : ''
                    : dataKq[el.name]
                    ? dataKq[el.name][i]
                    : '',
              } as CreateBaoCaoCauHoi);
            });
            this.addOtherItem(el, dataKq, lstBaoCaoCauHoi, dataDefault);
          } else if (el.type === 'comment' || el.type === 'text') {
            lstBaoCaoCauHoi.push({
              ...dataDefault,
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
                lstBaoCaoCauHoi.push({
                  ...dataDefault,
                  maCauHoi: el.name,
                  cauHoi: el.title,
                  cauHoiPhu: rEl.text,
                  maCauHoiPhu: rEl.value,
                  loaiCauHoi:
                    el.cellType === 'checkbox'
                      ? TypeCauHoi.MultiSelectMatrix
                      : TypeCauHoi.MultiTextMatrix,
                  maCauTraLoi: cEl.name,
                  cauTraLoi: dataKq[el.name]?.[rEl.value]?.[cEl.name]
                    ? JSON.stringify(dataKq[el.name][rEl.value][cEl.name])
                    : '',
                } as CreateBaoCaoCauHoi);
              });
            });
          } else if (el.type === 'matrix') {
            (el.rows as any[]).forEach((rEl) => {
              (el.columns as any[]).forEach((cEl) => {
                lstBaoCaoCauHoi.push({
                  ...dataDefault,
                  maCauHoi: el.name,
                  cauHoi: el.title,
                  cauHoiPhu: rEl.title,
                  maCauHoiPhu: rEl.value,
                  loaiCauHoi: TypeCauHoi.SingleSelectMatrix,
                  maCauTraLoi: cEl.name,
                  cauTraLoi: dataKq[el.name]?.[rEl.value]
                    ? cEl.value == dataKq[el.name][rEl.value]
                      ? cEl.text
                      : ''
                    : '',
                } as CreateBaoCaoCauHoi);
              });
            });
          } else if (el.type === 'file') {
            lstBaoCaoCauHoi.push({
              ...dataDefault,
              maCauHoi: el.name,
              cauHoi: el.title,
              cauHoiPhu: '',
              maCauHoiPhu: '',
              loaiCauHoi: TypeCauHoi.UploadFile,
              maCauTraLoi: '',
              cauTraLoi: dataKq[el.name] ? JSON.stringify(dataKq[el.name]) : '',
            } as CreateBaoCaoCauHoi);
          }
        }
      );
    }

    return lstBaoCaoCauHoi;
  };
  /** Mã hóa theo key bí mật */
  static encrypt = (dataEncrypt: string): string => {
    try {
      return crypto.AES.encrypt(dataEncrypt, environment.secretKey).toString();
    } catch {
      localStorage.removeItem('grand_client');
      return '';
    }
  };
  /** Giải mã theo key bí mật */
  static decrypt = (dataDecrypt: string): string => {
    try {
      return crypto.AES.decrypt(dataDecrypt, environment.secretKey).toString(
        crypto.enc.Utf8
      );
    } catch {
      localStorage.removeItem('grand_client');
      return '';
    }
  };

  static base64ToBytes = (base64: string) => {
    let arr = base64.split(',');
    let s = window.atob(arr[arr.length - 1]);
    let bytes = new Uint8Array(s.length);
    for (let i = 0; i < s.length; i++) bytes[i] = s.charCodeAt(i);
    return bytes;
  };
  /** Tải file base64 */
  static downloadFileBase64 = (file: FileQuestion) => {
    let filename = file.name;
    let type = file.type;
    let bytes = this.base64ToBytes(file.content);
    let blob = new Blob([bytes], { type: type });
    let downloadUrl = URL.createObjectURL(blob);
    let a = document.createElement('a');
    a.href = downloadUrl;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    setTimeout(function () {
      URL.revokeObjectURL(downloadUrl);
    }, 100);
  };
  /** format any sang datetime server + 1 */
  static anyToDateServer(dateStr: any, format: string = 'DD/MM/YYYY') {
    try {
      return moment(dateStr.format(format)).days(1);
    } catch (e) {
      console.error(e);
      return '';
    }
  }

  /** format any sang datetime server + 1 */
  static plusDate(dateStr: any, format: string = 'DD/MM/YYYY') {
    return moment(dateStr, format).add(1, 'days').toDate();
  }
  /** Convert số sang số la mã */
  static romanize = (num: number) => {
    let digits = String(+num).split(''),
      key = [
        '',
        'C',
        'CC',
        'CCC',
        'CD',
        'D',
        'DC',
        'DCC',
        'DCCC',
        'CM',
        '',
        'X',
        'XX',
        'XXX',
        'XL',
        'L',
        'LX',
        'LXX',
        'LXXX',
        'XC',
        '',
        'I',
        'II',
        'III',
        'IV',
        'V',
        'VI',
        'VII',
        'VIII',
        'IX',
      ],
      roman = '',
      i = 3;
    while (i--) roman = (key[+(digits?.pop() ?? 0) + i * 10] || '') + roman;
    return Array(+digits.join('') + 1).join('M') + roman;
  };
}
