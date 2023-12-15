import { Component } from "@angular/core";
import { UserClaim } from "../../models/UserClaim";
import { AuthService } from "../../services/AuthService";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
})
export class UserComponent {
  userClaims: UserClaim[] = [];
  constructor(private authService: AuthService) {
    this.getUser();
  }

  getUser() {
    this.authService.user().subscribe(
      result => {
        this.userClaims = result;
      });
  }
}
