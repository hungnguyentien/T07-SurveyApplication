import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-choose-an-answer',
  templateUrl: './choose-an-answer.component.html',
  styleUrls: ['./choose-an-answer.component.css'],
})
export class ChooseAnAnswerComponent {
  @Input() valueEditor: string = ''; // Khai báo biến lưu nội dung CKEditor
  @Input() inputValue: string = '';
  @Input() listAnswer: string[] = [];
  @Output() inputValueChange: EventEmitter<string> = new EventEmitter<string>();

  dynamicForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.dynamicForm = this.formBuilder.group({
      items: this.formBuilder.array([]),
    });
  }

  get items(): FormArray {
    return this.dynamicForm.get('items') as FormArray;
  }

  addItem() {
    const newItem = this.formBuilder.group({
      name: '',
      age: '',
    });
    this.items.push(newItem);
    console.log(newItem);
  }

  deleteItem(index: number) {
    this.items.removeAt(index);
  }
}
