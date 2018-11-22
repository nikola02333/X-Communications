import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { RequestOptions, Request, RequestMethod } from '@angular/http';
import { RegistratedUser } from 'src/app/Models/RegistratedUser';

@Injectable({
  providedIn: 'root'
})
export class RegistratedUserService {

  constructor(private http: HttpClient) { }

  readonly baseUrl = 'https://localhost:44350/api/RegistratedUsers';
  form = new FormGroup({
    id: new FormControl('', Validators.required),
    imsi: new FormControl('', Validators.required),
    customerId: new FormControl('', Validators.required),
    workerId: new FormControl('', Validators.required),
    numberId: new FormControl('', Validators.required)
  });

  post(user: RegistratedUser): Observable<RegistratedUser> {
    debugger
    return this.http.post<RegistratedUser>(this.baseUrl, user);
  }

  getAll(): Observable<RegistratedUser[]> {

    return this.http.get<RegistratedUser[]>(this.baseUrl);
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + '/' + id);
  }

  updateUser(user: RegistratedUser) {
    return this.http.put(this.baseUrl + '/' + user.id, user);
  }
}
