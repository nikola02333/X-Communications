import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Customer } from '../../Models/Customer';
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  form:FormGroup;

  constructor(private http: HttpClient) { 
    this.form = new FormGroup({
      fullName: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z]*')]),
      lastname: new FormControl('', [Validators.required, Validators.pattern('[a-zA-Z]*')]),
      id: new FormControl('', Validators.required),
    });
  }

  readonly baseUrl = 'https://localhost:44351/api/Customers';

  post(user: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.baseUrl, user);
  }

  getAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.baseUrl);
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }

  updateUser(user: Customer) {
    return this.http.put(this.baseUrl + '/' + user.id, user);
  }
}
