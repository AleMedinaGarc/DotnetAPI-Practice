import { Component } from '@angular/core';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  http: any;
  postId: any;

  constructor(
    private router: Router,
    private socialAuthService: SocialAuthService
  ) {}

  loginWithGoogle(): void {
    this.socialAuthService
      .signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(()=>this.PostData())
      .then(() => this.router.navigate(['']));
  }

  async PostData() {
    const data = await this.socialAuthService.authState
    console.log(data);
    let obs = this.http.post('http://localhost:5000/api/Login', data);
    try {
      obs.subscribe((data: { id: any; }) => {
          this.postId = data.id;
        });
    } catch (e) {
      console.log(e);
    }
    
  }
}
