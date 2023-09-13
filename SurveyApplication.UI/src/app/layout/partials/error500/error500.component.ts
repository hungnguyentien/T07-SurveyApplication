import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import Utils from '@app/helpers/utils';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-error500',
  templateUrl: './error500.component.html',
  styleUrls: ['./error500.component.css'],
})
export class Error500Component {
  constructor(
    private activatedRoute: ActivatedRoute,
    private messageService: MessageService
  ) {}
  ngOnInit() {
    let data = this.activatedRoute.snapshot.queryParamMap.get('message') ?? '';
    Utils.messageError(this.messageService, data);
  }
}
