import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Customer } from '../../Models/Customer';
import { UserServiceService } from '../../../app/Services/userService/user-service.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {
  constructor(private userService: UserServiceService, private toastService: ToastrService) { }

  custumer: Customer;
  submitted = false;
  formControls = this.userService.form.controls;

  ngOnInit() {
    //this.formControls= this.userService.form.controls;
  }

  onSubmit() {
    this.submitted = true;

    if (this.userService.form.valid) {
      this.custumer = new Customer(this.userService.form.value.id, this.userService.form.value.fullName, this.userService.form.value.lastname);

      this.userService.post(this.custumer).subscribe(
        response => {
          console.log(response);
        },
        err => {
          this.toastService.error("Something went wrong");

          console.log(err);
        },
        () => {
          this.toastService.success('User Inserted successfully', 'X-Communications');
        });
      this.submitted = false;
      this.userService.form.reset();
    }

  }

}






