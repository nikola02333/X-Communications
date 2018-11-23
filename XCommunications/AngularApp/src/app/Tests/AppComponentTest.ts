import { TestBed, async } from '@angular/core/testing';
import { APP_BASE_HREF } from '@angular/common';
import {} from 'jasmine';
import { AppComponent } from '../app.component';
import { WorkerService } from '../Services/workerService/worker.service';
import { RegistratedUserService } from '../Services/registratedUserService/registrated-user.service';
import { ListNumbersService } from '../Services/numberService/list-numbers.service';
import { SimCardServiceService } from '../Services/simCardService/sim-card-service.service';
import { UserServiceService } from '../Services/user-service.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { ListRegistratedUsersComponent } from '../RegistratedUser/list-registrated-users/list-registrated-users.component';
import { AddRegistratedUserComponent } from '../RegistratedUser/add-registrated-user/add-registrated-user.component';
import { ListAllContractComponent } from '../Contract/list-all-contract/list-all-contract.component';
import { AddContractComponent } from '../Contract/add-contract/add-contract.component';
import { AddNumberComponent } from '../Number/add-number/add-number.component';
import { AddSimcardComponent } from '../SimCard/add-simcard/add-simcard.component';
import { ListNumbersComponent } from '../Number/list-numbers/list-numbers.component';
import { ListSimCardsComponent } from '../SimCard/list-sim-cards/list-sim-cards.component';
import { ListAllUsersComponent } from '../User/list-all-users/list-all-users.component';
import { NgBoostrapDropdownDirective } from '../directives/ng-boostrap-dropdown.directive';
import { AddUserComponent } from '../User/add-user/add-user.component';

describe('AppComponent', () => {
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
    beforeEach(async(() => {
      TestBed.configureTestingModule({
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
          HttpClientModule,
          ReactiveFormsModule,
          RouterModule.forRoot(appRoutes),
          BrowserAnimationsModule,
          ToastrModule.forRoot(),
          AppRoutingModule,
          FormsModule,
          ReactiveFormsModule
        ],
        providers: [
          UserServiceService, 
          SimCardServiceService, 
          ListNumbersService, 
          RegistratedUserService, 
          WorkerService
        ]
      }).compileComponents();
    }));
  
    // check if title property contains what is expected
    it(`Should have as title 'App'`, async(() => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.debugElement.componentInstance;
      expect(app.title).toEqual('App');
    }));
  });