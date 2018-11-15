import { Injectable } from '@angular/core';
import {HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Customer } from '../Models/Customer';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  constructor(private http: HttpClient) { }
  addUser(services: Customer) : Observable<Customer>
  {
    console.log('bilo sta 2');
     return this.http.post<Customer>('https://localhost:44350/api/Customers/',services);
     /*.subscribe(

        (response) => console.log(response),
        (error)=> console.log(error)
       );*/
  }
}
