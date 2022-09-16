import { Component, OnInit } from '@angular/core';
import { Register } from '../Models/Register';
import { EcommerceAPIService } from '../Services/ecommerce-api.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  userregister: any;
  data: any;
  

  constructor(private EcommerceAPI: EcommerceAPIService) { }

  ngOnInit(): void {
  }

  RegisterUsers(data : Register){
    this.EcommerceAPI.postRegistration(data).subscribe(res => {
      console.log(res);
    })
  }

}
