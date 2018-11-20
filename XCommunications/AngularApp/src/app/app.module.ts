import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AddUserComponent } from './add-user/add-user.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgBoostrapDropdownDirective } from './directives/ng-boostrap-dropdown.directive';
import { ListAllUsersComponent } from './list-all-users/list-all-users.component';
import { UserServiceService } from './Services/user-service.service';
import { HttpModule } from '@angular/http';
import {HttpClient, HttpClientModule } from '@angular/common/http';
import { ListSimCardsComponent } from './list-sim-cards/list-sim-cards.component'
import {SimCardServiceService} from '../app/Services/simCardService/sim-card-service.service';
import { ListNumbersComponent } from './list-numbers/list-numbers.component'
import { ListNumbersService } from './Services/numberService/list-numbers.service';
import { AddSimcardComponent } from './add-simcard/add-simcard.component';
import { AddNumberComponent } from './add-number/add-number.component';
import { AddContractComponent } from './add-contract/add-contract.component';
import { ListAllContractComponent } from './list-all-contract/list-all-contract.component';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

const appRoutes: Routes =[
              {path : '' ,component: HomeComponent},
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
    HomeComponent,
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
