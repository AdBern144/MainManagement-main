import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CustomerService } from '../../core/services/customer.service';
import { CustomerViewModel } from '../../core/dataContracts/customerViewModel';

@Component({
  selector: 'app-customers',
  imports: [CommonModule, RouterModule, MatButtonModule, MatIconModule],
  providers: [CustomerService],
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.scss'
})
export class CustomerComponent implements OnInit {
  public customers: CustomerViewModel[] = [];
  
  constructor(private _customerService: CustomerService) { }

  public ngOnInit(): void {
    this.getCustomers();
  }

  public getCustomers(): void {
    this._customerService.getAll().subscribe({
      next: (receivedCustomers) => {
        this.customers = receivedCustomers;
      },
      error: (err) => {
        console.error('Error fetching customers:', err);
      }
    });
  }

  public deleteCustomer(customerId: number): void {
    this._customerService.delete(customerId).subscribe({
      next: () => {
        this.customers = this.customers.filter(customer => customer.id !== customerId);
      },
      error: (err) => {
        console.error('Error deleting customer:', err);
      }
    });
  }
}
