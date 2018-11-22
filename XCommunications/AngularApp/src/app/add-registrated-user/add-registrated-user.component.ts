import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { RegistratedUserService } from '../Services/registratedUserService/registrated-user.service';
import { RegistratedUser } from '../Models/RegistratedUser';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-registrated-user',
  templateUrl: './add-registrated-user.component.html',
  styleUrls: ['./add-registrated-user.component.css']
})
export class AddRegistratedUserComponent implements OnInit {

  constructor(private registratedService: RegistratedUserService, private toastService: ToastrService) { }

  submitted: boolean;
  user: RegistratedUser;
  formControls = this.registratedService.form.controls;
  ngOnInit() {
  }

  onSubmit(form: NgForm) {
    this.submitted = true;
    debugger
    if (this.registratedService.form.valid) {
      this.user = new RegistratedUser(form.value.id, form.value.imsi, form.value.customerId, form.value.workerId, form.value.numberId);
      
      debugger

      this.registratedService.post(this.user).subscribe(
        response => {
          console.log(response);
        },
        err => {
          console.log(err);
        },
        () => {
          this.toastService.success('Inserted successfully', 'X-Communications');
        });
      this.submitted = false;
      form.resetForm();
    }
  }
}
