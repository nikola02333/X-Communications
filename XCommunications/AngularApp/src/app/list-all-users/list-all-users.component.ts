import { Customer } from './../Models/Customer';
import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../Services/user-service.service';
import { FormControl, FormGroup, Validators, NgForm } from '@angular/forms';

@Component({
  selector: 'app-list-all-users',
  templateUrl: './list-all-users.component.html',
  styleUrls: ['./list-all-users.component.css']
})
export class ListAllUsersComponent implements OnInit {

  constructor(private service: UserServiceService) { }
  selectedUser: Customer;
  users: Customer[] = [];

  onSelect(user: Customer): void {
    this.selectedUser = user;
  }

  ngOnInit() {
    this.getAllUsers();
  }

  onClickDelete() {
    this.service.deleteUser(this.selectedUser.id).subscribe(


    );
  }

  getAllUsers() {
    this.service.getAll().subscribe((data: Array<Customer>) => {
      this.users = data;
      console.log(data);
    }
    );
  }

  onClickEdit() {
    this.service.updateUser(this.selectedUser = this.selectedUser).subscribe();
  }

}

