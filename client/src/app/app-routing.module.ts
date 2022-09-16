import { RegistrationComponent } from './registration/registration.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';



import { AuthGuard } from '@auth0/auth0-angular';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HomeComponent } from './components/home/home.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'my-orders', component: DashboardComponent, canActivate: [AuthGuard]},
  {path: 'my-cart', component: HomeComponent, canActivate: [AuthGuard]},
  {path: 'my-profile', component: HomeComponent, canActivate: [AuthGuard]},
  {path: '', component:ProductListComponent},
  {path: 'registration', component: RegistrationComponent},
  {path: 'my-orders', component: MyOrdersComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
