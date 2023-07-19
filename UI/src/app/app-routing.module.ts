import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SignupComponent } from './components/signup/signup.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { AddBankComponent } from './components/add-bank/add-bank.component';
import { AddsmsComponent } from './components/addsms/addsms.component';
import { AddBinComponent } from './components/add-bin/add-bin.component';
import { CashoutComponent } from './components/cashout/cashout.component';
import { AlimentationWalletComponent } from './components/alimentation-wallet/alimentation-wallet.component';
import { AddBinFileComponent } from './components/add-bin-file/add-bin-file.component';
import { CashinComponent } from './components/cashin/cashin.component';
import { AddTokenComponent } from './components/add-token/add-token.component';
import { AddInACSComponent } from './components/add-in-acs/add-in-acs.component';


const routes: Routes = [
  {path:'', redirectTo:'login', pathMatch:'full'},
  {path:'login', component:LoginComponent},
  {path:'home', component:HomeComponent},
  {path:'signup', component:SignupComponent},
  {path:'addsms', component:AddsmsComponent},
  {path:'addbank', component:AddBankComponent},
  {path:'addbin', component:AddBinComponent},
  {path:'addtoken', component:AddTokenComponent},
  {path:'addbinfile', component:AddBinFileComponent},
  {path:'cashout', component:CashoutComponent},
  {path:'cashin', component:CashinComponent},
  {path:'addinacs', component:AddInACSComponent},
  {path:'alimentationwallet', component:AlimentationWalletComponent},
  {path:'dashboard', component:DashboardComponent, canActivate:[AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
