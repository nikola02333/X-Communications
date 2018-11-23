import { RegistratedUser } from '../../Models/RegistratedUser';
import { Component, OnInit } from '@angular/core';
import { RegistratedUserService } from '../../Services/registratedUserService/registrated-user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-registrated-users',
  templateUrl: './list-registrated-users.component.html',
  styleUrls: ['./list-registrated-users.component.css']
})
export class ListRegistratedUsersComponent implements OnInit {

  constructor(private service: RegistratedUserService, private toastService: ToastrService) { }
  selectedUser: RegistratedUser;
  users: RegistratedUser[] = [];

  onSelect(user: RegistratedUser): void {
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
    this.service.getAll().subscribe((data: Array<RegistratedUser>) => {
      this.users = data;
      console.log(data);
    }
    );
  }
}

