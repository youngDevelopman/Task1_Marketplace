import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PageVisitTrackerService {
  trackPageVisit(pageName: string) {
    const cookieValue = this.getCookieValue("isTrackingAvailable");
    if (cookieValue == 'allow') {
      const pageVisitKey = `page_visit_count_${pageName}`;
      let visitCount = 0;

      if (this.cookieExists(pageVisitKey)) {
        visitCount = this.parseStringToNumber(this.getCookieValue(pageVisitKey));
      }

      visitCount++;
      document.cookie = pageVisitKey + "=" + (visitCount || "") + "; path=/";
    }
  }

  parseStringToNumber(input: string | null) {
    if (input === null) {
      return 0;
    }

    let number = parseInt(input); 
    if (isNaN(number)) {
      return 0; 
    }

    return number;
  }


  cookieExists(name: string) {
    let cookies = document.cookie.split('; ');
    for (let i = 0; i < cookies.length; i++) {
      let cookie = cookies[i].split('=');
      if (cookie[0] === name) {
        return true;
      }
    }
    return false;
  }
  getCookieValue(name: string) {
    let cookies = document.cookie.split('; ');
    for (let i = 0; i < cookies.length; i++) {
      let cookie = cookies[i].split('=');
      if (cookie[0] === name) {
        return cookie[1];
      }
    }
    return null;
  }
}
