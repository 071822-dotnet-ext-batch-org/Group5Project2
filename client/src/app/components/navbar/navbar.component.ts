import { Component, Inject } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable, Subscription } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AuthService } from '@auth0/auth0-angular';
import { DOCUMENT } from '@angular/common';
import { GetUserInfoService } from 'src/app/Services/get-user-info/get-user-info.service';
import { DataSharingService } from 'src/app/Services/data-sharing/data-sharing.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
  
  myName: any;
  cartItems: number = 0;

  ngOnInit(): void {
    this.auth.isAuthenticated$.subscribe(authenticated => {
      if(authenticated) {
        this.UIS.getUserInfo().subscribe(data => {
          this.myName = data.profileName.split(' ').shift();
          this.cartItems = data.cart.cartItems;
        });
      }
    });
  }

  constructor(
    private breakpointObserver: BreakpointObserver, 
    public auth: AuthService, 
    @Inject(DOCUMENT) private doc: Document, 
    private UIS: GetUserInfoService,
    private DSS: DataSharingService
  ) {
    this.DSS.getUpdatedCart().subscribe(items => {
      console.log(items)
      this.cartItems=items
    })
  }

  login():void {
    this.auth.loginWithRedirect();
  }

  logout(): void {
    this.auth.logout({
      returnTo: this.doc.location.origin
    });
  }

  signup(): void {
    this.auth.loginWithRedirect({
      screen_hint: 'signup'
    });
  }

}
