import { Component, OnInit } from '@angular/core';
import { SurveyCreatorModel } from 'survey-creator-core';
import { Model } from 'survey-core';
import { themeJson } from './theme';
import { jsonDataFake } from './json';

import { ClientHomeService } from '@app/services';

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
    // this.clientHomeService.getAll().subscribe((rep) => {
    //   this.bangKhaoSat = rep;
    // });

    const creator = new SurveyCreatorModel(creatorOptions);
    creator.text =
      window.localStorage.getItem('survey-json') || JSON.stringify(defaultJson);

    // creator.saveSurveyFunc = (saveNo: number, callback: Function) => {
    //   window.localStorage.setItem("survey-json", creator.text);
    //   callback(saveNo, true);
    // };
    // this.surveyCreatorModel = creator;

    const survey = new Model(defaultJson);
    // You can delete the line below if you do not use a customized theme
    survey.setDataCore(jsonDataFake.data);
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
                return { file: fileContent.file, content: fileContent.content };
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
  }
}
