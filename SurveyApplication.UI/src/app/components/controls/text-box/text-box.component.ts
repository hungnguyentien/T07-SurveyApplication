import { NgClass } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormControl, FormControlName, NgForm } from '@angular/forms';

@Component({
  selector: 'text-box',
  templateUrl: './text-box.component.html',
  styleUrls: ['./text-box.component.css'],
})
export class TextBoxComponent {
  @Input() name: string = '';
  @Input() displayName: string = '';
  @Input() placeholder: string = '';
  @Input('ngClass') ngClass: NgClass['ngClass'];
  @Input() fControl!: FormControl;
  @Input() submitted: boolean = false;
}
