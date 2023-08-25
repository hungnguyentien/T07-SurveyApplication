import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@Component({
  selector: 'app-choose-an-answer',
  templateUrl: './choose-an-answer.component.html',
  styleUrls: ['./choose-an-answer.component.css']
})
export class ChooseAnAnswerComponent {
  Editor = ClassicEditor; // Tham chiếu đến ClassicEditor
  @Input() valueEditor: string = ''; // Khai báo biến lưu nội dung CKEditor
  @Input() inputValue: string = '';
  @Input() listAnswer:string[] =  [];
  @Output() inputValueChange: EventEmitter<string> = new EventEmitter<string>();
  
  onInputChange() {
    this.inputValueChange.emit(this.inputValue);
    this.inputValueChange.emit(this.valueEditor)
    console.log(this.inputValue)
  }
  dynamicForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.dynamicForm = this.formBuilder.group({
      items: this.formBuilder.array([])
    });
  }

  get items(): FormArray {
    return this.dynamicForm.get('items') as FormArray;
  }

  addItem() {
    const newItem = this.formBuilder.group({
      name: '',
      age: ''
    });
    this.items.push(newItem);
    console.log(newItem)
  }

  deleteItem(index: number) {
    this.items.removeAt(index);
  }
  
  // Config ckeditor 5
  EditorConfig = {
    toolbar: {
      items: [
        'heading',
        '|',
        'bold',
        'italic',
        'link',
        'bulletedList',
        'numberedList',
        '|',
        'alignment',
        'indent',
        'outdent',
        '|',
        'blockQuote',
        'insertTable',
        'undo',
        'redo'
      ]
    },
    heading: {
      options: [
        { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
        { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
        { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' }
      ]
    },
    table: {
      contentToolbar: [
        'tableColumn',
        'tableRow',
        'mergeTableCells'
      ]
    },
    language: 'en'
  };
  
}
