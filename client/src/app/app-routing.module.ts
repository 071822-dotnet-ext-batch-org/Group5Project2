import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';
import { ProductListComponent } from './components/product-list/product-list.component';
import { MyOrdersComponent } from './components/my-orders/my-orders.component';
import { ProfileComponent } from './components/profile/profile.component';
import { CheckoutComponent } from './components/checkout/checkout.component';

const routes: Routes = [
  {path: '', component:ProductListComponent},
  {path: 'checkout', component: CheckoutComponent, canActivate: [AuthGuard]},
  {path: 'my-profile', component: ProfileComponent, canActivate: [AuthGuard]},
  {path: 'my-orders', component: MyOrdersComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
