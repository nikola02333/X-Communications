import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators, NgForm} from '@angular/forms';
import { formArrayNameProvider } from '@angular/forms/src/directives/reactive_directives/form_group_name';
import {Customer} from '../Models/Customer';
import { UserServiceService } from '../Services/user-service.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  custumer : Customer;
  constructor(private service :UserServiceService) { }

  ngOnInit() {
  }
 
  onSubmit(form :NgForm)
  {
      console.log(form.value.firstName);
      console.log(form.value.lastname);

      this.custumer = new Customer(2,form.value.firstName,form.value.lastname);
      
      this.service.addUser(this.custumer).subscribe(() => console.log('bilo sta '));
  
      form.reset();

  }
 
 
    // do something else
}

