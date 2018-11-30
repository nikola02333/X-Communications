import { Injectable } from '@angular/core';
import { Number } from 'src/app/Models/Number';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { FormControl, FormGroup, Validators } from "@angular/forms"
import { WorkerService } from '../workerService/worker.service';

@Injectable({
  providedIn: 'root'
})
export class ListNumbersService {

  readonly baseUrl = 'https://localhost:44351/api/Numbers';


  form = new FormGroup({
    id: new FormControl('', Validators.required),
    cc: new FormControl('', Validators.required),
    ndc: new FormControl('', Validators.required),
    sn: new FormControl('', Validators.required)
  });
  constructor(private http: HttpClient,
              private workerService:WorkerService) { }

  postNumber(services: Number): Observable<Number> {
    return this.http.post<Number>(this.baseUrl, services, this.workerService.getAuthorization());

  }

  getAllNumbers(): Observable<Number[]> {
    return this.http.get<Number[]>(this.baseUrl, this.workerService.getAuthorization());
  }

  deleteNumber(id: number) {
    return this.http.delete(this.baseUrl + '/' + id, this.workerService.getAuthorization());
  }

  updateNumber(number: Number) {
    return this.http.put(this.baseUrl + '/' + number.id, number, this.workerService.getAuthorization());
  }

  getAvailableNumbers():Observable<Number[]>{
    return this.http.get<Number[]>(this.baseUrl + '/GetAvailableNumber', this.workerService.getAuthorization());
  }
}
