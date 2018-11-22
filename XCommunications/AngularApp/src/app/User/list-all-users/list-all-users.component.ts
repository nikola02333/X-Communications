import { Customer } from '../../Models/Customer';
import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../../Services/userService/user-service.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-all-users',
  templateUrl: './list-all-users.component.html',
  styleUrls: ['./list-all-users.component.css']
})
export class ListAllUsersComponent implements OnInit {

  constructor(private service: UserServiceService, private toastService: ToastrService) { }
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

      x => this.getAllUsers());
    if (this.users.length == 0) {
      this.toastService.info('There are no users');
    }

  }

  getAllUsers() {
    this.service.getAll().subscribe((data: Array<Customer>) => {

      this.users = data;
      console.log(data);
    }
    );
  }

  onClickEdit() {
    this.service.updateUser(this.selectedUser = this.selectedUser).subscribe(x => this.getAllUsers());
  }

}

