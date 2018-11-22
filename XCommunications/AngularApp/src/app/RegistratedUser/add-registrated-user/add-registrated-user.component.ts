import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { RegistratedUserService } from '../../Services/registratedUserService/registrated-user.service';
import { RegistratedUser } from '../../Models/RegistratedUser';
import { NgForm } from '@angular/forms';
import { Customer } from 'src/app/Models/Customer';
import { SimCard } from 'src/app/Models/SimCard';
import { Number } from 'src/app/Models/Number';
import { Worker } from 'src/app/Models/Worker';
import { getLocaleNumberSymbol } from '@angular/common';
import { SimCardServiceService } from 'src/app/Services/simCardService/sim-card-service.service';
import { UserServiceService } from 'src/app/Services/userService/user-service.service';
import { WorkerService } from 'src/app/Services/workerService/worker.service';
import { ListNumbersService } from 'src/app/Services/numberService/list-numbers.service';

@Component({
  selector: 'app-add-registrated-user',
  templateUrl: './add-registrated-user.component.html',
  styleUrls: ['./add-registrated-user.component.css']
})

export class AddRegistratedUserComponent implements OnInit {

  constructor(private registratedService: RegistratedUserService,
              private simCardService: SimCardServiceService,
              private customerService: UserServiceService,
              private workerService: WorkerService,
              private numberService: ListNumbersService,
              private toastService: ToastrService) { }

  submitted: boolean;
  user: RegistratedUser;
  simCards: SimCard[] = [];
  customers: Customer[] = [];
  workers: Worker[] = [];
  numbers: Number[] = [];
  formControls = this.registratedService.form.controls;

  ngOnInit() {
    this.getAllSimCards();
    this.getAllCustomers();
    this.getAllWorkers();
    this.getAllNumbers();
  }

  onSubmit(form: NgForm) {
    this.submitted = true;

    if (this.registratedService.form.valid) {
      this.user = new RegistratedUser(form.value.id, form.value.imsi, form.value.customerId, form.value.workerId, form.value.numberId);

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

  getAllSimCards() {
    this.simCardService.getAllSimCards().subscribe((data: Array<SimCard>) => {
      this.simCards = data;
    });
  }

  getAllCustomers() {
    this.customerService.getAll().subscribe((data: Array<Customer>) => {
      this.customers = data;
    });
  }

  getAllWorkers() {
    this.workerService.getAllWorkers().subscribe((data: Array<Worker>) => {
      this.workers = data;
    });
  }

  getAllNumbers() {
    this.numberService.getAllNumbers().subscribe((data: Array<Number>) => {
      this.numbers = data;
    });
  }
}