import { NgModule } from '@angular/core';
import { HomeComponent } from './cars/pages/home/home.component';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './users/pages/login/login.component';
import { AuthenticationService } from './shared/services/authentication.service';;
import { ProfileComponent } from './users/pages/profile/profile.component';
import { AddCarComponent } from './cars/pages/add-car/add-car.component';
import { AllUsersComponent } from './users/pages/all-users/all-users.component';
import { AllCarsComponent } from './cars/pages/all-cars/all-cars.component';
import { AllReservationsComponent } from './reservations/pages/all-reservations/all-reservations.component';
import { UserReservationsComponent } from './reservations/pages/user-reservations/user-reservations.component';
import { AddReservationComponent } from './reservations/pages/add-reservation/add-reservation.component';
import { CarDetailComponent } from './cars/pages/car-detail/car-detail.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthenticationService],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'addCar',
    component: AddCarComponent,
    canActivate: [AuthenticationService],
  },
  {
    path: 'allCars',
    component: AllCarsComponent,
    canActivate: [AuthenticationService],
  },
  {
    path: 'allCars',
    component: AllCarsComponent,
    canActivate: [AuthenticationService],
  },
   {
     path: 'detailedCarData/:id',
     component: CarDetailComponent,
     canActivate: [AuthenticationService],
   },
   {
    path: 'allReservations',
    component: AllReservationsComponent,
    canActivate: [AuthenticationService],
  },
   {
    path: 'userReservations',
    component: UserReservationsComponent,
    canActivate: [AuthenticationService],
  },
  {
    path: 'addReservation/:id',
    component: AddReservationComponent,
    canActivate: [AuthenticationService],
  }, 
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthenticationService],
  },
  {
    path: 'allUsers',
    component: AllUsersComponent,
    canActivate: [AuthenticationService],
  },
  {
    path: '**',
    component: HomeComponent,
    canActivate: [AuthenticationService],
  },
  // all reservations
  // reservation by id
  // addCar
  // updateCar
  // detailed car by id
  // user data by id
  // update user
  // add reservation
  // update reservation
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
