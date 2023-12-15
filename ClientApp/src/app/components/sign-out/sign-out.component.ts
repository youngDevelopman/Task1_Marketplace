import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/AuthService';

@Component({
  selector: 'app-sign-out',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sign-out.component.html',
  styleUrls: ['./sign-out.component.css']
})
export class SignOutComponent {
  constructor(private authService: AuthService) {
    this.signout();
  }
  public signout() {
    this.authService.signOut().subscribe();
  }
}
