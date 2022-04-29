import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';


import { FormsModule } from '@angular/forms';
import { MenubarModule } from 'primeng/menubar';
import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';

@NgModule({
  declarations: [HeaderComponent, FooterComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    CommonModule,
    MenubarModule,
    ButtonModule,
    DividerModule
  ],
  exports:[
    HeaderComponent,
    FooterComponent
  ]
})
export class SharedModule {}