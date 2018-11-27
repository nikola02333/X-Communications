import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddUserComponent } from '../app/User/add-user/add-user.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { NgBoostrapDropdownDirective } from './directives/ng-boostrap-dropdown.directive';
import { ListAllUsersComponent } from '../app/User/list-all-users/list-all-users.component';
import { UserServiceService } from '../app/Services/userService/user-service.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ListSimCardsComponent } from '../app/SimCard/list-sim-cards/list-sim-cards.component'
import { SimCardServiceService } from '../app/Services/simCardService/sim-card-service.service';
import { ListNumbersComponent } from '../app/Number/list-numbers/list-numbers.component'
import { ListNumbersService } from './Services/numberService/list-numbers.service';
import { AddSimcardComponent } from '../app/SimCard/add-simcard/add-simcard.component';
import { AddNumberComponent } from '../app/Number/add-number/add-number.component';
import { AddContractComponent } from '../app/Contract/add-contract/add-contract.component';
import { ListAllContractComponent } from '../app/Contract/list-all-contract/list-all-contract.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AddRegistratedUserComponent } from './RegistratedUser/add-registrated-user/add-registrated-user.component';
import { RegistratedUserService } from './Services/registratedUserService/registrated-user.service';
import { ListRegistratedUsersComponent } from './RegistratedUser/list-registrated-users/list-registrated-users.component';
import { WorkerService } from './Services/workerService/worker.service';

const appRoutes: Routes = [
  { path: 'User', component: AddUserComponent },
  { path: 'UsersList', component: ListAllUsersComponent },
  { path: 'SimCardsList', component: ListSimCardsComponent },
  { path: 'NumbersList', component: ListNumbersComponent },
  { path: 'SimCard', component: AddSimcardComponent },
  { path: 'AddNumber', component: AddNumberComponent },
  { path: 'AddContract', component: AddContractComponent },
  { path: 'ContractsList', component: ListAllContractComponent },
  { path: 'AddRegistratedUser', component: AddRegistratedUserComponent },
  { path: 'RegistratedUsersList', component: ListRegistratedUsersComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    AddUserComponent,
    NgBoostrapDropdownDirective,
    ListAllUsersComponent,
    ListSimCardsComponent,
    ListNumbersComponent,
    AddSimcardComponent,
    AddNumberComponent,
    AddContractComponent,
    ListAllContractComponent,
    AddRegistratedUserComponent,
    ListRegistratedUsersComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    AppRoutingModule,
    ReactiveFormsModule,
    FormGroup
  ],
  providers: [UserServiceService, SimCardServiceService, ListNumbersService, RegistratedUserService, WorkerService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
