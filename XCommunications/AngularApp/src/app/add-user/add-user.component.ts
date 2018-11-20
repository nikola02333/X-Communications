import { ToastrModule, ToastrService } from 'ngx-toastr';
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
  constructor(private userService :UserServiceService, private toastService : ToastrService) { }

  custumer : Customer;
  submitted: boolean;
  showSuccessMessage: boolean;
  formControls = this.userService.form.controls;
  

  ngOnInit() {
  }

  onSubmit()
   {
      this.submitted = true;
      if (this.userService.form.valid)
      {
        this.custumer = new Customer(this.userService.form.value.id,this.userService.form.value.fullName,this.userService.form.value.lastname);
      
      
     this.userService.post(this.custumer).subscribe(
      response => {
           console.log(response);
      },
      err => {
           console.log(err);
      },
      () => {
        this.toastService.success('Inserted successfully','X-Communications');}); 
      }
      this.submitted=false;
       this.userService.form.reset();
    }

  }
  

  
 


