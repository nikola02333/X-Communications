import { Component, OnInit } from '@angular/core';
import { WorkerService } from '../Services/workerService/worker.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Worker } from '../Models/Worker';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private workerService: WorkerService,
    private toastService: ToastrService,
    private route: ActivatedRoute,
    private router: Router) { }

  worker: Worker;
  submitted = false;
  formControls = this.workerService.form.controls;
  loggedIn = false;

  ngOnInit() {
    this.workerService.form.reset();
  }

  onSubmit() {
    this.submitted = true;

    if (this.workerService.form.valid) {
      this.worker = new Worker(this.workerService.form.value.id, this.workerService.form.value.email);
    }

    this.login(this.worker);
  }

  login(worker: Worker) {
    this.workerService.login(this.worker).subscribe(
      response => {
        debugger
        localStorage.setItem('token', response);
        this.loggedIn = true;
        this.router.navigate(['Worker']);
      },
      err => {
        this.toastService.error("Something went wrong");
        console.log(err);
      });
  }
}
