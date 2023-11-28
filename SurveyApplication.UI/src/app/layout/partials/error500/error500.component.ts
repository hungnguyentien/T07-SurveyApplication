import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import Utils from '@app/helpers/utils';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-error500',
  templateUrl: './error500.component.html',
  styleUrls: ['./error500.component.css'],
})
export class Error500Component {
  message: string = '';
  constructor(
    private activatedRoute: ActivatedRoute,
    private messageService: MessageService,
    private titleService: Title
  ) {}
  ngOnInit() {
    this.message =
      this.activatedRoute.snapshot.queryParamMap.get('message') ?? '';
    this.titleService.setTitle(this.message);
    Utils.messageInfo(this.messageService, this.message);
  }
}
