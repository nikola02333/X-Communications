import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WorkerComponent } from './worker/worker.component';
import { AddUserComponent } from './User/add-user/add-user.component';
import { ListAllUsersComponent } from './User/list-all-users/list-all-users.component';
import { ListSimCardsComponent } from './SimCard/list-sim-cards/list-sim-cards.component';
import { ListNumbersComponent } from './Number/list-numbers/list-numbers.component';
import { AddSimcardComponent } from './SimCard/add-simcard/add-simcard.component';
import { AddNumberComponent } from './Number/add-number/add-number.component';
import { AddContractComponent } from './Contract/add-contract/add-contract.component';
import { ListAllContractComponent } from './Contract/list-all-contract/list-all-contract.component';
import { AddRegistratedUserComponent } from './RegistratedUser/add-registrated-user/add-registrated-user.component';
import { ListRegistratedUsersComponent } from './RegistratedUser/list-registrated-users/list-registrated-users.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

const appRoutes: Routes = [
  { path: 'Worker', component: WorkerComponent },
  { path: 'User', component: AddUserComponent },
  { path: 'UsersList', component: ListAllUsersComponent },
  { path: 'SimCardsList', component: ListSimCardsComponent },
  { path: 'NumbersList', component: ListNumbersComponent },
  { path: 'SimCard', component: AddSimcardComponent },
  { path: 'AddNumber', component: AddNumberComponent },
  { path: 'AddContract', component: AddContractComponent },
  { path: 'ContractsList', component: ListAllContractComponent },
  { path: 'AddRegistratedUser', component: AddRegistratedUserComponent },
  { path: 'RegistratedUsersList', component: ListRegistratedUsersComponent },
  { path: 'home', component:HomeComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
