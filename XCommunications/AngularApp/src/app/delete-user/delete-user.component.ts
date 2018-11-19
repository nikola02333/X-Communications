import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserServiceService } from 'local/src/app/Services/user-service.service';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {

  constructor(private userService :UserServiceService) { }

  ngOnInit() {
  }
  onSubmit(form: NgForm)
  {
    this.userService.deleteUser(form.value).subscribe();
  }
}
