import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CustomerViewModel } from '../../../core/dataContracts/customerViewModel';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../../../core/services/customer.service';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinner } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-customer-view',
  imports: [CommonModule, MatListModule, MatProgressSpinner],
  templateUrl: './customer-view.component.html',
  styleUrl: './customer-view.component.scss'
})
export class CustomerViewComponent {
  public customer?: CustomerViewModel;

  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService
  ) {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        const numericId = Number(id);
        this.customerService.getById(numericId).subscribe(customer => {
          this.customer = customer;
        });
      }
    });
  }
}
