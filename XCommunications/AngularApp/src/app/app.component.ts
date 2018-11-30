import { Component } from '@angular/core';
import { TestBed, async } from '@angular/core/testing';
import { APP_BASE_HREF } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkerService } from './Services/workerService/worker.service';
import { Worker } from './Models/Worker';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {   

  constructor(private workerService: WorkerService,
              private toastService: ToastrService,
              private route: ActivatedRoute,
              private router:Router) {}
              

}
