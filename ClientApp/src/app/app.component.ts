import { Component } from '@angular/core';
import { NgcCookieConsentService, NgcStatusChangeEvent } from 'ngx-cookieconsent';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  private isTrackingAvailableKey = 'isTrackingAvailable';

  constructor(
    private ccService: NgcCookieConsentService,
  ) {
    this.ccService.statusChange$.subscribe(
      (event: NgcStatusChangeEvent) => {
        document.cookie = this.isTrackingAvailableKey + "=" + (event.status || "") + "; path=/";
      }
    );
  }
}
