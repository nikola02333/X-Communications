import { Injectable } from '@angular/core';
import {HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Customer } from '../Models/Customer';
import { FormControl, FormGroup, Validators } from "@angular/forms";

import {RequestOptions, Request, RequestMethod} from '@angular/http';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

    constructor(private http: HttpClient) { }

    readonly baseUrl='https://localhost:44350/api/';
    form = new FormGroup({
      fullName: new FormControl('', Validators.required),
      lastname: new FormControl('', Validators.required),
      id: new FormControl('', Validators.required),
    });

    post(user: Customer) : Observable<Customer>
    {
      return this.http.post<Customer>(this.baseUrl+ 'Customers/',user);
      
    }

    getAll(): Observable<Customer[]> {

      return this.http.get<Customer[]>(this.baseUrl+'Customers/GetCustomers');
  } 
  
    deleteUser(id: number) {
      debugger
      return this.http.delete(this.baseUrl+'Customers' + '/' + id);
      }

      updateUser(user: Customer) {  
        return this.http.put(this.baseUrl+'PutCustomer'+ '/' + user.id,user);  
      }  
}
