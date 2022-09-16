import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { ProductListComponent } from './product-list/product-list.component';
import { MatButtonModule} from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RegistrationComponent } from './registration/registration.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductListComponent,
    RegistrationComponent,
    MyOrdersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatMenuModule,
    MatCardModule,
    MatIconModule,
    MatSelectModule,
    MatSliderModule,
    FormsModule,
    ReactiveFormsModule,
    MatToolbarModule,
    RouterModule.forRoot([
      
      {path: '', component:ProductListComponent},
      {path: 'registration', component: RegistrationComponent},
      {path: 'my-orders', component: MyOrdersComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
