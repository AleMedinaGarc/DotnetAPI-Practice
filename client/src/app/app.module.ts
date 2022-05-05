import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { FooterComponent } from './shared/components/footer/footer.component';
import { MenubarModule } from 'primeng/menubar';
import { AgGridModule } from 'ag-grid-angular';
import { SharedModule } from './shared/shared.module';
import {
  SocialLoginModule,
  SocialAuthServiceConfig,
  GoogleLoginProvider,
} from '@abacritt/angularx-social-login';
import { CarsModule } from './cars/cars.module';
import { UsersModule } from './users/users.module';
import { ReservationsModule } from './reservations/reservations.module';
import {BreadcrumbModule} from 'primeng/breadcrumb';


@NgModule({
  declarations: [AppComponent],
  imports: [
    CarsModule,
    UsersModule,
    ReservationsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    CommonModule,
    MenubarModule,
    AgGridModule,
    SharedModule,
    BreadcrumbModule
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '843954018382-5kj7b6jfklnkbjep5carmrd06cmapg2c.apps.googleusercontent.com'
            ),
          },
        ],
      } as SocialAuthServiceConfig,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
