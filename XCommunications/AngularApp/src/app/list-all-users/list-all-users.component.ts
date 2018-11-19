import { Customer } from './../Models/Customer';
import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../Services/user-service.service';
import {FormControl, FormGroup, Validators, NgForm} from '@angular/forms';

@Component({
  selector: 'app-list-all-users',
  templateUrl: './list-all-users.component.html',
  styleUrls: ['./list-all-users.component.css']
})
export class ListAllUsersComponent implements OnInit {

  constructor(private service: UserServiceService) { }
   selectedUser : Customer;
   selectedUserUpdate: Customer;

   private  customers:  Array<object> = [];

   users : Customer[] =[
     new Customer(3,'nikola1','velickovic1'),
     new Customer(4,'nikola2','car2')
   ];

   onSelect(user: Customer): void {
    this.selectedUser = user;  
  }

  onSubmit(form :NgForm)
  {
      this.selectedUserUpdate = new Customer(2,form.value.firstName,form.value.lastname);
      this.service.post(this.selectedUserUpdate).subscribe();
  
      form.reset();
  }
 
   
  ngOnInit() {
  }
  onClickDelete()
  {
    
    this.service.deleteUser(this.selectedUser.id).subscribe();
  }
  getAllUsers(){
    this.service.getAll().subscribe( ( data: Array<Customer>) => 
    {
      this.customers =data;
      console.log(data);
    }
    );
  }

  onClickEdit()
{
  this.service.post(this.selectedUser);
}

}

