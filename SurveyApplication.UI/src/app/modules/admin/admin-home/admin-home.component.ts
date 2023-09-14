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
  constructor() {}
  ngOnInit() {}

  ngAfterViewInit(): void {}
}
