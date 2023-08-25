import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { ServiceService } from 'src/app/services/service.service';
import { FormControl, FormGroup } from '@angular/forms';
import { Customer, Representative } from '@app/models';
@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent {
  public value: any
  customers!: Customer[];
  selectedCustomers!: Customer[];
  representatives!: Representative[];
  loading: boolean = true;
  

  constructor(private customerService: ServiceService,private messageService: MessageService,private confirmationService: ConfirmationService){}
  inputValue: string = '';
  valueEditor: string = '';
  selectedOption: string = 'motdapan';
  submitForm() {
    if (this.selectedOption === 'motdapan') {
      console.log('Input Value:', this.inputValue);
      console.log('ValueEditor:', this.valueEditor);
    }
    else if(this.selectedOption === 'nhieudapan'){
      console.log('Input Value:', this.inputValue);
    }
  }
  ngOnInit() {
    this.customerService.getCustomersLarge().then((customers) => {
        this.customers = customers;
        this.loading = false;
    });
  }
  confirm1() {
    this.confirmationService.confirm({
        message: 'Bạn có chắc chắn muốn xoá đợt khảo sát này?',
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.messageService.add({ severity: 'info', summary: 'Confirmed', detail: 'You have accepted' });
        },
        reject: (type: ConfirmEventType) => {
            switch (type) {
                case ConfirmEventType.REJECT:
                    this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
                    break;
                case ConfirmEventType.CANCEL:
                    this.messageService.add({ severity: 'warn', summary: 'Cancelled', detail: 'You have cancelled' });
                    break;
            }
        }
    });
  }
}
