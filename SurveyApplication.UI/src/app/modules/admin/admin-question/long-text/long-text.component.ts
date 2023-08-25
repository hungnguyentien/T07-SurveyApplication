import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-long-text',
  templateUrl:'./long-text.component.html',
  styleUrls: ['./long-text.component.css']
})
export class LongTextComponent {
  @Input() selectedItem: string | null = null;
  formData: string = '';
}
