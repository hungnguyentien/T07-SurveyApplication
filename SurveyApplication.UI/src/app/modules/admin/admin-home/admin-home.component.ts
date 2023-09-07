import { Component, ElementRef, ViewChild } from '@angular/core';
import { VisualizationPanel } from 'survey-analytics';
import Utils from '@app/helpers/utils';
import { PhieuKhaoSatService } from '@app/services';

const surveyResults: any[] = [];
const vizPanelOptions = {
  allowHideQuestions: false,
};

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent {
  @ViewChild('surveyVizPanel') elem: ElementRef | undefined;
  constructor(private phieuKhaoSatService: PhieuKhaoSatService) {}
  ngOnInit() {}

  ngAfterViewInit(): void {
    this.phieuKhaoSatService.getSurveyConfig(`0dAdviHHkd7WnsjW2TM0vQCHq//WZ6EH`).subscribe((res) => {
      let defaultJson = Utils.getJsonSurvey(res);
      const survey = Utils.configSurvey(defaultJson, undefined, undefined);
      //Add list kết quả Survey
      surveyResults.push({
        question1: 'other',
        'question1-Comment': 'LOL',
        question2: ['Doanh nghiệp nhà nước'],
        question6: {
          'Row 1 _MultiTextMatrix': { 'columns1 _MultiTextMatrix': '1' },
        },
        question4: {
          'Row 2 _MultiSelectMatrix': { 'columns1 _MultiSelectMatrix': ['1'] },
        },
      });
      const vizPanel: any = new VisualizationPanel(
        survey.getAllQuestions(),
        surveyResults,
        vizPanelOptions
      );
      vizPanel.showHeader = false;
      vizPanel.render(this.elem?.nativeElement);
    });
  }
}
