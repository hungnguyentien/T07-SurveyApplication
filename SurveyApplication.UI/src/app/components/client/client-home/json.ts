export const jsonDataFake = {
  config: {
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
            name: 'question1',
            title: '1. Sử dụng mã GS1 (ví dụ GTIN, GLN) (Chọn ô thích hợp):',
            defaultValue: 'false',
            labelTrue: 'Có',
            labelFalse: 'Không',
          },
          {
            type: 'checkbox',
            name: 'question2',
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
            storeDataAsText: false,
            allowMultiple: true,
            maxSize: 102400,
            name: 'question7',
            title: '7. [...]',
          },
        ],
        title: 'I. Thông tin sử dụng Mã số mã vạch của đơn vị',
      },
    ],
    showQuestionNumbers: 'off',
  },
  data: {
    question1: true,
    question2: [
      'Doanh nghiệp nhà nước',
      'Doanh nghiệp tư nhân',
      'Hợp tác xã',
      'other',
    ],
    question3: 'Trí Nam',
    question4: {
      'Row 1': {
        'Column 1': ['1'],
        'Column 2': ['1'],
      },
      'Row 2': {
        'Column 2': ['1'],
        'Column 3': ['1'],
      },
    },
    question5: {
      'Row 1': 'Column 1',
      'Row 2': 'Column 2',
    },
    question6: {
      'Row 1': {
        'Column 1': 'Câu trả lời 1',
        'Column 2': 'Câu trả lời 2',
      },
      'Row 2': {
        'Column 2': 'Câu trả lời 2',
        'Column 3': 'Câu trả lời 3',
      },
    },
    question7: [
      {
        name: 'scene.json',
        type: 'application/json',
        content:
          'data:application/json;base64,ew0KCSJjYW1lcmEiIDogDQoJew0KCQkiY2VudGVyIiA6ICI0LjA0MSAxLjQwNiA0LjMzMSIsDQoJCSJleWUiIDogIjQuNjQwIDEuNTIwIDUuMTI0IiwNCgkJInBhdGhzIiA6IFsgInNjcmlwdHMvY2FtZXJhXzAwLmpzb24iIF0sDQoJCSJ1cCIgOiAiMC4wMDAgMS4wMDAgMC4wMDAiDQoJfSwNCgkiZ2VuZXJhbCIgOiANCgl7DQoJCSJhbWJpZW50Y29sb3IiIDogIjAuMzAwIDAuMzAwIDAuMzAwIiwNCgkJImJsb29tIiA6IHRydWUsDQoJCSJub3JlY29tcGlsZSIgOiB0cnVlLA0KCQkiY2FtZXJhZmFkZSIgOiBmYWxzZSwNCgkJImNsZWFyY29sb3IiIDogIjAuNzAwIDAuNzAwIDAuNzAwIiwNCgkJInNreWxpZ2h0Y29sb3IiIDogIjAuMzAwIDAuMzAwIDAuMzAwIg0KCX0sDQoJIm9iamVjdHMiIDogDQoJWw0KCQl7DQoJCQkiYW5nbGVzIiA6ICIwLjAwMCAwLjAwMCAwLjAwMCIsDQoJCQkiY29sb3IiIDogIjEuMDAwIDEuMDAwIDEuMDAwIiwNCgkJCSJpZCIgOiA1LA0KCQkJImludGVuc2l0eSIgOiAwLjY3MDAwMDAxNjY4OTMwMDU0LA0KCQkJImxpZ2h0IiA6ICJwb2ludCIsDQoJCQkibmFtZSIgOiAiIiwNCgkJCSJvcmlnaW4iIDogIjMuMTA2IDIuNjkyIC0wLjIwNCIsDQoJCQkicmFkaXVzIiA6IDE3LjEyOTk5OTE2MDc2NjYwMiwNCgkJCSJzY2FsZSIgOiAiMS4wMDAgMS4wMDAgMS4wMDAiDQoJCX0sDQoJCXsNCgkJCSJhbmdsZXMiIDogIjAuMDAwIDAuMDAwIDAuMDAwIiwNCgkJCSJpZCIgOiA2LA0KCQkJIm1vZGVsIiA6ICJtb2RlbHMvY29yZS9jb3JlLm1kbCIsDQoJCQkibmFtZSIgOiAiY29yZSIsDQoJCQkib3JpZ2luIiA6ICIwLjAwMCAwLjAwMCAwLjAwMCIsDQoJCQkic2NhbGUiIDogIjEuMDAwIDEuMDAwIDEuMDAwIg0KCQl9LA0KCQl7DQoJCQkiYW5nbGVzIiA6ICIwLjAwMCAwLjAwMCAwLjAwMCIsDQoJCQkiaWQiIDogNywNCgkJCSJtb2RlbCIgOiAibW9kZWxzL2JhY2tncm91bmRzcGhlcmUvYmFja2dyb3VuZHNwaGVyZS5tZGwiLA0KCQkJIm5hbWUiIDogImJhY2tncm91bmRzcGhlcmUiLA0KCQkJIm9yaWdpbiIgOiAiMC4wMDAgMC4wMDAgMC4wMDAiLA0KCQkJInNjYWxlIiA6ICIxLjAwMCAxLjAwMCAxLjAwMCINCgkJfSwNCgkJew0KCQkJImFuZ2xlcyIgOiAiMC4wMDAgMC4wMDAgMC4wMDAiLA0KCQkJImlkIiA6IDgsDQoJCQkibmFtZSIgOiAiIiwNCgkJCSJvcmlnaW4iIDogIjAuMDAwIDAuMDAwIDAuMDAwIiwNCgkJCSJwYXJ0aWNsZSIgOiAicGFydGljbGVzL3BhcnRpY2xlcy5qc29uIiwNCgkJCSJzY2FsZSIgOiAiMS4wMDAgMS4wMDAgMS4wMDAiDQoJCX0NCgldDQp9',
      },
      {
        name: 'project.json',
        type: 'application/json',
        content:
          'data:application/json;base64,ew0KCSJhdXRob3JzdGVhbWlkIiA6ICI3NjU2MTE5Nzk3NTYxOTYxOSIsDQoJImZpbGUiIDogInNjZW5lLmpzb24iLA0KCSJvZmZpY2lhbCIgOiB0cnVlLA0KCSJnZW5lcmFsIiA6IA0KCXsNCgkJInByb3BlcnRpZXMiIDogDQoJCXsNCgkJCSJiZ2NvbG9yIiA6IA0KCQkJew0KCQkJCSJ0ZXh0IiA6ICJ1aV9icm93c2VfcHJvcGVydGllc19iYWNrZ3JvdW5kX2NvbG9yIiwNCgkJCQkidHlwZSIgOiAiY29sb3IiLA0KCQkJCSJ2YWx1ZSIgOiAiMSAwLjY0NzA1ODgyMzUyOTQxMTggMCINCgkJCX0sDQoJCQkic2NoZW1lY29sb3IiIDogDQoJCQl7DQoJCQkJInRleHQiIDogInVpX2Jyb3dzZV9wcm9wZXJ0aWVzX2FjY2VudF9jb2xvciIsDQoJCQkJInR5cGUiIDogImNvbG9yIiwNCgkJCQkidmFsdWUiIDogIjEgMCAwIg0KCQkJfQ0KCQl9LA0KCQkic3VwcG9ydHNhdWRpb3Byb2Nlc3NpbmciIDogdHJ1ZQ0KCX0sDQoJInByZXZpZXciIDogInByZXZpZXcuanBnIiwNCgkidGl0bGUiIDogIkRlbW9uIENvcmUiLA0KCSJ0eXBlIiA6ICJzY2VuZSIsDQoJInZpc2liaWxpdHkiIDogInB1YmxpYyINCn0=',
      },
    ],
    'question2-Comment': 'Test',
  },
};
