import { Component, OnInit } from '@angular/core';
import { SurveyCreatorModel } from 'survey-creator-core';
import { Model } from 'survey-core';
import { themeJson } from './theme';
import { jsonDataFake } from './json';

import { ClientHomeService } from '@app/services';
import { first } from 'rxjs';

const creatorOptions = {
  showLogicTab: true,
  isAutoSave: true,
};

const defaultJson = jsonDataFake.config;

@Component({
  selector: 'app-client-home',
  templateUrl: './client-home.component.html',
  styleUrls: ['./client-home.component.css'],
})
export class ClientHomeComponent implements OnInit {
  surveyCreatorModel!: SurveyCreatorModel;
  model!: Model;
  bangKhaoSat?: any;

  constructor(private clientHomeService: ClientHomeService) {}

  ngOnInit() {
    const configSurvey = (configJson: any) => {
      const survey = new Model(configJson);
      // survey.setDataCore(jsonDataFake.data);
      // You can delete the line below if you do not use a customized theme
      survey.applyTheme(themeJson);
      // Set label for btn Complete
      survey.completeText = 'Gửi thông tin';
      survey.addNavigationItem({
        id: 'sv-nav-back-page',
        title: 'Quay lại',
        action: () => {
          //TODO quay lại trang trước
          alert('Comming son!');
        },
        css: 'nav-button',
        innerCss: 'sd-btn nav-input',
      });

      survey.addNavigationItem({
        id: 'sv-nav-clear-page',
        title: 'Khai lại từ đầu',
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
        console.log(JSON.stringify(sender.data, null, 3));
      });

      this.model = survey;
    };

    this.clientHomeService.getAll().subscribe((rep) => {
      this.bangKhaoSat = rep;
    });

    this.clientHomeService
      .getSurveyConfig()
      .pipe(first())
      .subscribe((res) => {
        let pages = defaultJson.pages[0];
        let els = new Array();
        res.forEach((el, i) => {
          let loaiCauHoi = el.loaiCauHoi;
          let name = el.maCauHoi;
          let title = el.tieuDe;
          let isRequired = el.batBuoc ?? false;
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
              description: description,
            });
          } else if (loaiCauHoi === 1) {
            els.push({
              type: 'checkbox',
              name: name,
              title: title,
              isRequired: isRequired,
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
              description: description,
            });
          } else if (loaiCauHoi == 3) {
            els.push({
              type: 'comment',
              name: name,
              title: title,
              isRequired: isRequired,
              description: description,
            });
          } else if (loaiCauHoi == 4) {
            els.push({
              type: 'matrix',
              name: name,
              title: title,
              isRequired: isRequired,
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
      });

    // const creator = new SurveyCreatorModel(creatorOptions);
    // creator.text =
    //   window.localStorage.getItem('survey-json') || JSON.stringify(defaultJson);

    // creator.saveSurveyFunc = (saveNo: number, callback: Function) => {
    //   window.localStorage.setItem("survey-json", creator.text);
    //   callback(saveNo, true);
    // };
    // this.surveyCreatorModel = creator;

    // survey.onComplete.add((sender, options) => {
    //   console.log(JSON.stringify(sender.data, null, 3));
    // });

    // this.model = survey;
  }
}
