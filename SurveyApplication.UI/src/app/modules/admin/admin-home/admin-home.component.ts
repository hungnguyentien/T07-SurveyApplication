import { Component, OnInit } from '@angular/core';
import { Model } from 'survey-core';
// import { VisualizationPanel } from 'survey-analytics';
// import "survey-analytics/survey.analytics.css";
import { json } from './json';
import { dataUrl, tabsInfo } from './settings';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent implements OnInit {
  cities: any[] = [
    { name: 'New York' },
    { name: 'Los Angeles' },
    { name: 'Chicago' },
  ];
  selectedCity: any; // Biến để lưu trữ thành phố được chọn
  isLoaded!: boolean;
  tabIndex!: number;
  tabs: Array<any> = tabsInfo;
  ngOnInit() {
    this.isLoaded = false;
    this.tabIndex = 0;
    const survey = new Model(json);
    fetch(dataUrl)
      .then((response) => response.json())
      .then((data) => {
        const dataFromServer = data.Data;
        const allQuestions = survey.getAllQuestions();
        for (var i = 0; i < tabsInfo.length; i++) {
          const tab: any = tabsInfo[i];
          const questions = allQuestions.filter(
            (question) => tab.questions.indexOf(question.name) > -1
          );
          // tab.vizPanel = new VisualizationPanel(questions, dataFromServer);
        }

        this.isLoaded = true;
        this.renderContainer(0);
      });
  }

  renderContainer(index: number) {
    this.tabIndex = index;
    const tab: any = tabsInfo[index];
    const el: any = document.getElementById('surveyDashboardContainer');
    el.innerHTML = '';
    tab.vizPanel.render(el);
  }
}
