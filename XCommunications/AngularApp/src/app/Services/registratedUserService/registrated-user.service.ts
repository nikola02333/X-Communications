import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { RegistratedUser } from 'src/app/Models/RegistratedUser';
import { WorkerService } from '../workerService/worker.service';

@Injectable({
  providedIn: 'root'
})

export class RegistratedUserService {

  constructor(private http: HttpClient,
              private workerService:WorkerService) { }

  readonly baseUrl = 'https://localhost:44351/api/RegistratedUsers';
  form = new FormGroup({
    id: new FormControl('', Validators.required),
    imsi: new FormControl('', Validators.required),
    customerId: new FormControl('', Validators.required),
    workerId: new FormControl('', Validators.required),
    numberId: new FormControl('', Validators.required)
  });

  post(user: RegistratedUser): Observable<RegistratedUser> {
    return this.http.post<RegistratedUser>(this.baseUrl, user, this.workerService.getAuthorization());
  }

  getAll(): Observable<RegistratedUser[]> {
    return this.http.get<RegistratedUser[]>(this.baseUrl, this.workerService.getAuthorization());
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + '/' + id, this.workerService.getAuthorization());
  }

  updateUser(user: RegistratedUser) {
    return this.http.put(this.baseUrl + '/' + user.id, user, this.workerService.getAuthorization());
  }
}
