import { Routes } from '@angular/router';
import { ProductComponent } from './platform/products/products.component';
import { ProductCreateComponent } from './platform/products/product-create/product-create.component';
import { ProductViewComponent } from './platform/products/product-view/product-view.component';
import { ProductUpdateComponent } from './platform/products/product-update/product-update.component';
import { LoginComponent } from './authentication/login/login.component';
import { AuthGuard } from './core/authGaurd';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: 'platform',
        canActivate: [AuthGuard],
        children: [
            {
                path: 'products', children: [
                    { path: '', component: ProductComponent },
                    { path: 'create', component: ProductCreateComponent },
                    { path: 'edit/:id', component: ProductUpdateComponent },
                    { path: ':id', component: ProductViewComponent }
                ]
            }
        ]
    }
];
