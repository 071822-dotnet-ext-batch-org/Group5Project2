import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { GetUserInfoService } from 'src/app/Services/get-user-info/get-user-info.service';
import { UpdateUserInfoService } from 'src/app/Services/update-user-info/update-user-info.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(
    private UIS: GetUserInfoService,
    private UUI: UpdateUserInfoService
  ) { }

  userInfo: any;
  showAddress: boolean = true;
  showPhone: boolean = true;
  address = new FormControl('');
  phone = new FormControl('');


  ngOnInit(): void {
    this.UIS.getUserInfo().subscribe(data => {
      this.userInfo = data;
      this.address.setValue(data.profileAddress);
      this.phone.setValue(data.profilePhone);
    });
  }

  editAddress(): void {
    this.showAddress = false;
  }

  editPhone(): void {
    this.showPhone = false;
  }

  updateAddressInfo(): void {
    this.showAddress = true;
    this.UUI.updateUserInfo(null, this.address.value).subscribe(data=> {
      this.address.setValue(data.profileAddress)
      this.userInfo = data;
    });
  }

  updatePhoneInfo(): void {
    this.showPhone = true;
    this.UUI.updateUserInfo(this.phone.value, null).subscribe(data=> {
      this.phone.setValue(data.profilePhone)
      this.userInfo = data;
    });
  }
}
