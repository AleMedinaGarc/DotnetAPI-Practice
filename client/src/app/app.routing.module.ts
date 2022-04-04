import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthenticationService } from './shared/services/authentication.service';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate:  [AuthenticationService]

  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '**', 
    component: LoginComponent
  }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {
}
