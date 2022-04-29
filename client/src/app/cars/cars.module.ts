import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

// Angular material components
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CardModule, } from 'primeng/card';
import { DividerModule } from "primeng/divider";
import { ReactiveFormsModule } from '@angular/forms';
import {InputTextModule} from 'primeng/inputtext';

import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { AddCarComponent } from './pages/add-car/add-car.component';
import { AllCarsComponent } from './pages/all-cars/all-cars.component';
import { CarDetailComponent } from './pages/car-detail/car-detail.component';
import { HomeComponent } from './pages/home/home.component';
import { SharedModule } from '../shared/shared.module';
import { AgGridModule } from 'ag-grid-angular';
import {BreadcrumbModule} from 'primeng/breadcrumb';


@NgModule({
  declarations: [AddCarComponent, AllCarsComponent, CarDetailComponent, HomeComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    CommonModule,
    ButtonModule,
    CardModule,
    DividerModule,
    ReactiveFormsModule,
    InputTextModule,
    SharedModule,
    AgGridModule,
    BreadcrumbModule
  ]
})
export class CarsModule {}