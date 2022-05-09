import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Router } from '@angular/router';
import { SocialAuthService } from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html', // Manages Component's HTML
  styleUrls: ['./header.component.scss'], // Handles Component's styling
})
export class HeaderComponent implements OnInit {
  items: MenuItem[] | any;
  jwt: string | null | undefined;
  logged: boolean | undefined;

  constructor(
    private router: Router,
    public socialAuthService: SocialAuthService
  ) {}

  ngOnInit() {
    this.jwt = localStorage.getItem('jwt');
    if (this.jwt) {
      let jwtData = this.jwt.split('.')[1];
      let decodedJwtJsonData = window.atob(jwtData);
      let decodedJwtData = JSON.parse(decodedJwtJsonData);

      Object.entries(decodedJwtData).forEach(([key, value]) => {
        if (value == 'Employee' && this.isAuthenticated())
          this.items = this.employeeItems();
        if (value == 'Administrator' && this.isAuthenticated())
          this.items = this.adminItems();
      });
    } else this.items = this.unlogedItems();
  }

  logout(): void {
    this.socialAuthService
      .signOut()
      .then(() => localStorage.removeItem('jwt'))
      .then(() => this.router.navigate(['login']));
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('jwt');
    if (token) {
      var expiry = JSON.parse(atob(token.split('.')[1])).exp;
      return Math.floor(new Date().getTime() / 1000) <= expiry;
    }
    return false;
  }

  employeeItems() {
    this.logged = true;
    var items = [
      {
        label: 'Home(Employee)',
        icon: 'pi pi-fw pi-home',
        routerLink: [''],
      },
      {
        label: 'Car list',
        icon: 'pi pi-fw pi-car',
        routerLink: ['/allCars'],
      },
      {
        label: 'Reservations',
        icon: 'pi pi-fw pi-calendar',
        items: [
          {
            label: 'My Reservations',
            icon: 'pi pi-fw pi-user',
            routerLink: ['/userReservations'],
          },
          {
            label: 'All Reservations',
            icon: 'pi pi-fw pi-user-edit',
            routerLink: ['/allReservations'],
          },
        ],
      },
      {
        label: 'Profile',
        icon: 'pi pi-fw pi-user',
        routerLink: ['/profile'],
      },
      {
        label: 'Users',
        icon: 'pi pi-fw pi-user',
        routerLink: ['/allUsers'],
      },
    ];
    return items;
  }

  adminItems() {
    this.logged = true;
    var items = [
      {
        label: 'Home',
        icon: 'pi pi-fw pi-home',
        routerLink: [''],
      },
      {
        label: 'Car list',
        icon: 'pi pi-fw pi-car',
        routerLink: ['/allCars'],
      },
      {
        label: 'Reservations',
        icon: 'pi pi-fw pi-calendar',
        items: [
          {
            label: 'My Reservations',
            icon: 'pi pi-fw pi-user',
            routerLink: ['/userReservations'],
          },
          {
            label: 'All Reservations',
            icon: 'pi pi-fw pi-user-edit',
            routerLink: ['/allReservations'],
          },
        ],
      },
      {
        label: 'Profile',
        icon: 'pi pi-fw pi-user',
        routerLink: ['/profile'],
      },
      {
        label: 'Users',
        icon: 'pi pi-fw pi-user',
        routerLink: ['/allUsers'],
      },
      {
        separator: true,
      },
    ];
    return items;
  }

  unlogedItems() {
    this.logged = false;
    var items = [
      {
        label: 'Home',
        icon: 'pi pi-fw pi-home',
      },
      {
        separator: true,
      },
      {
        label: 'Login',
        icon: 'pi pi-fw pi-sign-out',
      },
    ];
    return items;
  }
}
