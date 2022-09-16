import { Component, Inject } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AuthService } from '@auth0/auth0-angular';
import { DOCUMENT } from '@angular/common';

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

  constructor(private breakpointObserver: BreakpointObserver, public auth: AuthService, @Inject(DOCUMENT) private doc: Document) {}

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
