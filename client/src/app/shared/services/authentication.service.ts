import { Injectable } from '@angular/core';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})

export class AuthenticationService implements CanActivate {
  constructor(
    private router: Router,
    private socialAuthService: SocialAuthService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.socialAuthService.authState.pipe(
      map((socialUser: SocialUser) => !!socialUser),
      tap((isLoggedIn: boolean) => {
        if (!isLoggedIn || !this.isAuthenticated()) 
          this.router.navigate(['login']);
      })
    );
  }

  isAuthenticated():boolean {
    const token = localStorage.getItem('jwt');
    if (token){
      var expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
      return (Math.floor((new Date).getTime() / 1000)) <= expiry;
    }
    return false;
  }
}
