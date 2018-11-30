import { Component, OnInit } from '@angular/core';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Worker } from '../Models/Worker';
import { WorkerService } from '../Services/workerService/worker.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoutingModule } from '../app-routing.module';

@Component({
  selector: 'app-worker',
  templateUrl: './worker.component.html',
  styleUrls: ['./worker.component.css']
})
export class WorkerComponent implements OnInit {

  constructor(private workerService: WorkerService,
              private toastService: ToastrService,
              private route:ActivatedRoute,
              private router:Router) { }

  title = 'X-Communications';
  worker: WorkerComponent;
  loggedOut=false;

  ngOnInit() { 

  }

  logout()
  {
    this.loggedOut = this.workerService.logout();

    if(this.loggedOut)
    {
      this.router.navigate(['']);
    }
  }  
}
