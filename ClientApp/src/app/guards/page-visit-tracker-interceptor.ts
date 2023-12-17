import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { PageVisitTrackerService } from '../services/PageVisitrackerService';

@Injectable({
  providedIn: 'root'
})
export class PageVisitGuard implements CanActivate {

  constructor(private visitTracker: PageVisitTrackerService) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    
    const pageName = state.url;
    this.visitTracker.trackPageVisit(pageName);

    return true;
  }
}
