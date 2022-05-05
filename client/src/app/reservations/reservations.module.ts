import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { AgGridModule } from 'ag-grid-angular';

import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { AllReservationsComponent } from './pages/all-reservations/all-reservations.component';
import { SharedModule } from '../shared/shared.module';
import { UserReservationsComponent } from './pages/user-reservations/user-reservations.component';
import {InputTextModule} from 'primeng/inputtext';
import { DividerModule } from "primeng/divider";
import { ReactiveFormsModule } from '@angular/forms';
import { AddReservationComponent } from './pages/add-reservation/add-reservation.component';
import { ButtonModule } from 'primeng/button';
import {SelectButtonModule} from 'primeng/selectbutton';
import { CardModule, } from 'primeng/card';
import { CalendarModule } from "primeng/calendar";
import { BreadcrumbModule } from 'primeng/breadcrumb';

@NgModule({
  declarations: [UserReservationsComponent, AllReservationsComponent, AddReservationComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    CommonModule,
    AgGridModule,
    SharedModule,
    InputTextModule,
    DividerModule,
    ReactiveFormsModule,
    ButtonModule,
    SelectButtonModule,
    CardModule,
    CalendarModule,
    BreadcrumbModule
  ]
})
export class ReservationsModule {}