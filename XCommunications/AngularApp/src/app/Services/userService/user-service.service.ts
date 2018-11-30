import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Customer } from '../../Models/Customer';
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { WorkerService } from '../workerService/worker.service';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  form:FormGroup;

  constructor(private http: HttpClient,
              private workerService:WorkerService) { 
      this.form = new FormGroup({
      fullName: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z]*')]),
      lastname: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z]*')]),
      id: new FormControl('', Validators.required),
    });
  }

  readonly baseUrl = 'https://localhost:44351/api/Customers';

  post(user: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.baseUrl, user, this.workerService.getAuthorization());
  }

  getAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.baseUrl, this.workerService.getAuthorization());
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + '/' + id, this.workerService.getAuthorization());
  }

  updateUser(user: Customer) {
    return this.http.put(this.baseUrl + '/' + user.id, user, this.workerService.getAuthorization());
  }
}
