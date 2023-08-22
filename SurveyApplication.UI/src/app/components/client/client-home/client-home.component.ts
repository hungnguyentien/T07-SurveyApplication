import { Component, OnInit } from '@angular/core';
import { SurveyCreatorModel } from 'survey-creator-core';
import { Model } from 'survey-core';
import { themeJson } from './theme';
import { surveyLocalization } from 'survey-core';

const creatorOptions = {
  showLogicTab: true,
  isAutoSave: true,
};

const defaultJson = {
  title: 'PHẦN 2: THÔNG TIN KHẢO SÁT',
  completedHtml: '<h3>Thank you for your feedback</h3>',
  completedHtmlOnCondition: [
    {
      html: '<h3>Thank you for your feedback</h3> <h4>We are glad that you love our product. Your ideas and suggestions will help us make it even better.</h4>',
    },
    {
      html: '<h3>Thank you for your feedback</h3> <h4>We are glad that you shared your ideas with us. They will help us make our product better.</h4>',
    },
  ],
  pages: [
    {
      name: 'page1',
      elements: [
        {
          type: 'boolean',
          name: 'question1',
          title:
            '1. Sử dụng mã GS1 (ví dụ GTIN, GLN) (Đánh dấu “X” vào ô thích hợp):',
          labelTrue: 'Có',
          labelFalse: 'Không',
        },
        {
          type: 'checkbox',
          name: 'question2',
          title: '2. Loại hình của đơn vị',
          choices: [
            {
              value: 'Item 1',
              text: 'b. Doanh nghiệp tư nhân',
            },
            {
              value: 'Item 2',
              text: 'c. Hợp tác xã',
            },
            {
              value: 'Item 3',
              text: 'e. Trách nhiệm hữu hạn',
            },
            {
              value: 'Item 4',
              text: 'f. Có vốn đầu tư nước ngoài',
            },
            {
              value: 'Item 5',
              text: 'g. Công ty liên doanh',
            },
            {
              value: 'Item 6',
              text: 'h. Hộ kinh doanh cá thể',
            },
            {
              value: 'Item 7',
              text: 'i. Công ty hợp danh',
            },
          ],
          showOtherItem: true,
          noneText: 'h. Hộ kinh doanh cá thể',
          otherText: 'j. Khác',
          showSelectAllItem: true,
          selectAllText: 'a. Doanh nghiệp nhà nước',
        },
        {
          type: 'comment',
          name: 'question3',
          title: '3. Liệt kê các sản phẩm đơn vị đang sản xuất',
        },
        {
          type: 'matrixdropdown',
          name: 'question4',
          title: '4. [...]',
          alternateRows: true,
          columns: [
            {
              name: 'Column 1',
              title: 'Câu trả lời 1',
            },
            {
              name: 'Column 2',
              title: 'Câu trả lời 2',
            },
            {
              name: 'Column 3',
              title: 'Câu trả lời 3',
            },
          ],
          choices: [
            {
              value: '1',
              text: 'Có',
            },
          ],
          cellType: 'checkbox',
          columnColCount: 1,
          rows: [
            {
              value: 'Row 1',
              text: 'Câu hỏi phụ 1',
            },
            {
              value: 'Row 2',
              text: 'Câu hỏi phụ 2',
            },
          ],
        },
        {
          type: 'matrix',
          name: 'question5',
          title: '5. [...]',
          alternateRows: true,
          columns: [
            {
              value: 'Column 1',
              text: 'Câu trả lời 1',
            },
            {
              value: 'Column 2',
              text: 'Câu trả lời 2',
            },
            {
              value: 'Column 3',
              text: 'Câu trả lời 3',
            },
          ],
          rows: [
            {
              value: 'Row 1',
              text: 'Câu hỏi phụ 1',
            },
            {
              value: 'Row 2',
              text: 'Câu hỏi phụ 2',
            },
          ],
        },
        {
          type: 'matrixdropdown',
          name: 'question6',
          title: '6. [...]',
          alternateRows: true,
          columns: [
            {
              name: 'Column 1',
              title: 'Câu trả lời 1',
            },
            {
              name: 'Column 2',
              title: 'Câu trả lời 2',
            },
            {
              name: 'Column 3',
              title: 'Câu trả lời 3',
            },
          ],
          cellType: 'text',
          rows: [
            {
              value: 'Row 1',
              text: 'Câu hỏi phụ 1',
            },
            {
              value: 'Row 2',
              text: 'Câu hỏi phụ 2',
            },
          ],
        },
        {
          type: 'file',
          name: 'question7',
          title: '7. [...]',
        },
      ],
      title: 'I. Thông tin sử dụng Mã số mã vạch của đơn vị',
    },
  ],
  showQuestionNumbers: 'off',
};

@Component({
  selector: 'app-client-home',
  templateUrl: './client-home.component.html',
  styleUrls: ['./client-home.component.css'],
})
export class ClientHomeComponent implements OnInit {
  surveyCreatorModel!: SurveyCreatorModel;
  model!: Model;
  ngOnInit() {
    const creator = new SurveyCreatorModel(creatorOptions);
    creator.text = window.localStorage.getItem("survey-json") || JSON.stringify(defaultJson);

    creator.saveSurveyFunc = (saveNo: number, callback: Function) => {
      window.localStorage.setItem("survey-json", creator.text);
      callback(saveNo, true);
    };
    this.surveyCreatorModel = creator;

  //   const survey = new Model(defaultJson);
  //   // You can delete the line below if you do not use a customized theme
  //   survey.applyTheme(themeJson);
  //   survey.addNavigationItem({
  //     id: "sv-nav-clear-page",
  //     title: "Clear Page",
  //     action: () => {
  //         survey.currentPage.questions.forEach((question:any) => {
  //             question.value = undefined;
  //         });
  //     },
  //     css: "nav-button",
  //     innerCss: "sd-btn nav-input"
  // });
  //   survey.onComplete.add((sender, options) => {
  //     debugger;
  //     console.log(JSON.stringify(sender.data, null, 3));
  //   });
  //   this.model = survey;
  }
}
