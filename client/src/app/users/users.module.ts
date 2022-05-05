import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { AgGridModule } from 'ag-grid-angular';
import { ButtonModule } from 'primeng/button';

import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { LoginComponent } from './pages/login/login.component';
import { SharedModule } from '../shared/shared.module';
import { ProfileComponent } from './pages/profile/profile.component';
import { AllUsersComponent } from './pages/all-users/all-users.component';
import {CardModule} from 'primeng/card';
import {AvatarModule} from 'primeng/avatar';
import { BadgeModule } from "primeng/badge";
import { DividerModule } from 'primeng/divider';
import { BreadcrumbModule } from 'primeng/breadcrumb';

@NgModule({
  declarations: [AllUsersComponent, LoginComponent, ProfileComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    CommonModule,
    AgGridModule,
    SharedModule,
    ButtonModule,
    CardModule,
    AvatarModule,
    BadgeModule,
    DividerModule,
    BreadcrumbModule
  ]
})
export class UsersModule {}