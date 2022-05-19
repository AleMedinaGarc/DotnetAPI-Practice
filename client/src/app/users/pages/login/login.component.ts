import { Component } from '@angular/core';
import {
  GoogleLoginProvider,
  SocialAuthService
} from '@abacritt/angularx-social-login';
import { UserService } from '../../users.service';
import { MenuItem } from 'primeng/api';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  user: any;

  constructor(
    private socialAuthService: SocialAuthService,
    private userService: UserService
  ) {}
  
  loginWithGoogle(): void {
    this.socialAuthService
      .signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(() => {
        this.socialAuthService.authState.subscribe((user) => {
          console.log(user)
          this.user = user;
          localStorage.setItem('loggedUser', JSON.stringify(user))
        });
        this.userService.postUser(this.user);
      });
  }
}
