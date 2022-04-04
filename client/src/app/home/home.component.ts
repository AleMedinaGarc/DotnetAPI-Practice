import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {SocialAuthService} from 'angularx-social-login';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [],
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  response: Object | undefined;
  
  constructor(
    private http: HttpClient,
    private router: Router,
    public socialAuthService: SocialAuthService
  ) {}

  ngOnInit() {
    let obs = this.http.get('http://localhost:5000/api/Cars/allCars');
    try {
      obs.subscribe((response) => {
        this.response = response;
        console.log(response);
      });
    } catch (e) {
      console.log(e);
    }

  }

  printContent(test: any){
    console.log(test)
  }

  logout(): void {
    this.socialAuthService
      .signOut()
      .then(() => this.router.navigate(['login']));
  }
}
