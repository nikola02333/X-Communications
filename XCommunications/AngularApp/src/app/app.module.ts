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
import { DeleteUserComponent } from './delete-user/delete-user.component';
import { ListAllUsersComponent } from './list-all-users/list-all-users.component';
import { UserServiceService } from './Services/user-service.service';
import { HttpModule } from '@angular/http';
import {HttpClient, HttpClientModule } from '@angular/common/http'

const appRoutes: Routes =[
              {path : '' ,component: HomeComponent},
              {path : 'User' ,component: AddUserComponent},
              {path : 'UserDelete' ,component: DeleteUserComponent},
              {path : 'UserList' ,component: ListAllUsersComponent},
              
              { path: '**', redirectTo: '' }

];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddUserComponent,
     NgBoostrapDropdownDirective,
     DeleteUserComponent,
     ListAllUsersComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
     
    AppRoutingModule,
    FormsModule,
        ReactiveFormsModule,
      
        
    
  ],
  providers: [UserServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
