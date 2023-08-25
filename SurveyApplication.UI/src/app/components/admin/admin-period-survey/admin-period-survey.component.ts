import { Component } from '@angular/core';
import { ConfirmEventType, ConfirmationService } from 'primeng/api';
import { ServiceService } from '@app/services/service.service';
import { Customer, Representative } from '@app/models/customer';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-admin-period-survey',
  templateUrl: './admin-period-survey.component.html',
  styleUrls: ['./admin-period-survey.component.css']
})
export class AdminPeriodSurveyComponent {
  public value: any
  customers!: Customer[];

  selectedCustomers!: Customer[];

  representatives!: Representative[];

  statuses!: any[];

  loading: boolean = true;

  activityValues: number[] = [0, 100];


  constructor(private customerService: ServiceService,private messageService: MessageService,private confirmationService: ConfirmationService) {}
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
