import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../../core/services/customer.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { UpdateCustomerModel } from '../../../core/dataContracts/updateCustomerModel';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-customer-update',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterModule, MatFormFieldModule, MatInputModule, MatButtonModule ],
  providers: [CustomerService],
  templateUrl: './customer-update.component.html',
  styleUrls: ['./customer-update.component.scss']
})
export class CustomerUpdateComponent {
  public customerForm: FormGroup | undefined;
  private customerId!: number;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        const numericId = Number(id);
        this.customerId = numericId;
        
        this.customerService.getById(numericId).subscribe(customer => {
          this.customerForm = this.fb.group({
            name: [customer.name, Validators.required],
            email: [customer.email, [Validators.required, Validators.email]],
            phone: [customer.phone, Validators.required],
            address: [customer.address, Validators.required],
            postalCode: [customer.postalCode, Validators.required],
            city: [customer.city, Validators.required],
            country: [customer.country, Validators.required],
            
          });
        });
      }
    });
  }

  updateCustomer() {
    if (this.customerForm!.valid) {
      const customer: UpdateCustomerModel = this.customerForm!.value;
      customer.id = this.customerId;
      this.customerService.update(customer).subscribe({
        next: (_) => {
          this.router.navigate(['/platform/customers']);
        },
        error: (err) => {
          console.error('Customer update failed:', err);
        }
      });
    }
  }
}