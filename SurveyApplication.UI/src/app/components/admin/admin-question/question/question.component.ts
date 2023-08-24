import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { ServiceService } from 'src/app/application/services/service.service';
import { Customer, Representative } from 'src/app/core/Entities/customer';

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
  dropdownItems = ['Một đáp án', 'Chọn nhiều đáp án', 'Văn bản ngắn', 'Văn bản dài', 'Dạng bảng(một lựa chọn)',
  'Dạng bảng(nhiều lựa chọn)', 'Dạng bảng(văn bản)', 'Tải tệp tin'];
  selectedItem: string | null = null;

  constructor(private customerService: ServiceService,private messageService: MessageService,private confirmationService: ConfirmationService){}
  @ViewChild('dynamicFormContainer', { read: ViewContainerRef }) dynamicFormContainer!: ViewContainerRef;
  onItemChange(){
    
  }
  ngOnInit() {
    this.customerService.getCustomersLarge().then((customers) => {
        this.customers = customers;
        this.loading = false;
    });
    this.representatives = [
        { name: 'Amy Elsner', image: 'amyelsner.png' },
        { name: 'Anna Fali', image: 'annafali.png' },
        { name: 'Asiya Javayant', image: 'asiyajavayant.png' },
        { name: 'Bernardo Dominic', image: 'bernardodominic.png' },
        { name: 'Elwin Sharvill', image: 'elwinsharvill.png' },
        { name: 'Ioni Bowcher', image: 'ionibowcher.png' },
        { name: 'Ivan Magalhaes', image: 'ivanmagalhaes.png' },
        { name: 'Onyama Limba', image: 'onyamalimba.png' },
        { name: 'Stephen Shaw', image: 'stephenshaw.png' },
        { name: 'Xuxue Feng', image: 'xuxuefeng.png' }
    ];
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
