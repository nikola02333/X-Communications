import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Worker } from '../../Models/Worker';
import { Observable } from 'rxjs';
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class WorkerService {

  form:FormGroup;

  constructor(private http: HttpClient) { 
    this.form = new FormGroup({
      id: new FormControl('', [Validators.required, Validators.pattern('[0-9]*')]),
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }

  readonly baseUrl = 'https://localhost:44351/api/Workers';

  postWorker(worker: Worker): Observable<Worker> {
    return this.http.post<Worker>(this.baseUrl, worker);
  }

  getAllWorkers(): Observable<Worker[]> {
    return this.http.get<Worker[]>(this.baseUrl);
  }

  deleteWorker(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }

  updateWorker(worker: Worker) {
    return this.http.put(this.baseUrl + '/' + worker.id, worker);
  }

  login(worker: Worker):boolean {
    return true;
    //return this.http.login(this.baseUrl + '/', worker);
  }
}
