import { TestBed, async } from '@angular/core/testing';
import { APP_BASE_HREF } from '@angular/common';
import {} from 'jasmine';
import { AppComponent } from '../app/app.component';
import { WorkerService } from '../app/Services/workerService/worker.service';
import { RegistratedUserService } from '../app/Services/registratedUserService/registrated-user.service';
import { ListNumbersService } from '../app/Services/numberService/list-numbers.service';
import { SimCardServiceService } from '../app/Services/simCardService/sim-card-service.service';
import { UserServiceService } from '../app/Services/user-service.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app/app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { ListRegistratedUsersComponent } from '../app/RegistratedUser/list-registrated-users/list-registrated-users.component';
import { AddRegistratedUserComponent } from '../app/RegistratedUser/add-registrated-user/add-registrated-user.component';
import { ListAllContractComponent } from '../app/Contract/list-all-contract/list-all-contract.component';
import { AddContractComponent } from '../app/Contract/add-contract/add-contract.component';
import { AddNumberComponent } from '../app/Number/add-number/add-number.component';
import { AddSimcardComponent } from '../app/SimCard/add-simcard/add-simcard.component';
import { ListNumbersComponent } from '../app/Number/list-numbers/list-numbers.component';
import { ListSimCardsComponent } from '../app/SimCard/list-sim-cards/list-sim-cards.component';
import { ListAllUsersComponent } from '../app/User/list-all-users/list-all-users.component';
import { NgBoostrapDropdownDirective } from '../app/directives/ng-boostrap-dropdown.directive';
import { AddUserComponent } from '../app/User/add-user/add-user.component';

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
          FormsModule,
          HttpClientModule,
          ReactiveFormsModule,
          RouterModule.forRoot(appRoutes),
          BrowserAnimationsModule,
          ToastrModule.forRoot(),
          AppRoutingModule
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
  
    it(`Should have as title 'X-Communications'`, async(() => {
      const fixture = TestBed.createComponent(AppComponent);
      const app = fixture.debugElement.componentInstance;
      expect(app.title).toEqual('X-Communications');
    }));

    it('should render title in a h1 tag', async(() => {
      const fixture = TestBed.createComponent(AppComponent);
      fixture.detectChanges();
      const compiled=fixture.debugElement.nativeElement;
      expect(compiled.querySelector('h1').textContent).toContain('X-Communications');
    }));
  });