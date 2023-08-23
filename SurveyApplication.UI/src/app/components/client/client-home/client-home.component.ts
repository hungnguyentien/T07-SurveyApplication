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
  completedHtml: '<h3>Cảm ơn phản hồi của bạn</h3>',
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
          name: 'Sử dụng mã GS1 (ví dụ GTIN, GLN)',
          title: '1. Sử dụng mã GS1 (ví dụ GTIN, GLN) (Chọn ô thích hợp):',
          defaultValue: 'false',
          labelTrue: 'Có',
          labelFalse: 'Không',
        },
        {
          type: 'checkbox',
          name: 'Loại hình của đơn vị',
          title: '2. Loại hình của đơn vị',
          choices: [
            {
              value: 'Doanh nghiệp nhà nước',
              text: 'a. Doanh nghiệp nhà nước',
            },
            {
              value: 'Doanh nghiệp tư nhân',
              text: 'b. Doanh nghiệp tư nhân',
            },
            {
              value: 'Hợp tác xã',
              text: 'c. Hợp tác xã',
            },
            {
              value: 'Cổ phần',
              text: 'd. Cổ phần',
            },
            {
              value: 'Trách nhiệm hữu hạn',
              text: 'e. Trách nhiệm hữu hạn',
            },
            {
              value: 'Có vốn đầu tư nước ngoài',
              text: 'f. Có vốn đầu tư nước ngoài',
            },
            {
              value: 'Công ty liên doanh',
              text: 'g. Công ty liên doanh',
            },
            {
              value: 'Hộ kinh doanh cá thể',
              text: 'h. Hộ kinh doanh cá thể',
            },
            {
              value: 'Công ty hợp danh',
              text: 'i. Công ty hợp danh',
            },
          ],
          showOtherItem: true,
          otherPlaceholder: 'Câu trả lời của bạn',
          noneText: 'h. Hộ kinh doanh cá thể',
          otherText: 'j. Khác',
          selectAllText: 'a. Doanh nghiệp nhà nước',
        },
        {
          type: 'comment',
          name: 'Liệt kê các sản phẩm đơn vị đang sản xuất',
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
    creator.text =
      window.localStorage.getItem('survey-json') || JSON.stringify(defaultJson);

    // creator.saveSurveyFunc = (saveNo: number, callback: Function) => {
    //   window.localStorage.setItem("survey-json", creator.text);
    //   callback(saveNo, true);
    // };
    // this.surveyCreatorModel = creator;

    const survey = new Model(defaultJson);
    // You can delete the line below if you do not use a customized theme
    survey.applyTheme(themeJson);
    survey.addNavigationItem({
      id: 'sv-nav-clear-page',
      title: 'Clear Page',
      action: () => {
        survey.currentPage.questions.forEach((question: any) => {
          question.value = undefined;
        });
      },
      css: 'nav-button',
      innerCss: 'sd-btn nav-input',
    });
    survey.onComplete.add((sender, options) => {
      debugger;
      console.log(JSON.stringify(sender.data, null, 3));
    });
    this.model = survey;
  }
}
