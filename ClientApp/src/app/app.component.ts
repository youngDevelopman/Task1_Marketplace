import { Component } from '@angular/core';
import { NgcCookieConsentService } from 'ngx-cookieconsent';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(private ccService: NgcCookieConsentService) {
    this.ccService.popupOpen$.subscribe(
      // Your code when cookie consent popup is opened
    );

    this.ccService.popupClose$.subscribe(
      // Your code when cookie consent popup is closed
    );
  }
}
