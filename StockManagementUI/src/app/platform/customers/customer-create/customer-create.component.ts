import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../../core/services/customer.service';
import { Router, RouterModule } from '@angular/router';
import { CreateCustomerModel } from '../../../core/dataContracts/createCustomerModel';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-customer-create',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterModule, MatFormFieldModule, MatInputModule, MatButtonModule ],
  providers: [CustomerService],
  templateUrl: './customer-create.component.html',
  styleUrls: ['./customer-create.component.scss']
})
export class CustomerCreateComponent {
  customerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private router: Router
  ) {
    this.customerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      postalCode: ['', Validators.required]
    });
  }

  createCustomer() {
    if (this.customerForm.valid) {
      const customer: CreateCustomerModel = this.customerForm.value;
      this.customerService.create(customer).subscribe({
        next: (_) => {
          this.router.navigate(['/platform/customers']);
        },
        error: (err) => {
          console.error('Customer creation failed:', err);
        }
      });
    }
  }
}