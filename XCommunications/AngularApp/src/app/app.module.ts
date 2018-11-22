import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddUserComponent } from '../app/User/add-user/add-user.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgBoostrapDropdownDirective } from './directives/ng-boostrap-dropdown.directive';
import { ListAllUsersComponent } from '../app/User/list-all-users/list-all-users.component';
import { UserServiceService } from '../app/Services/userService/user-service.service';
import {HttpClient, HttpClientModule } from '@angular/common/http';
import { ListSimCardsComponent } from '../app/SimCard/list-sim-cards/list-sim-cards.component'
import {SimCardServiceService} from '../app/Services/simCardService/sim-card-service.service';
import { ListNumbersComponent } from '../app/Number/list-numbers/list-numbers.component'
import { ListNumbersService } from './Services/numberService/list-numbers.service';
import { AddSimcardComponent } from '../app/SimCard/add-simcard/add-simcard.component';
import { AddNumberComponent } from '../app/Number/add-number/add-number.component';
import { AddContractComponent } from '../app/Contract/add-contract/add-contract.component';
import { ListAllContractComponent } from '../app/Contract/list-all-contract/list-all-contract.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

const appRoutes: Routes =[
              {path : 'User' ,component: AddUserComponent},
              {path : 'UserList' ,component: ListAllUsersComponent},
              {path : 'SimCardList' ,component: ListSimCardsComponent},
              {path : 'NumberList' ,component: ListNumbersComponent},
              {path : 'SimCard',component : AddSimcardComponent},
              { path: 'AddNumber', component: AddNumberComponent},
              { path: 'AddContract', component: AddContractComponent},
              {path : 'ContractList', component: ListAllContractComponent}
              
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
     
     ListAllContractComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule, 
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule, 
    ToastrModule.forRoot(),
    AppRoutingModule,
    FormsModule,
        ReactiveFormsModule,
  ],
  providers: [UserServiceService, SimCardServiceService,ListNumbersService],
  bootstrap: [AppComponent]
})
export class AppModule { }
