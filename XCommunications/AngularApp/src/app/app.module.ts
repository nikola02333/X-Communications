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

const appRoutes: Routes =[
              {path : '' ,component: HomeComponent},
              {path : 'User' ,component: AddUserComponent},

              { path: '**', redirectTo: '' }

];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddUserComponent,
     NgBoostrapDropdownDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
        ReactiveFormsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
