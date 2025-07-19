import { Routes } from '@angular/router';
import { ProductComponent } from './platform/products/products.component';
import { ProductCreateComponent } from './platform/products/product-create/product-create.component';
import { ProductViewComponent } from './platform/products/product-view/product-view.component';
import { ProductUpdateComponent } from './platform/products/product-update/product-update.component';
import { LoginComponent } from './authentication/login/login.component';
import { AuthGuard } from './core/authGuard';
import { PlatformComponent } from './platform/platform.component';
import { DashboardComponent } from './platform/dashboard/dashboard.component';
import { RegisterComponent } from './authentication/register/register.component';
import { PersonalInfoComponent } from './platform/personal-info/personal-info.component';
import { CustomerComponent } from './platform/customers/customers.component';
import { CustomerCreateComponent } from './platform/customers/customer-create/customer-create.component';
import { CustomerViewComponent } from './platform/customers/customer-view/customer-view.component';
import { CustomerUpdateComponent } from './platform/customers/customer-update/customer-update.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    {
        path: 'platform',
        canActivate: [AuthGuard],
        component: PlatformComponent,
        children: [
            {
                path: 'products', children: [
                    { path: '', component: ProductComponent },
                    { path: 'create', component: ProductCreateComponent },
                    { path: 'edit/:id', component: ProductUpdateComponent },
                    { path: ':id', component: ProductViewComponent }
                ]
            },
            {
                path: 'customers', children: [
                    { path: '', component: CustomerComponent },
                    { path: 'create', component: CustomerCreateComponent },
                    { path: 'edit/:id', component: CustomerUpdateComponent },
                    { path: ':id', component: CustomerViewComponent }
                ]
            },
            { path: 'dashboard', component: DashboardComponent },
            { path: 'personal-info', component: PersonalInfoComponent }
        ]
    },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: '**', redirectTo: '/login' }, // Of een mooie 404 pagina hier toevoegen
    
];