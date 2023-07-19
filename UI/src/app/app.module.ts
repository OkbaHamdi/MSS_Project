import { SignupComponent } from './components/signup/signup.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgToastModule } from 'ng-angular-popup';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { HomeComponent } from './components/home/home.component';
import { AddsmsComponent } from './components/addsms/addsms.component';
import { AddBankComponent } from './components/add-bank/add-bank.component';
import { AddBinComponent } from './components/add-bin/add-bin.component';
import { AddBinFileComponent } from './components/add-bin-file/add-bin-file.component';
import { CashoutComponent } from './components/cashout/cashout.component';
import { AlimentationWalletComponent } from './components/alimentation-wallet/alimentation-wallet.component';
import { CashinComponent } from './components/cashin/cashin.component';
import { AddTokenComponent } from './components/add-token/add-token.component';
import { AddInACSComponent } from './components/add-in-acs/add-in-acs.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    DashboardComponent,
    HomeComponent,
    AddsmsComponent,
    AddBankComponent,
    AddBinComponent,
    AddBinFileComponent,
    CashoutComponent,
    AlimentationWalletComponent,
    CashinComponent,
    AddTokenComponent,
    AddInACSComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    NgToastModule
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,
    useClass:TokenInterceptor,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
